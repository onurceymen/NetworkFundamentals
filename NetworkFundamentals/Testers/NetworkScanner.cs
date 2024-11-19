using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFundamentals.Testers
{
    public class NetworkScanner
    {
        private readonly string _ipBase;

        // NetworkScanner sınıfının kurucusu, bağlı olunan ağın IP adresini otomatik olarak alır
        public NetworkScanner()
        {
            _ipBase = GetLocalIPAddressBase();
        }

        // Bağlı olunan yerel ağın IP adresinin temel kısmını döndürür (örn: "192.168.1")
        private string GetLocalIPAddressBase()
        {
            string localIP = string.Empty;
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(localIP))
            {
                throw new Exception("Yerel IP adresi bulunamadı!");
            }

            // IP adresinin son oktetini kesip IP temelini döndürüyoruz (örn: "192.168.1")
            var ipParts = localIP.Split('.');
            return $"{ipParts[0]}.{ipParts[1]}.{ipParts[2]}";
        }

        // Ağ taramasını başlatan metot
        public void StartScan()
        {
            // 1'den 254'e kadar IP adreslerinin son oktetini döngü ile oluşturuyoruz
            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{_ipBase}.{i}"; // IP adresini oluşturuyoruz (örn: "192.168.1.1", "192.168.1.2" ...)
                Ping ping = new Ping();

                // PingCompleted olayını tetikleyerek cihazların yanıt verip vermediğini kontrol ediyoruz
                ping.PingCompleted += (s, e) =>
                {
                    if (e.Reply != null && e.Reply.Status == IPStatus.Success)
                    {
                        // Eğer ping başarılıysa cihaz bulundu mesajı yazdırılır
                        Console.WriteLine($"Cihaz bulundu: {e.Reply.Address}");
                    }
                };

                // Asenkron olarak Ping gönderiyoruz, böylece program beklemeden devam ediyor
                ping.SendAsync(ip, 100);
            }
        }
    }

}
