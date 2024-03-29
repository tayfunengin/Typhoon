using Microsoft.AspNetCore.Mvc;
using Typhoon.Core;
using Typhoon.Domain.DTOs.User;
using Typhoon.Service.Responses;
using Typhoon.Service.Services.Interfaces;

namespace Typhoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            this._authManager = authManager;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse>> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authManager.Register(userDto);
            if (!response.Success)
            {
                if (response is AuthorizationErrorResponse)
                {
                    ModelState.Clear();
                    var errorRes = (AuthorizationErrorResponse)response;
                    foreach (var error in errorRes.IdentityErrors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }

            return Ok(response);

        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authManager.Login(loginDto);

            if (!response.Success)
            {
                if (response is AuthorizationErrorResponse)
                {
                    ModelState.Clear();
                    var errorRes = (AuthorizationErrorResponse)response;
                    foreach (var error in errorRes.IdentityErrors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                else
                    return Ok(response);


            }

            return Ok(response);
        }

        [HttpPost]
        [Route("refreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse>> RefreshToken([FromBody] AuthResponseDto request)
        {
            var response = await _authManager.VerifyRefreshToken(request);

            if (!response.Success)
            {
                if (response is AuthorizationErrorResponse)
                {
                    ModelState.Clear();
                    var errorRes = (AuthorizationErrorResponse)response;
                    foreach (var error in errorRes.IdentityErrors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Unauthorized(ModelState);
                }
                else
                    return Unauthorized(response.Message);
            }

            return Ok(response);
        }
    }
}
