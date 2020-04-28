namespace InsuranceInc.Core.Entities
{
    //
    // The RoleType entity class defines the set of individual roles in the application. I created it to use
    // like an enum to avoid passing roles around as strings, so instead of 'Admin' we can use 'Role.Admin'.
    //
    // I used a static class with string properties rather than an enum type, because the [Authorize] attribute
    // requires roles to be passed as strings.
    //
    public static class RoleType
    {
        public const string User = "user";
        public const string Admin = "admin";
        public const string AdminOrUser = Admin + "," + User;
    }
}
