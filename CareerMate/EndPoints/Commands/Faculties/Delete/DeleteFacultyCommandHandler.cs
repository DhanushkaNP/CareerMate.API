using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Faculties.Delete
{
    public class DeleteFacultyCommandHandler : IRequestHandler<DeleteFacultyCommand, BaseResponse>
    {
        public readonly IFacultyRepository _facultyRepository;

        public DeleteFacultyCommandHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<BaseResponse> Handle(DeleteFacultyCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.Id, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            _facultyRepository.Remove(faculty);

            await _facultyRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
