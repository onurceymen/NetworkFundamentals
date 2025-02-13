using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPMulticastClient
    {
        public void Start()
        {
            Console.WriteLine("UDP Multicast İstemcisi başlatılıyor...");
            UdpClient udpClient = new UdpClient(8080);

            try
            {
                IPAddress multicastAddress = IPAddress.Parse("224.0.0.1");
                udpClient.JoinMulticastGroup(multicastAddress);

                Console.WriteLine("Multicast grubuna katıldınız. Mesaj bekleniyor...");
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                    Console.WriteLine($"Multicast mesaj alındı: {receivedMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}
