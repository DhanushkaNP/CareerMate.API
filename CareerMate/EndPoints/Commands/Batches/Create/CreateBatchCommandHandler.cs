using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Batches;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.StudentBatches;
using CareerMate.Models.Entities.Students;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Batches.Create
{
    public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IBatchesRepository _batchesRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateBatchCommandHandler(IFacultyRepository facultyRepository, IBatchesRepository batchesRepository, IStudentRepository studentRepository)
        {
            _facultyRepository = facultyRepository;
            _batchesRepository = batchesRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(CreateBatchCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            if (await _batchesRepository.AnyBatchWithProvidedCode(command.BatchCode, cancellationToken))
            {
                return new BadRequestResponse(ErrorCodes.AlreadyBatchWithProvidedName, "Batch code already exist.");
            }


            using (var transaction = await _facultyRepository.BeginTransaction(cancellationToken))
            {
                StudentBatch studentBatch = new StudentBatch(
                    command.BatchCode,
                    DateOnly.FromDateTime(command.StartAt),
                    DateOnly.FromDateTime(command.EndAt),
                    DateOnly.FromDateTime(command.LastAllowedDateForStartInternship),
                    command.ValidInternshipPeriodInMonths,
                    command.DailyDiaryDueWeeks);
                _batchesRepository.Add(studentBatch);

                foreach(StudentCsvModel student in command.Students)
                {
                    Student newStudent = new Student(student.StudentId, student.Email);
                    newStudent.SetStudentBatch(studentBatch);
                    _studentRepository.Add(newStudent);
                }

                faculty.AddStudentBatch(studentBatch);
                _facultyRepository.Update(faculty);
                await _facultyRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }
               
            return new SuccessResponse();
        }
    }
}
