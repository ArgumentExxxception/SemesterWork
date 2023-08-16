using System.Security.Claims;
using HW2Sem.DTO;

namespace Core1.Interfaces;

public interface IAuthenticationService
{
    public Task<UserDto?> UserAuthenticationAsync(UserDto? userDto);
    public Task<string?> GetUserId(ClaimsPrincipal claimsPrincipal);
    public void LogOut();
}