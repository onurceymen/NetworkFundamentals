namespace NetworkFundamentals.NetworkProtokols._6.AddressingandNameResolutionProtocols
{
    public class DHCPSimulator
    {
        private readonly Queue<string> ipPool;
        private readonly string gateway;
        private readonly string dnsServer;

        public DHCPSimulator()
        {
            // IP adres havuzu oluştur
            ipPool = new Queue<string>(new[]
            {
                "192.168.1.100",
                "192.168.1.101",
                "192.168.1.102",
                "192.168.1.103",
                "192.168.1.104"
            });

            // Ağ geçidi ve DNS sunucusu
            gateway = "192.168.1.1";
            dnsServer = "8.8.8.8";
        }

        public void Start()
        {
            Console.WriteLine("DHCP Simülatörüne Hoş Geldiniz!");
            Console.WriteLine("Yeni bir cihaz bağlandı. IP adresi tahsis ediliyor...");

            if (ipPool.Count > 0)
            {
                string assignedIP = ipPool.Dequeue();
                Console.WriteLine($"Tahsis Edilen IP: {assignedIP}");
                Console.WriteLine($"Ağ Geçidi: {gateway}");
                Console.WriteLine($"DNS Sunucusu: {dnsServer}");
            }
            else
            {
                Console.WriteLine("Hata: IP havuzu dolu. Yeni cihazlara IP adresi atanamıyor!");
            }
        }

    }
}
