using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._5.TransportLayerProtocols
{
    public class UDPChatServer
    {
        private List<IPEndPoint> clients = new List<IPEndPoint>();

        public void Start()
        {
            Console.WriteLine("UDP Sohbet Sunucusu başlatılıyor...");
            UdpClient udpServer = new UdpClient(8080);

            try
            {
                Console.WriteLine("Sunucu Mesaj Bekliyor...");
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpServer.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                    Console.WriteLine($"[{remoteEndPoint.Address}] Gelen Mesaj : {receivedMessage}");

                    //Yeni bir istemci
                    if (!clients.Contains(remoteEndPoint))
                    {
                        clients.Add(remoteEndPoint);
                        Console.WriteLine($"Yeni istemci eklendi: {remoteEndPoint.Address}");

                    }

                    //Mesajı Diğer Tüm istemcilerde Yayınla
                    foreach (var client in clients)
                    {
                        if (!client.Equals(remoteEndPoint))
                        {
                            byte[] messageBytes = Encoding.UTF8.GetBytes(receivedMessage);
                            udpServer.Send(messageBytes, messageBytes.Length, client);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu {ex.Message}");
            }
            finally
            {
                udpServer.Close();
            }
        }

    }
}
