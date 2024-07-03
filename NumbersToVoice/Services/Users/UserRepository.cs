using Microsoft.EntityFrameworkCore;
using NumbersToVoice.Database;

namespace NumbersToVoice.Entities;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext context)
    {
        _databaseContext = context;
    }
    public Task<User?> GetEmailAsync(string email)
    {
        return _databaseContext.Users.FirstOrDefaultAsync(u => u.emailUser == email)!;
    }

    public void Add(User user)
    {
        _databaseContext.Users.Add(user);
        _databaseContext.SaveChangesAsync();
    }
}