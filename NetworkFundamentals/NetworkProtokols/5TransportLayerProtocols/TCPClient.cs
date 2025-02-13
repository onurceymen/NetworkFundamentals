using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class TCPClient
    {
        public void Start()
        {
            Console.WriteLine("TCP Sunucusu başlatılıyor...");
            TcpListener server = new TcpListener(System.Net.IPAddress.Any, 8080);

            try
            {
                server.Start();
                Console.WriteLine("Sunucu 8080 portunda dinleniyor..");
                while (true)
                {
                    Console.WriteLine("Bir istemci bağlantısı bekleniyor...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Bir İstemci bağlandı.!");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"İstemciden gelen mesaj; {message}");

                    string response = "Mesaj alındı";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    Console.WriteLine("Yanıt Gönderildi");

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
