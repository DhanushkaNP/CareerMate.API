namespace CareerMate.Abstractions.Enums
{
    public class Policies
    {
        public const string SysAdminOnly = "AllowedSysAdmin";
        public const string CoordinatorOnly = "AllowedCoordinatorOnly";
        public const string StudentOnly = "StudentOnly";
        public const string CompaniesOnly = "CompaniesOnly";
        public const string SupervisorOnly = "SupervisorOnly";
        
        public const string CoordinatorAssistantLevel = "AllowedCoordinatorAssistantLevel";
        public const string CoordinatorLevel = "AllowedCoordinatorLevel";
        public const string AllUserRoles = "AllUsers";

        public const string CompanyAndCoordinatorLevel = "CompanyAndCoordinatorLevel";
        public const string StudentAndCompanyLevel = "StudentAndCompanyLevel";
    }
}
