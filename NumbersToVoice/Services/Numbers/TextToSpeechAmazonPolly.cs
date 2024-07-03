using Amazon.Polly;
using Amazon.Polly.Model;

namespace NumbersToVoice.Services;

public class TextToSpeechAmazonPolly : ITextToSpeech
{
    private readonly IConfiguration _configuration;
    
    public TextToSpeechAmazonPolly(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<SynthesizeSpeechResponse> ConvertAudio(string text)
    {
        var AWS_ACCESS_KEY = _configuration.GetConnectionString("AccessKeyPolly");
        var AWS_SECRET_ACCESS_KEY = _configuration.GetConnectionString("SecretKeyPolly");

        if (string.IsNullOrEmpty(AWS_ACCESS_KEY) || string.IsNullOrEmpty(AWS_SECRET_ACCESS_KEY))
        {
            throw new AggregateException("AWS Credentials are not configured");
        }

        var client = new AmazonPollyClient(AWS_ACCESS_KEY, AWS_SECRET_ACCESS_KEY, Amazon.RegionEndpoint.USEast1);

        var config = new SynthesizeSpeechRequest
        {
            Text = text,
            OutputFormat = OutputFormat.Mp3,
            VoiceId = "Lucia"
        };

        var audioMp3 = await client.SynthesizeSpeechAsync(config);
        return audioMp3;
        
    }
}