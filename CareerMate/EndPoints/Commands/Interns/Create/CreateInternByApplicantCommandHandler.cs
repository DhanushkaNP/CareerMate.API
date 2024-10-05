using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Applicants;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.DailyRecords;
using CareerMate.Infrastructure.Persistence.Repositories.Interns;
using CareerMate.Infrastructure.Persistence.Repositories.Internships;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.DailyRecords;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Supervisors;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Interns.Create
{
    public class CreateInternByApplicantCommandHandler : IRequestHandler<CreateInternByApplicantCommand, BaseResponse>
    {
        private readonly IInternshipsRepository _internshipsRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IInternRepository _internRepository;
        private readonly IDailyDiaryRepository _dailyDiariesRepository;
        private readonly IDailyRecordRepository _dailyRecordRepository;
        private readonly ISupervisorRepository _SupervisorRepository;

        public CreateInternByApplicantCommandHandler(
            IInternshipsRepository internshipsRepository,
            IApplicantRepository applicantRepository,
            IInternRepository internRepository,
            IDailyDiaryRepository dailyDiariesRepository,
            IDailyRecordRepository dailyRecordRepository,
            ISupervisorRepository supervisorRepository)
        {
            _internshipsRepository = internshipsRepository;
            _applicantRepository = applicantRepository;
            _internRepository = internRepository;
            _dailyDiariesRepository = dailyDiariesRepository;
            _dailyRecordRepository = dailyRecordRepository;
            _SupervisorRepository = supervisorRepository;
        }

        public async Task<BaseResponse> Handle(CreateInternByApplicantCommand command, CancellationToken cancellationToken)
        {
            Applicant applicant = await _applicantRepository.GetByIdAsync(command.ApplicantId, cancellationToken);

            if (applicant == null)
            {
                return new NotFoundResponse<Applicant>();
            }

            Internship internship = await _internshipsRepository.GetByIdAsync(command.InternshipId, cancellationToken);

            if (internship == null)
            {
                return new NotFoundResponse<Internship>();
            }

            if (applicant.Student.Intern != null)
            {
                return new BadRequestResponse(ErrorCodes.AlreadyAnIntern, "Already an intern");
            }

            Supervisor supervisor = await _SupervisorRepository.GetByIdAsync(command.SupervisorId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            Guid internId;

            using (var transaction = await _dailyDiariesRepository.BeginTransaction(cancellationToken))
            {
                Intern intern = new Intern(command.StartAt, command.EndAt, applicant.Student, internship, internship.Company, supervisor);

                _internRepository.Add(intern);

                internId = intern.Id;

                // Create daily diaries and records for the internship period
                DateOnly currentDate = command.StartAt;
                DateOnly endDate = command.StartAt.AddMonths(applicant.Student.Batch.ValidInternshipPeriodInMonths);
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
                        new InternshipPeriod(command.StartAt, command.EndAt),
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

                _applicantRepository.Remove(applicant);

                await _internRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }

            return new CreatedResponse(internId);
        }
    }
}
