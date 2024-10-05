using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Interns;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.Supervisors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Interns.GetSupervisorInterns
{
    public class GetSupervisorInternsQueryHandler : IRequestHandler<GetSupervisorInternsQuery, BaseResponse>
    {
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly IInternRepository _internRepository;

        public GetSupervisorInternsQueryHandler(
            ISupervisorRepository supervisorRepository,
            IInternRepository internRepository)
        {
            _supervisorRepository = supervisorRepository;
            _internRepository = internRepository;
        }

        public async Task<BaseResponse> Handle(GetSupervisorInternsQuery query, CancellationToken cancellationToken)
        {
            Supervisor supervisor = await _supervisorRepository.GetByIdAsync(query.SupervisorId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            return new GetSupervisorInternsQueryResponse
            {
                Items = await _internRepository.GetSupervisorInterns(query, supervisor.Id, cancellationToken)
            };
        }
    }
}
