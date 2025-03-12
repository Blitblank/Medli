
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LedController : ControllerBase
{
    private static string _ledColor = "off";

    public class Request
    {
        public string Color { get; set; }
    }

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new { color = _ledColor });
    }

    [HttpPost("setColor")]
    public IActionResult SetColor([FromBody] Request req)
    {
        _ledColor = req.Color;
        return Ok(new { message = $"LED color set to {req.Color}" });
    }
}