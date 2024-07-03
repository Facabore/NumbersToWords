namespace NumbersToVoice.Entities;

public interface IUserRepository
{ 
    Task<User?> GetEmailAsync(string email);
    void Add(User user);
}