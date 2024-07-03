using System.Globalization;
using NumbersToVoice.Entities;

namespace NumbersToVoice.Services;

public interface ISelectLanguage
{
    CultureInfo GetLanguage(string lang);
}