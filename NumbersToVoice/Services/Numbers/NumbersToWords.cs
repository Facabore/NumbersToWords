using System.Globalization;
using NumbersToVoice.Entities;
using Humanizer;


namespace NumbersToVoice.Services;

public class NumbersToWords : INumbersToWords
{

    public string ConvertToText(CultureInfo lang, NumberRequest numberRequest)
    {
        return numberRequest.Number.ToWords(lang);
    }
}