using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Typhoon.Core;
using Typhoon.Domain.DTOs.User;
using Typhoon.Domain.Entities;
using Typhoon.Service.Responses;
using Typhoon.Service.Services.Interfaces;

namespace Typhoon.Service.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private User _user;

        private const string _loginProvider = "TyphoonsApi";
        private const string _refreshToken = "RefreshToken";
        public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
            this._logger = logger;
        }
        public async Task<BaseResponse> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

            if (!result.Succeeded)
                return new AuthorizationErrorResponse(result.Errors.ToList());


            return new BaseEntityResponse<string>(data: newRefreshToken);

        }

        public async Task<BaseResponse> Login(LoginDto loginDto)
        {
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            _logger.LogWarning($"User with email {loginDto.Email} was not found.");

            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user == null || isValidUser == false)
            {
                return new BaseEntityResponse<User>("Email or password is wrong.");
            }

            return await GenerateAutAndRefreshTokens();

        }

        public async Task<BaseResponse> Register(UserDto userDto)
        {
            _user = _mapper.Map<User>(userDto);
            _user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(_user, "User");
            else
            {
                return new AuthorizationErrorResponse(result.Errors.ToList());
            }

            return new BaseEntityResponse<User>(true);
        }

        public async Task<BaseResponse> VerifyRefreshToken(AuthResponseDto request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(request.Token);

            var username = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;

            _user = await _userManager.FindByEmailAsync(username);

            if (_user == null || _user.Id != request.UserId)
            {
                return new BaseEntityResponse<User>("User not found!");
            }

            var isRefreshTokenValid = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);
            if (isRefreshTokenValid)
                return await GenerateAutAndRefreshTokens();

            await _userManager.UpdateSecurityStampAsync(_user);
            return new BaseEntityResponse<User>("Refresh token not valid!");

        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid",_user.Id)
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMunites"])),
                claims: claims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<BaseResponse> GenerateAutAndRefreshTokens()
        {
            var token = await GenerateToken();
            var refreshTokenResult = await CreateRefreshToken();
            if (!refreshTokenResult.Success)
                return refreshTokenResult;

            var dto = new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = ((BaseEntityResponse<string>)refreshTokenResult).Data
            };

            return new BaseEntityResponse<AuthResponseDto>(dto);
        }

    }
}
