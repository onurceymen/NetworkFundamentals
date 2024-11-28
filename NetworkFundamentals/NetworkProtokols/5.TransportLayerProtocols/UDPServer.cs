using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPServer
    {
        public void Start()
        {
            Console.WriteLine("UDP Sunucusu başlatılıyor...");
            UdpClient udpServer = new UdpClient(8080); // 8080 portunda dinliyor

            try
            {
                Console.WriteLine("Sunucu mesaj bekliyor...");
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpServer.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);
                    Console.WriteLine($"[{remoteEndPoint.Address}] Gelen mesaj: {receivedMessage}");

                    // Yanıt gönderme
                    string response = "Mesaj alındı!";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    udpServer.Send(responseBytes, responseBytes.Length, remoteEndPoint);
                    Console.WriteLine("Yanıt gönderildi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                udpServer.Close();
            }
        }
    }
}
