using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class TCPServer
    {
        public void Start()
        {
            Console.WriteLine("TCP İstemcisi başlatılıyor...");
            Console.WriteLine("Sunucu IP adresini girin (örn: 127.0.0.1):");
            string serverIP = Console.ReadLine();

            try
            {
                TcpClient client = new TcpClient(serverIP, 8080);
                Console.WriteLine("Sunucuya bağlandı!");

                NetworkStream stream = client.GetStream();
                Console.WriteLine("Sunucuya gönderilecek mesajı yazın:");
                string message = Console.ReadLine();

                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Mesaj gönderildi.");

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Sunucudan gelen yanıt: {response}");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
