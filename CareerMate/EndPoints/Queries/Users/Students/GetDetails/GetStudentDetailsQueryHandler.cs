using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Users.Students.GetDetails
{
    public class GetStudentDetailsQueryHandler : IRequestHandler<GetStudentDetailsQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentDetailsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(GetStudentDetailsQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            switch (query.UserContext.Role)
            {
                case Roles.Student:
                    if (query.UserContext.Id != student.ApplicationUserId)
                    {
                        return new UnauthorizedResponse("You can't get other student's info");
                    }
                    break;
            }

            var studentDetail = new StudentDetailQueryItem
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DegreeName = student.Degree.Name,
                PathwayName = student.Pathway.Name,
                About = student.About,
                Headline = student.Headline,
                Location = student.Location,
                CompanyFeedbackLevel = student.CompanyFeedback.Level,
                CompanyFeedbackMessage = student.CompanyFeedback.Message,
                UniversityEmail = student.UniversityEmail,
                PersonalEmail = student.PersonalEmail,
                PhoneNumber = student.PhoneNumber,
                CGPA = student.CGPA,
                CVStatus = student.CVStatus,
                StudentId = student.StudentId,
                IsHired = student.IsHired(),
                ProfilePicFirebaseId = student.ProfilePicFirebaseId,
                CompanyName = student.Intern?.Company?.Name,
                CompanyId = student.Intern?.Company?.Id
            };

            return new GetStudentDetailsQueryResponse
            {
                Item = studentDetail,
            };
        }
    }
}
