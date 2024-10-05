using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Industries;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Industries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Industries.Create
{
    public class CreateIndustryCommandHandler : IRequestHandler<CreateIndustryCommand, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IIndustryRepository _industryRepository;

        public CreateIndustryCommandHandler(IFacultyRepository facultyRepository, IIndustryRepository industryRepository)
        {
            _facultyRepository = facultyRepository;
            _industryRepository = industryRepository;
        }

        public async Task<BaseResponse> Handle(CreateIndustryCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Industry industry = new Industry(command.Name);

            industry.SetFaculty(faculty);

            _industryRepository.Add(industry);

            await _industryRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse();
        }
    }
}
