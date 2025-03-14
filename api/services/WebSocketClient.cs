using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketClient
{
    private static readonly Uri WebSocketUri = new Uri("ws://192.168.1.140/ws");

    public static float temperature = 0;

    public static async Task SendCommand(string command)
    {
        using (ClientWebSocket ws = new ClientWebSocket())
        {
            try {
                await ws.ConnectAsync(WebSocketUri, CancellationToken.None);
                Console.WriteLine("Connected to ESP32 WebSocket");

                byte[] sendBuffer = Encoding.UTF8.GetBytes(command);
                await ws.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"Sent Command: {command}");

                byte[] receiveBuffer = new byte[1024];
                WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                string response = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                Console.WriteLine($"Received Response: {response}");

                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
            } catch (WebSocketException ex) {
                Console.WriteLine($"WebSocket connect error: {ex.Message}");
            }

        }
    }

    public static async Task ConnectAndReceiveTemperature()
    {
        try {
            using (ClientWebSocket ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(WebSocketUri, CancellationToken.None);
                Console.WriteLine("Connected to ESP32 WebSocket");

                // Request temperature data
                byte[] requestBuffer = Encoding.UTF8.GetBytes("get_temp");
                await ws.SendAsync(new ArraySegment<byte>(requestBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

                while (ws.State == WebSocketState.Open)
                {
                    byte[] receiveBuffer = new byte[1024];

                    try {
                        WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                        string message = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                        Console.WriteLine($"Received: {message}");
                        string pattern = @"[^0123456789.]";
                        Regex regex = new Regex(pattern);
                        string newMessage = regex.Replace(message, "");
                        if(newMessage.Length >= 1) {
                            temperature = float.Parse(newMessage);
                        }
                    } catch (WebSocketException ex) {
                        Console.WriteLine($"WebSocket receive error: {ex.Message}");
                    }
                }
            }
        } catch (WebSocketException ex) {
            Console.WriteLine($"WebSocket connection error: {ex.Message}");
        } finally {
            Console.WriteLine("WebSocket disconnected. Attempting to reconnect...");
            await Task.Delay(5000);  // Wait before retrying
            await ConnectAndReceiveTemperature();  // Retry connection
        }
        
    }
}