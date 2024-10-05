using System;

namespace CareerMate.Abstractions
{
    public class ErrorCodes
    {
        public static readonly Guid LoggingUserDetailsIncorrect = new Guid("a263cd1b-2c5b-4ddc-b312-7acf61ced383");
        public static readonly Guid ExistingUser = new Guid("7fabc0b0-424d-4874-b74c-b75b0b12160e");
        public static readonly Guid AlreadyBatchWithProvidedName = new Guid("866d4772-ea17-4a47-bafe-78080d1e0355");
        public static readonly Guid StudentCsvNotInCorrectFormat = new Guid("bdf2b9be-ae39-4a4a-94eb-532990bf09da");
        public static readonly Guid InvalidStudentData = new Guid("bfa8ebf4-e2de-451b-9a1c-e5a4813a9b21");
        public static readonly Guid InvalidFacultyForCompany = new Guid("5d279e49-be33-41fa-84f4-5ebc28050a33");
        public static readonly Guid InvalidUniversityForCompany = new Guid("828214d3-313e-4be7-b06a-190d964a72db");
        public static readonly Guid DailyDiaryNotComplete = new Guid("60ed65e3-9836-4764-af76-22f66facd07f");
        public static readonly Guid AlreadyAnIntern = new Guid("3d23938a-334a-42a1-8c26-50b628f605b9");
        public static readonly Guid AllreadyGaveAnOffer = new Guid("f7e0ff56-b341-48e8-a90d-ad57ddfefb98");
    }
}
