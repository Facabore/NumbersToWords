using System.ComponentModel.DataAnnotations;

namespace NumbersToVoice.Entities;

public class NumberRequest
{
    [RegularExpression(@"^\d{1,12}", ErrorMessage = "The number need between range from 0 to 999.999.999.999.")]
    public long Number { get; set; }

}