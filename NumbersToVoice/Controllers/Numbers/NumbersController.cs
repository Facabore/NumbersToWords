using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NumbersToVoice.Controllers.Numbers;
using NumbersToVoice.Controllers.Request;
using NumbersToVoice.Services;

using NumbersToVoice.Entities;
using NumbersToVoice.Helpers;

namespace NumbersToVoice.Controllers;

[Authorize]
[ApiController]
[Route("/api/numbers")]
public class NumbersController : ControllerBase
{
    private readonly INumbersToWords _numbersToWords;
    private readonly ITextToSpeech _textToSpeech;
    private readonly ISelectLanguage _selectLanguage;
    private readonly IConvertBase64 _convertBase64;


    public NumbersController(INumbersToWords numbersToWords, ITextToSpeech textToSpeech, ISelectLanguage selectLanguage, IConvertBase64 convertBase64)
    {
        _numbersToWords = numbersToWords;
        _textToSpeech = textToSpeech;
        _selectLanguage = selectLanguage;
        _convertBase64 = convertBase64;
    }

    [HttpPost("/convert")]
    public IActionResult ConvertToText(RequestNumber requestNumber)
    {
        try
        {
            var numberRequest = new NumberRequest { Number = requestNumber.number };

            var validationResult = NumberValidator.ValidateNumber(numberRequest);

            if (validationResult != null)
            {
                return validationResult;
            }

            var culture = _selectLanguage.GetLanguage(requestNumber.lang);
            var words = _numbersToWords.ConvertToText(culture, numberRequest);
            var audioMp3 = _textToSpeech.ConvertAudio(words);
            var audioBase64 = _convertBase64.ConvertToBase64(audioMp3.Result);

            var response = new ResponseNumber(
                words,
                audioBase64
            );
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
