using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPMulticastServer
    {
        public void Start()
        {
            Console.WriteLine("UDP Multicast Sunucusu başlatılıyor...");
            UdpClient udpServer = new UdpClient();

            try
            {
                IPAddress multicastAddress = IPAddress.Parse("224.0.0.1");
                IPEndPoint endPoint = new IPEndPoint(multicastAddress, 8080);

                int messageCount = 1;
                while (true)
                {
                    string message = $"Multicast Mesaj {messageCount}";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    udpServer.Send(messageBytes, messageBytes.Length, endPoint);

                    Console.WriteLine($"Mesaj yayınlandı: {message}");
                    messageCount++;

                    Thread.Sleep(2000); // Her 2 saniyede bir mesaj yayınla
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
