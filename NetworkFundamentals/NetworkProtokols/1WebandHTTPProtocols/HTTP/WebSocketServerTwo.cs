using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols.HttpProtokols
{
    public class WebSocketServerTwo
    {
        private readonly HttpListener _listener;
        private readonly List<WebSocket> _clients;

        public WebSocketServerTwo(string url)
        {
            _listener = new HttpListener();
            _clients = new List<WebSocket>();
            _listener.Prefixes.Add(url);
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("WebSocket Serveri Çalışmaya başladı" + _listener.Prefixes.First());

            //Konsoldan mesaj alıp istemcilere gönderen işlem başlatılıyor.
            Task.Run(() => ReadAndBroadcastMessagesFromConsole());

            while (true)
            {
                HttpListenerContext context = await _listener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;

                    _clients.Add(webSocket);
                    Console.WriteLine("İstemciye Bağlandı");

                    //İstemciden gelen mesajları dinleyip diğer istemcilere yayar
                    await ReceiveAndBroadcastMessages(webSocket);

                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }


        }

        private async Task ReadAndBroadcastMessagesFromConsole()
        {
            while (true)
            {
                string consoleMessage = Console.ReadLine();
                await BroadcastMessage(consoleMessage);
            }
        }

        private async Task ReceiveAndBroadcastMessages(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine("Alınan Mesaj" + receivedMessage);

                await BroadcastMessage(receivedMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    _clients.Remove(webSocket);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the server", CancellationToken.None);
                    Console.WriteLine("Client Disconnected.");
                    break;
                }

            }
        }

        private async Task BroadcastMessage(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);

            foreach (var client in _clients.ToList())
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }

        }

    }
}
