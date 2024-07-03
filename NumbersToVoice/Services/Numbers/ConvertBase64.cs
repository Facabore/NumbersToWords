using Amazon.Polly;
using Amazon.Polly.Model;

namespace NumbersToVoice.Services;

public class ConvertBase64 : IConvertBase64
{
    public string ConvertToBase64(SynthesizeSpeechResponse synthesizeSpeechResponse)
    {
        using (var memoryStream = new MemoryStream())
        {
            synthesizeSpeechResponse.AudioStream.CopyTo(memoryStream);
            byte[] audioBytes = memoryStream.ToArray();
            string base64Audio = Convert.ToBase64String(audioBytes);

            return base64Audio;
        }
    }
}