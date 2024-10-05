using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Models.Entities.Degrees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Degrees.Update
{
    public class UpdateDegreeCommandHandler : IRequestHandler<UpdateDegreeCommand, BaseResponse>
    {
        private readonly IDegreeRepository _degreeRepository;

        public UpdateDegreeCommandHandler(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(UpdateDegreeCommand command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.Id, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            degree
                .SetName(command.Name)
                .SetAcronym(command.Acronym);

            _degreeRepository.Update(degree);

            await _degreeRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
