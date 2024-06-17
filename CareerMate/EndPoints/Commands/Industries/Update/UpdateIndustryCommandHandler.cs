using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Industries;
using CareerMate.Models.Entities.Industries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Industries.Update
{
    public class UpdateIndustryCommandHandler : IRequestHandler<UpdateIndustryCommand, BaseResponse>
    {
        private readonly IIndustryRepository _industryRepository;

        public UpdateIndustryCommandHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<BaseResponse> Handle(UpdateIndustryCommand command, CancellationToken cancellationToken)
        {
            Industry industry = await _industryRepository.GetByIdAsync(command.Id, cancellationToken);

            if (industry == null)
            {
                return new NotFoundResponse<Industry>();
            }

            industry.SetName(command.Name);

            _industryRepository.Update(industry);

            await _industryRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
