using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPServerWithLoss
    {
        public void Start()
        {
            Console.WriteLine("UDP Sunucusu (Paket Kaybı Simülasyonu) başlatılıyor...");
            UdpClient udpServer = new UdpClient(8080); // Sunucu 8080 portunu dinliyor
            Random random = new Random();

            try
            {
                Console.WriteLine("Sunucu mesaj bekliyor...");
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpServer.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                    // Paket kaybı simülasyonu
                    if (random.Next(0, 100) < 30) // %30 kayıp ihtimali
                    {
                        Console.WriteLine($"Paket kayboldu! (Mesaj: {receivedMessage})");
                        continue;
                    }

                    Console.WriteLine($"[{remoteEndPoint.Address}] Gelen mesaj: {receivedMessage}");

                    // Yanıt gönderme
                    string response = "Paket alındı!";
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
