using Typhoon.Core;

namespace Typhoon.Service.Responses
{
    public class BaseEntityResponse : BaseResponse
    {
        public object? Data { get; set; }

        public BaseEntityResponse(bool success) : base(success) { }

        public BaseEntityResponse(string message) : base(message) { }

        public BaseEntityResponse(object data) : base(true)
        {
            Data = data;
        }

    }
}
