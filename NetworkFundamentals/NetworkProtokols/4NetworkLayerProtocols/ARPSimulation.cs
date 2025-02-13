namespace NetworkFundamentals.NetworkProtokols._4.NetworkLayerProtocols
{
    public class ARPSimulation
    {
        private Dictionary<string, string> arpTable = new Dictionary<string, string>();

        // Yerel ağda var olan IP-MAC eşleşmeleri
        private Dictionary<string, string> networkDevices = new Dictionary<string, string>
        {
            { "192.168.1.1", "AA:BB:CC:DD:EE:01" },
            { "192.168.1.2", "AA:BB:CC:DD:EE:02" },
            { "192.168.1.3", "AA:BB:CC:DD:EE:03" }
        };

        public void Start()
        {
            Console.WriteLine("ARP Simülasyonuna Hoş Geldiniz!");
            Console.WriteLine("Hedef IP adresini girin (örn: 192.168.1.2):");
            string targetIP = Console.ReadLine();

            // ARP tablosunda kontrol
            if (arpTable.ContainsKey(targetIP))
            {
                Console.WriteLine($"ARP Tablosu: {targetIP} adresinin MAC adresi {arpTable[targetIP]}.");
            }
            else
            {
                Console.WriteLine($"ARP isteği gönderiliyor: '{targetIP}' adresinin MAC adresi nedir?");
                if (networkDevices.TryGetValue(targetIP, out string macAddress))
                {
                    Console.WriteLine($"ARP Yanıtı: {targetIP} adresinin MAC adresi {macAddress}.");
                    Console.WriteLine("Bilgi ARP tablosuna kaydediliyor...");
                    arpTable[targetIP] = macAddress;
                }
                else
                {
                    Console.WriteLine($"Hata: '{targetIP}' adresi yerel ağda bulunamadı.");
                }
            }
        }
    }
}
