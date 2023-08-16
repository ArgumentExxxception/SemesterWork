using System.Security.Claims;
using HW2Sem.DTO;
using HW2Sem.Exceptions;
using HW2Sem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using IAuthenticationService = Core1.Interfaces.IAuthenticationService;

namespace Infrastructure1.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<IAuthenticationService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ILogger<IAuthenticationService> logger)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<UserDto?> UserAuthenticationAsync(UserDto? userDto)
    {
        var allUsers = await _userRepository.GetAllUsersAsync();
        var person = allUsers.FirstOrDefault(p => p.Email == userDto?.Email);
        if (person != null && person.ValidatePassword(userDto.Password))
        {
            var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier, person.UserId.ToString()),
                new (ClaimsIdentity.DefaultRoleClaimType, person.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims,"Cookies");
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            if (_httpContextAccessor.HttpContext != null)
                await _httpContextAccessor.HttpContext.SignInAsync(claimPrincipal);
            _logger.LogInformation($"Пользователь - {person.UserId} вошел в аккаунт");
            return userDto;
        }
        
        throw new UnauthorizedException("Неверный пароль");
    }

    public async Task<string?> GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public async void LogOut()
    {
        if (_httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}