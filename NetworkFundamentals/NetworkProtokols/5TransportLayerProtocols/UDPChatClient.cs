using Renci.SshNet;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPChatClient
    {
        public void Start()
        {
            Console.WriteLine("UDP Sohbet İstemcisi başlatılıyor...");
            UdpClient udpclient = new UdpClient();

            Console.WriteLine("Sunucu IP adresini girin (örn: 127.0.0.1):");
            string serverIP = Console.ReadLine();

            //Sunucuya Bağlan
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), 8080);

            //Gelen Mesajları dinlemek için ayrı bir iş parcacığı oluştur.
            Thread ListenerThread = new Thread(() => ListenForMessages(udpclient));
            ListenerThread.Start();

            try
            {
                Console.WriteLine("Mesaj göndermek için yazın. Çıkış yapmak için 'exit' yazın.");

                while (true)
                {
                    string message = Console.ReadLine();

                    if (message.ToLower() == "Exit")
                    {
                        Console.WriteLine("Sohbetten çıkılıyor...");
                        break;
                    }

                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    udpclient.Send(messageBytes,messageBytes.Length,serverEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                udpclient.Close();
            }
        }

        private void ListenForMessages(UdpClient udpClient)
        {
            try
            {
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                    Console.WriteLine($"[Sunucudan gelen mesaj]: {receivedMessage}");

                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Dinleme sırasında hata oluştu: {ex.Message}");
            }
        }
    }
}
