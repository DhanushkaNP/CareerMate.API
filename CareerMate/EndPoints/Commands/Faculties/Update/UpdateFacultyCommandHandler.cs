using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Faculties.Update
{
    public class UpdateFacultyCommandHandler : IRequestHandler<UpdateFacultyCommand, BaseResponse>
    {
        public readonly IFacultyRepository _facultyRepository;

        public UpdateFacultyCommandHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<BaseResponse> Handle(UpdateFacultyCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.Id, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }
            
            faculty
                .UpdateName(command.Name)
                .UpdateShortName(command.ShortName)
                .UpdateEmail(command.Email);

            _facultyRepository.Update(faculty);

            await _facultyRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
