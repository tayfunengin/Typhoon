using FluentValidation.Results;
using Typhoon.Core;

namespace Typhoon.Service.Responses
{
    public class ValidationErrorResponse : BaseResponse
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; set; }

        public ValidationErrorResponse(List<ValidationFailure> failures) : base("One or more validation errors occurred.")
        {
            ValidationErrors = failures;
        }
    }
}
