﻿using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Batches;
using CareerMate.Models.Entities.StudentBatches;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Batches.Update
{
    public class UpdateFacultyStudentBatchCommandHandler : IRequestHandler<UpdateFacultyStudentBatchCommand, BaseResponse>
    {
        private readonly IBatchesRepository _batchesRepository;

        public UpdateFacultyStudentBatchCommandHandler(IBatchesRepository batchesRepository)
        {
            _batchesRepository = batchesRepository;
        }

        public async Task<BaseResponse> Handle(UpdateFacultyStudentBatchCommand command, CancellationToken cancellationToken)
        {
            StudentBatch studentBatch = await _batchesRepository.GetByIdAsync(command.Id, cancellationToken);

            if (studentBatch == null)
            {
                return new NotFoundResponse<StudentBatch>();
            }

            studentBatch.Update(command.BatchCode, command.StartAt, command.EndAt, command.LastAllowedDateForStartInternship);

            _batchesRepository.Update(studentBatch);
            
            await _batchesRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
