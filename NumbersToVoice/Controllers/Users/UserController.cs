using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using NumbersToVoice.Authentication;
using NumbersToVoice.Entities;

namespace NumbersToVoice.Controllers.Users;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;

    public UserController(IUserRepository userRepository, IAuthRepository authRepository)
    {
        _userRepository = userRepository;
        _authRepository = authRepository;
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> RegisterUser(RegisterUser registerUser)
    {
        var existUser = await _userRepository.GetEmailAsync(registerUser.emailUser);
        

        if (existUser is not null)
        {
            return NotFound(UserErrors.EmailAlreadyExists(registerUser.emailUser));
        }
        
        var hashedPassword = _authRepository.HashPassword(registerUser.passwordUser);
        
        var user = new User(
            Guid.NewGuid(),
            registerUser.userName,
            hashedPassword,
            registerUser.emailUser
        );
        
        _userRepository.Add(user);
        return Ok(user.idUser);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LogIn(LoginUser loginUser)
    {
        var existUser = await _userRepository.GetEmailAsync(loginUser.emailUser);
        
        if(existUser is null)
        {
            return NotFound(UserErrors.NotFound);
        }
        
        if(!_authRepository.VerifyPassword(existUser.passwordUser, loginUser.passwordUser))
        {
            return BadRequest(UserErrors.InvalidCredentials);
        }
        
        string token = _authRepository.GenerateJwtToken(existUser);
        var response = new { token };
    
        return Ok(response);
    }
}