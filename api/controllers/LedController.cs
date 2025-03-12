
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LedController : ControllerBase
{

    public class LedColorRequest
    {
        public required string Color { get; set; } 
    }
 
    private static string _ledColor = "off";

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new { color = _ledColor });
    }

    [HttpPost("setColor")]
    public IActionResult SetColor([FromBody] LedColorRequest req)
    {
        _ledColor = req.Color;
        return Ok(new { message = $"LED color set to {req.Color}" });
    }
}