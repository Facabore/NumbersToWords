using System.Globalization;
using NumbersToVoice.Entities;

namespace NumbersToVoice.Services;

public interface INumbersToWords
{
    string ConvertToText(CultureInfo lang, NumberRequest number);
 
}