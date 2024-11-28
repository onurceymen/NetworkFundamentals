namespace NetworkFundamentals.NetworkProtokols._4.NetworkLayerProtocols
{
    public class RARPSimulation
    {
        // RARP tablosu (MAC adresi -> IP adresi eşlemesi)
        private Dictionary<string, string> rarpTable = new Dictionary<string, string>
        {
            { "AA:BB:CC:DD:EE:01", "192.168.1.1" },
            { "AA:BB:CC:DD:EE:02", "192.168.1.2" },
            { "AA:BB:CC:DD:EE:03", "192.168.1.3" }
        };

        public void Start()
        {
            Console.WriteLine("RARP Simülasyonuna Hoş Geldiniz!");
            Console.WriteLine("Cihazın MAC adresini girin (örn: AA:BB:CC:DD:EE:02):");
            string macAddress = Console.ReadLine();

            Console.WriteLine($"RARP isteği gönderiliyor: '{macAddress}' adresinin IP adresi nedir?");
            if (rarpTable.TryGetValue(macAddress, out string ipAddress))
            {
                Console.WriteLine($"RARP Yanıtı: {macAddress} adresinin IP adresi {ipAddress}.");
            }
            else
            {
                Console.WriteLine($"Hata: '{macAddress}' adresi için bir IP adresi bulunamadı.");
            }
        }
    }
}
