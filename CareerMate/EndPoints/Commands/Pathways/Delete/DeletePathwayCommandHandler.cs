using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Pathways;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Pathways.Delete
{
    public class DeletePathwayCommandHandler : IRequestHandler<DeletePathwayCommand, BaseResponse>
    {
        private readonly IPathwayRepository _pathwayRepository;
        private readonly IDegreeRepository _degreeRepository;
        private readonly IStudentRepository _studentRepository;

        public DeletePathwayCommandHandler(IPathwayRepository pathwayRepository, IDegreeRepository degreeRepository, IStudentRepository studentRepository)
        {
            _pathwayRepository = pathwayRepository;
            _degreeRepository = degreeRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(DeletePathwayCommand command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.DegreeId, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            Pathway pathway = await _pathwayRepository.GetByIdAsync(command.Id, cancellationToken);

            if (pathway == null)
            {
                return new NotFoundResponse<Pathway>();
            }

            if (await _studentRepository.AnyByPathwayId(command.Id, cancellationToken))
            {
                return new BadRequestResponse("Cannot delete degrees which already have students");
            }

            pathway.Delete();

            _pathwayRepository.Update(pathway);

            await _pathwayRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
