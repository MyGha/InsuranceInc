using System.ComponentModel.DataAnnotations;

namespace InsuranceInc.Core.Models
{
    //
    // The Authenticate model defines the parameters for incoming requests to the /users/authenticate route of the api, because it
    // is set as the parameter to the Authenticate method of the UsersController. When an HTTP POST request is received to the route,
    // the data from the body is bound to an instance of the AuthenticateModel, validated and passed to the method.
    //
    // ASP.NET Core Data Annotations are used to automatically handle model validation, the [Required] attribute sets both the username
    // and password as required fields so if either are missing a validation error message is returned from the api.
    //
    public class AuthenticateModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
