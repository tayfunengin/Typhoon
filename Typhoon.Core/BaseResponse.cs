using System.ComponentModel.DataAnnotations;

namespace Typhoon.Core
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationResult> ValidationErrors { get; set; }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public BaseResponse(bool success) : this(success, success ? "Transaction successful." : "Transaction failed.")
        {

        }
        public BaseResponse(string message) : this(false, message)
        {

        }
    }
}
