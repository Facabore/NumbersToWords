using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using NumbersToVoice.Authentication;

namespace NumbersToVoice.AuthOptions;

public class JwtConfig : IConfigureOptions<JwtOptions>
{
    private readonly string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}