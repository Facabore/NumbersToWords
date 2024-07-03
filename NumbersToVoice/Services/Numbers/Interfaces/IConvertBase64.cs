using Amazon.Polly;
using Amazon.Polly.Model;

namespace NumbersToVoice.Services;

public interface IConvertBase64
{
    string ConvertToBase64(SynthesizeSpeechResponse synthesizeSpeechResponse);
}