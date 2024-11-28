using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPClient
    {
        public void Start()
        {
            Console.WriteLine("UDP İstemcisi başlatılıyor...");
            UdpClient udpClient = new UdpClient();

            try
            {
                Console.WriteLine("Sunucu IP adresini girin (örn: 127.0.0.1):");
                string serverIP = Console.ReadLine();

                Console.WriteLine("Gönderilecek mesajı yazın:");
                string message = Console.ReadLine();

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                udpClient.Send(messageBytes, messageBytes.Length, serverIP, 8080); // Mesaj gönderimi
                Console.WriteLine("Mesaj gönderildi.");

                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] responseBytes = udpClient.Receive(ref remoteEndPoint);
                string response = Encoding.UTF8.GetString(responseBytes);
                Console.WriteLine($"Sunucudan gelen yanıt: {response}");
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
