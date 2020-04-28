using System.Text.Json;

namespace InsuranceInc.Core.Entities
{
    //
    // The User entity class represents the data for a user in the application.
    // Purpose: authentication and authorization.
    //
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public override string ToString() => JsonSerializer.Serialize<User>(this);
    }
}
