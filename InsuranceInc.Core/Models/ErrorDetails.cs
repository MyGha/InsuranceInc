using System.Text.Json;

namespace InsuranceInc.Core.Models
{
    //
    // Used in ExceptionMiddlewareExtensions.
    //
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ErrorDetails>(this);
    }
}
