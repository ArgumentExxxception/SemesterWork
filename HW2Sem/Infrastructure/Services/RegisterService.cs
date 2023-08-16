using Core1.Interfaces;
using Hangfire;
using HW2Sem.DTO;
using HW2Sem.Entities;
using HW2Sem.Exceptions;
using HW2Sem.Models;
using HW2Sem.Repositories.Interfaces;

namespace Infrastructure1.Services;

public class RegisterService: IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IMailService _mailSender;

    public RegisterService(IUserRepository userRepository, IMailService mailSender)
    {
        _userRepository = userRepository;
        _mailSender = mailSender;
    }
    public async Task<RegisterUserDto?> RegisterUser(RegisterUserDto registerUserDto)
    {
        var allUsers = await _userRepository.GetAllUsersAsync();
        if (allUsers.FirstOrDefault(u => u.Email == registerUserDto.Email) != null)
        {
            throw new BadRequest("Пользователь уже существует");
        }
        
        var user = new User()
        {
            UserId = new int(),
            Email = registerUserDto.Email,
            Username = registerUserDto.Username,
            Password = registerUserDto.Password,
            Role = "user",
            Balance = 0
        };

        var result = await _userRepository.AddUserAsync(user);
        
        if (result != null)
        {
            var request = new WelcomeRequest()
            {
                ToEmail = user.Email,
                Username = user.Username
            };
            BackgroundJob.Enqueue(() =>
                _mailSender.SendWelcomeEmailAsync(request));
            return registerUserDto;
        }

        throw new Exception("Что то пошло не так! Попробуйте снова");
    }
}