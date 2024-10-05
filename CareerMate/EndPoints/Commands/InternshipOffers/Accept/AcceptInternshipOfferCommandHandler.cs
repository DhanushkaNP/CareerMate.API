using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.DailyRecords;
using CareerMate.Infrastructure.Persistence.Repositories.Interns;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipOffers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.DailyRecords;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.Students;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.InternshipOffers.Accept
{
    public class AcceptInternshipOfferCommandHandler : IRequestHandler<AcceptInternshipOfferCommand, BaseResponse>
    {
        private readonly IInternshipOfferRepository _internshipOfferRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IInternRepository _internRepository;
        private readonly IDailyDiaryRepository _dailyDiariesRepository;
        private readonly IDailyRecordRepository _dailyRecordRepository;

        public AcceptInternshipOfferCommandHandler(
            IInternshipOfferRepository internshipOfferRepository,
            IStudentRepository studentRepository,
            IInternRepository internRepository,
            IDailyDiaryRepository dailyDiariesRepository,
            IDailyRecordRepository dailyRecordRepository)
        {
            _internshipOfferRepository = internshipOfferRepository;
            _studentRepository = studentRepository;
            _internRepository = internRepository;
            _dailyDiariesRepository = dailyDiariesRepository;
            _dailyRecordRepository = dailyRecordRepository;
        }

        public async Task<BaseResponse> Handle(AcceptInternshipOfferCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            if (student.IsHired())
            {
                return new BadRequestResponse(ErrorCodes.AlreadyAnIntern, "Student is already hired");
            }

            InternshipOffer internshipOffer = await _internshipOfferRepository.GetByIdAsync(command.InternshipOfferId, cancellationToken);

            if (internshipOffer == null)
            {
                return new NotFoundResponse<InternshipOffer>();
            }

            Guid internId;

            using (var transaction = await _dailyDiariesRepository.BeginTransaction(cancellationToken))
            {
                Intern intern = new Intern(
                    internshipOffer.StartedDate,
                    internshipOffer.EndedDate,
                    student,
                    internshipOffer.Internship,
                    internshipOffer.Internship.Company,
                    internshipOffer.Supervisor);

                _internRepository.Add(intern);

                internId = intern.Id;

                // Create daily diaries and records for the internship period
                DateOnly currentDate = internshipOffer.StartedDate;
                DateOnly endDate = internshipOffer.StartedDate.AddMonths(student.Batch.ValidInternshipPeriodInMonths);
                int week = 1;

                while (currentDate <= endDate)
                {
                    DateOnly weekStart = currentDate;
                    DateOnly weekEnd = weekStart.AddDays(6 - (int)weekStart.DayOfWeek);

                    if (weekEnd > endDate)
                    {
                        weekEnd = endDate;
                    }

                    DailyDiary dailyDiary = new DailyDiary(
                        new PeriodCovered(weekStart, weekEnd),
                        new InternshipPeriod(internshipOffer.StartedDate, internshipOffer.EndedDate),
                        week,
                        intern);

                    if (DateOnly.FromDateTime(DateTime.Now) >= weekStart)
                    {
                        dailyDiary.Unlock();
                    }

                    _dailyDiariesRepository.Add(dailyDiary);

                    // Create daily records for each day in the week
                    for (DateOnly date = weekStart; date <= weekEnd; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                        {
                            DailyRecord dailyRecord = new DailyRecord(
                            date.DayOfWeek,
                            date);

                            dailyRecord.SetDailyDiary(dailyDiary);
                            _dailyRecordRepository.Add(dailyRecord);
                        }
                    }

                    // Move to the next week
                    currentDate = weekEnd.AddDays(1);
                    week++;
                }

                _internshipOfferRepository.Remove(internshipOffer);

                await _internRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }

            return new CreatedResponse(internId);
        }
    }
}
