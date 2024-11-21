using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols.HttpProtokols
{
    public class WebSocketServer
    {
        private readonly HttpListener _listener;

        public WebSocketServer(string url)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("WebSocket server is running on ws://localhost:5000/ws/");

            while (true)
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;

                    Console.WriteLine("Client connected");
                    await SendMessages(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        private static async Task SendMessages(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                string message = "Serverden Anlık Mesaj:" + DateTime.Now.ToString();
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                // Mesajı WebSocket aracılığıyla gönderiyoruz.
                await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

                Console.WriteLine("Mesaj gönderildi: " + message);

                // 5 saniyede bir mesaj gönderelim
                await Task.Delay(5000);
            }
        }


    }
}
