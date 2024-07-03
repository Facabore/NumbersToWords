using Amazon.Polly.Model;

namespace NumbersToVoice.Services;

public interface ITextToSpeech
{
    Task<SynthesizeSpeechResponse> ConvertAudio(string text);
}