using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipOffers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.InternshipOffers.GetList
{
    public class GetInternshipOffersListQueryHandler : IRequestHandler<GetInternshipOffersListQuery, BaseResponse>
    {
        private readonly IInternshipOfferRepository _internshipOfferRepository;
        private readonly IStudentRepository _studentRepository;

        public GetInternshipOffersListQueryHandler(
            IInternshipOfferRepository internshipOfferRepository,
            IStudentRepository studentRepository)
        {
            _internshipOfferRepository = internshipOfferRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(GetInternshipOffersListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<InternshipOfferQueryItem>
            {
                Items = await _internshipOfferRepository.GetInternshipOffers(query.StudentId, cancellationToken),
            };
        }
    }
}
