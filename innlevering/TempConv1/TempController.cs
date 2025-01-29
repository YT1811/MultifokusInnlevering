using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TemperatureConverter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            try
            {
                double result = ConvertTemperature(request.Value, request.FromUnit, request.ToUnit);
                return Ok(new { result = Math.Round(result, 2) });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private double ConvertTemperature(double value, string fromUnit, string toUnit)
        {
            // Same unit conversion
            if (fromUnit == toUnit)
                return value;

            // Convert to Celsius as intermediate step
            double celsius = fromUnit switch
            {
                "F" => (value - 32) * 5 / 9,
                "K" => value - 273.15,
                "C" => value,
                _ => throw new ArgumentException("Invalid source unit")
            };

            // Convert from Celsius to target unit
            return toUnit switch
            {
                "F" => (celsius * 9 / 5) + 32,
                "K" => celsius + 273.15,
                "C" => celsius,
                _ => throw new ArgumentException("Invalid target unit")
            };
        }
    }

    public class ConversionRequest
    {
        public double Value { get; set; }

        [Required(ErrorMessage = "FromUnit is required")]
        [RegularExpression("^[FCK]$", ErrorMessage = "FromUnit must be either F, C, or K")]
        public required string FromUnit { get; set; }

        [Required(ErrorMessage = "ToUnit is required")]
        [RegularExpression("^[FCK]$", ErrorMessage = "ToUnit must be either F, C, or K")]
        public required string ToUnit { get; set; }
    }
}