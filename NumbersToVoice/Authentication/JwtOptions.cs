namespace NumbersToVoice.Authentication;

public class JwtOptions
{
    public String Issuer { get; set; }
    public String Audience { get; set; }
    public String SecretKey { get; set; }
}