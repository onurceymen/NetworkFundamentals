using NetworkFundamentals.NetworkProtokols._6.AddressingandNameResolutionProtocols;
using NetworkFundamentals.NetworkProtokols._7.SecurityProtocols;

namespace NetworkFundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Network Protokolleri Uygulamasına Hoş Geldiniz =====");
                Console.WriteLine("Lütfen bir kategori seçin:");
                Console.WriteLine("1. HTTP Protokolleri");
                Console.WriteLine("2. FTP Protokolleri");
                Console.WriteLine("3. E-Posta Protokolleri");
                Console.WriteLine("4. Ağ Katmanı Protokolleri");
                Console.WriteLine("5. Taşıma Katmanı Protokolleri");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HttpProtocolsMenu();
                        break;
                    case "2":
                        FtpProtocolsMenu();
                        break;
                    case "3":
                        EmailProtocolsMenu();
                        break;
                    case "4":
                        NetworkLayerProtocolsMenu();
                        break;
                    case "5":
                        TransportLayerProtocolsMenu();
                        break;
                    case "6":
                        AddressingAndNameResolutionMenu();
                        break;
                    case "7":
                        SecurityProtocolsMenu();
                        break;
                    case "0":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }

                Console.WriteLine("Devam etmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }

        static void HttpProtocolsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== HTTP Protokolleri =====");
                Console.WriteLine("1. HTTP Protokol Handler");
                Console.WriteLine("2. WebSocket Server");
                Console.WriteLine("3. WebSocket Server Two");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var httpHandler = new NetworkProtokols.HttpProtokols.HttpProtocolHandler();
                        httpHandler.Execute().Wait();
                        break;
                    case "2":
                        var wsServer = new NetworkProtokols.HttpProtokols.WebSocketServer("http://localhost:5000/ws/");
                        wsServer.StartAsync().Wait();
                        break;
                    case "3":
                        var wsServerTwo = new NetworkProtokols.HttpProtokols.WebSocketServerTwo("http://localhost:5000/ws/");
                        wsServerTwo.StartAsync().Wait();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void FtpProtocolsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== FTP Protokolleri =====");
                Console.WriteLine("1. FTP Dosya Listeleme");
                Console.WriteLine("2. FTP Dosya İndirme");
                Console.WriteLine("3. FTP Dosya Yükleme");
                Console.WriteLine("4. FTP Dosya Silme");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var ftpListFiles = new NetworkProtokols.FtpProtokols.FTPListFilesHandler();
                        ftpListFiles.Execute();
                        break;
                    case "2":
                        var ftpDownload = new NetworkProtokols.FtpProtokols.FTPDownloadHandler();
                        ftpDownload.Execute();
                        break;
                    case "3":
                        var ftpUpload = new NetworkProtokols.FtpProtokols.FTPUploadHandler();
                        ftpUpload.Execute();
                        break;
                    case "4":
                        var ftpDelete = new NetworkProtokols.FtpProtokols.FTPDeleteHandler();
                        ftpDelete.Execute();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void EmailProtocolsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== E-Posta Protokolleri =====");
                Console.WriteLine("1. SMTP Mail Gönder");
                Console.WriteLine("2. POP3 Mail Okuma");
                Console.WriteLine("3. IMAP Mail Okuma");
                Console.WriteLine("4. IMAP Mail Okuma (Gelişmiş)");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var smtpSender = new NetworkProtokols._3.EmailProtocols.SMTPMailSender();
                        smtpSender.Execute();
                        break;
                    case "2":
                        var pop3Reader = new NetworkProtokols._3.EmailProtocols.POP3MailReader();
                        pop3Reader.Execute();
                        break;
                    case "3":
                        var imapReader = new NetworkProtokols._3.EmailProtocols.IMAPMailReader();
                        imapReader.Execute();
                        break;
                    case "4":
                        var imapReaderV2 = new NetworkProtokols._3.EmailProtocols.IMAPMailReaderV2();
                        imapReaderV2.Execute();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void NetworkLayerProtocolsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Ağ Katmanı Protokolleri =====");
                Console.WriteLine("1. ARP Simülasyonu");
                Console.WriteLine("2. RARP Simülasyonu");
                Console.WriteLine("3. ICMP Ping");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var arpSim = new NetworkProtokols._4.NetworkLayerProtocols.ARPSimulation();
                        arpSim.Start();
                        break;
                    case "2":
                        var rarpSim = new NetworkProtokols._4.NetworkLayerProtocols.RARPSimulation();
                        rarpSim.Start();
                        break;
                    case "3":
                        var icmpPing = new NetworkProtokols._4.NetworkLayerProtocols.ICMPPing();
                        icmpPing.Execute();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void TransportLayerProtocolsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Taşıma Katmanı Protokolleri =====");
                Console.WriteLine("1. TCP Sunucu");
                Console.WriteLine("2. TCP İstemci");
                Console.WriteLine("3. UDP Sunucu");
                Console.WriteLine("4. UDP İstemci");
                Console.WriteLine("5. UDP Chat Sunucu");
                Console.WriteLine("6. UDP Chat İstemci");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var tcpServer = new NetworkProtokols._5.TransportLayerProtocols.TCPServer();
                        tcpServer.Start();
                        break;
                    case "2":
                        var tcpClient = new NetworkProtokols._5.TransportLayerProtocols.TCPClient();
                        tcpClient.Start();
                        break;
                    case "3":
                        var udpServer = new NetworkProtokols._5.TransportLayerProtocols.UDPServer();
                        udpServer.Start();
                        break;
                    case "4":
                        var udpClient = new NetworkProtokols._5.TransportLayerProtocols.UDPClient();
                        udpClient.Start();
                        break;
                    case "5":
                        var udpChatServer = new NetworkProtokols._5.TransportLayerProtocols.UDPChatServer();
                        udpChatServer.Start();
                        break;
                    case "6":
                        var udpChatClient = new NetworkProtokols._5.TransportLayerProtocols.UDPChatClient();
                        udpChatClient.Start();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void AddressingAndNameResolutionMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Adresleme ve Ad Çözümleme Protokolleri =====");
                Console.WriteLine("1. DNS Çözümleyici");
                Console.WriteLine("2. DHCP Simülatörü");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var dnsResolver = new DNSResolver();
                        dnsResolver.Start();
                        break;
                    case "2":
                        var dhcpSimulator = new DHCPSimulator();
                        dhcpSimulator.Start();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
        static void SecurityProtocolsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Güvenlik Protokolleri =====");
                Console.WriteLine("1. IPsec Simülasyonu");
                Console.WriteLine("0. Geri");
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var ipsecSimulation = new IPsecSimulation();
                        ipsecSimulation.Start();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
    }
}
