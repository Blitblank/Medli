
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
    public async Task<IActionResult> SetColor([FromBody] LedColorRequest req)
    {
        _ledColor = req.Color;
        await WebSocketClient.SendCommand(_ledColor);
        return Ok(new { message = $"LED color set to {_ledColor}" });
    }

    [HttpGet("startTemperature")]
    public async Task<IActionResult> StartTemperature()
    {
        await WebSocketClient.ConnectAndReceiveTemperature();
        return Ok(new { message = "Temperature request sent to ESP32" });
    }

    [HttpGet("temperature")]
    public IActionResult GetTemperature()
    {
        return Ok(new { temperature = WebSocketClient.temperature });
    }
}