using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Degrees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Degrees.Delete
{
    public class DeleteDegreeCommandHandler : IRequestHandler<DeleteDegreeCommand, BaseResponse>
    {
        private readonly IDegreeRepository _degreeRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteDegreeCommandHandler(IDegreeRepository degreeRepository, IStudentRepository studentRepository)
        {
            _degreeRepository = degreeRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(DeleteDegreeCommand command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.Id, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            if (await _studentRepository.AnyByDegreeId(command.Id, cancellationToken))
            {
                return new BadRequestResponse("Cannot delete degrees which already have students");
            }

            degree.Delete();

            _degreeRepository.Update(degree);

            await _degreeRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
