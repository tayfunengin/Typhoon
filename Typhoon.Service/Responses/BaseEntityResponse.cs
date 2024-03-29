using Typhoon.Core;

namespace Typhoon.Service.Responses
{
    public class BaseEntityResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public BaseEntityResponse(bool success) : base(success) { }

        public BaseEntityResponse(string message) : base(message) { }

        public BaseEntityResponse(bool success, string message) : base(success, message) { }
        public BaseEntityResponse(T data) : base(true)
        {
            Data = data;
        }

    }
}
