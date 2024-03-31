using CareerMate.Models.Entities.ApplicationUsers;

namespace CareerMate.Models.Entities.InternshipPosts
{
    public class ApprovalDetail
    {
        public bool IsApproved { get; private set; }

        public string CreatedUserRole { get; private set; }

        public ApplicationUser CreatedApplicationUser { get; private set; }
    }
}