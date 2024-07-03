using NumbersToVoice.Entities;

namespace NumbersToVoice.Authentication;

public interface IAuthRepository
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
    string GenerateJwtToken(User user);
}