using Microsoft.AspNetCore.Identity;
using Typhoon.Core;

namespace Typhoon.Service.Responses
{
    public class AuthorizationErrorResponse : BaseResponse
    {
        public IEnumerable<IdentityError> IdentityErrors { get; set; }

        public AuthorizationErrorResponse(bool success) : base(success) { }

        public AuthorizationErrorResponse(List<IdentityError> failures) : base("One or more errors occurred.")
        {
            IdentityErrors = failures;
        }
    }
}

