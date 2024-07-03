using System.Globalization;

namespace NumbersToVoice.Services;

public class SelectLanguage : ISelectLanguage
{
    public CultureInfo GetLanguage(string lang)
    {
        var cultureLanguage = new CultureInfo($"{lang.ToLower()}-{lang.ToUpper()}");
        return cultureLanguage;
    }
}