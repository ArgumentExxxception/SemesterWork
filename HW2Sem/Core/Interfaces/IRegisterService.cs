using HW2Sem.DTO;

namespace Core1.Interfaces;

public interface IRegisterService
{
    public Task<RegisterUserDto?> RegisterUser(RegisterUserDto registerUserDto);
}