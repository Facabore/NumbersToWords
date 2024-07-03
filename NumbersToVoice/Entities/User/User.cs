namespace NumbersToVoice.Entities;

public class User
{
    public User(Guid idUser, string nameUser, string passwordUser, string emailUser)
    {
        this.idUser = idUser;
        this.nameUser = nameUser;
        this.passwordUser = passwordUser;
        this.emailUser = emailUser;
    }

    public Guid idUser { get; set; }
    public string nameUser { get; set; }
    public string passwordUser { get; set; }
    public string emailUser { get; set; }
    
    }