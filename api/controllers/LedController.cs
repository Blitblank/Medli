
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LedController : ControllerBase
{
    private static string _ledColor = "off";

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new { color = _ledColor });
    }

    [HttpPost("setColor")]
    public IActionResult SetColor([FromBody] string color)
    {
        _ledColor = color;
        return Ok(new { message = $"LED color set to {color}" });
    }
}