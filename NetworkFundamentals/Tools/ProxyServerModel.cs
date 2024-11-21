using System.Net;

namespace NetworkFundamentals.Tools
{
    public class ProxyServerModel
    {
        private readonly int _port;
        private readonly HttpListener _listener;

        public ProxyServerModel(int port)
        {
            _port = port;
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{_port}/");
        }

        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine($"Proxy sunucusu çalışıyor: http://localhost:{_port}");

            while (true)
            {
                var context = await _listener.GetContextAsync();
                _ = Task.Run(() => HandleRequest(context));
            }
        }
        private async Task HandleRequest(HttpListenerContext context)
        {
            //İstemcinin Yönlendirilmesini istediği hedef URL'yi al
            string targetUrl = context.Request.RawUrl;
            Console.WriteLine($"Proxy ile yönlendirilen istek: {targetUrl}");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Hedef URL'ye istek Gömder (Proxy'Den geçiyor)
                    var responseMessage = await client.GetAsync("https://jsonplaceholder.typicode.com" + targetUrl);

                    //Gelen Yanıtı İstemciye ilet 
                    context.Response.StatusCode = (int)responseMessage.StatusCode;
                    context.Response.ContentType = responseMessage.Content.Headers.ContentType.ToString();

                    using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        responseStream.CopyTo(context.Response.OutputStream);
                    }
                    context.Response.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Hata oluştu: " + ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Close();
                }
            }
        }

    }
}
//var proxyServer = new ProxyServer(8888);
//await proxyServer.StartAsync();