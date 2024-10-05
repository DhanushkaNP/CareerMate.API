using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Industries;
using CareerMate.Models.Entities.Industries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Industries.Delete
{
    public class DeleteIndustryCommandHandler : IRequestHandler<DeleteIndustryCommand, BaseResponse>
    {
        private readonly IIndustryRepository _industryRepository;

        public DeleteIndustryCommandHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<BaseResponse> Handle(DeleteIndustryCommand command, CancellationToken cancellationToken)
        {
            Industry industry = await _industryRepository.GetByIdAsync(command.Id, cancellationToken);

            if (industry == null)
            {
                return new NotFoundResponse<Industry>();
            }

            industry.Delete();

            _industryRepository.Update(industry);

            await _industryRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
