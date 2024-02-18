using Typhoon.Core;
using Typhoon.Domain.DTOs.User;
using Typhoon.Service.Responses;

namespace Typhoon.Service.Services.Interfaces
{
    public interface IAuthManager
    {
        Task<BaseResponse> Register(UserDto userDto);
        Task<BaseResponse> Login(LoginDto loginDto);
        Task<BaseResponse> CreateRefreshToken();
        Task<BaseResponse> VerifyRefreshToken(AuthResponseDto request);
    }
}
