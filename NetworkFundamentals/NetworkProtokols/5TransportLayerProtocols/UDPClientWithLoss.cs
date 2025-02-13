using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPClientWithLoss
    {
        public void Start()
        {
            Console.WriteLine("UDP İstemcisi (Paket Gönderimi) başlatılıyor...");
            UdpClient udpClient = new UdpClient();

            Console.WriteLine("Sunucu IP adresini girin (örn: 127.0.0.1):");
            string serverIP = Console.ReadLine();

            try
            {
                for (int i = 1; i <= 10; i++) // 10 paket gönderimi
                {
                    string message = $"Paket {i}";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    udpClient.Send(messageBytes, messageBytes.Length, serverIP, 8080);
                    Console.WriteLine($"Mesaj gönderildi: {message}");

                    // Sunucudan yanıt bekleme
                    try
                    {
                        udpClient.Client.ReceiveTimeout = 2000; // 2 saniye zaman aşımı
                        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] responseBytes = udpClient.Receive(ref remoteEndPoint);
                        string response = Encoding.UTF8.GetString(responseBytes);
                        Console.WriteLine($"Sunucudan gelen yanıt: {response}");
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine($"Paket {i} için yanıt alınamadı (Muhtemelen kayboldu).");
                    }
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
