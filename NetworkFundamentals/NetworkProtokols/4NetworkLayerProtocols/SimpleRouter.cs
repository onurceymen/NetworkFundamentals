namespace NetworkFundamentals.NetworkProtokols._4.NetworkLayerProtocols
{
    public class SimpleRouter
    {
        private Dictionary<string, string> routingTable = new Dictionary<string, string>();

        public SimpleRouter()
        {
            // Yönlendirme tablosunu tanımla
            routingTable.Add("192.168.1.0", "RouterA");
            routingTable.Add("192.168.2.0", "RouterB");
            routingTable.Add("192.168.3.0", "RouterC");
        }

        // Veriyi yönlendirme işlemi
        public void RoutePacket(string sourceIP, string destinationIP)
        {
            Console.WriteLine($"Kaynak IP: {sourceIP}");
            Console.WriteLine($"Hedef IP: {destinationIP}");
            Console.WriteLine("Yönlendirme başlatılıyor...");

            string sourceNetwork = GetNetworkAddress(sourceIP);
            string destinationNetwork = GetNetworkAddress(destinationIP);

            if (sourceIP == destinationIP)
            {
                Console.WriteLine($"Veri doğrudan {destinationIP} adresine iletildi.");
            }
            else
            {
                if (routingTable.TryGetValue(destinationNetwork, out string nextHop))
                {
                    Console.WriteLine($"Veri {nextHop} üzerinden {destinationIP} adresine yönlendiriliyor.");
                }
                else
                {
                    Console.WriteLine($"Hedef ağ {destinationNetwork} bilinmiyor. Yönlendirme başarısız.");
                }
            }

        }

        private string GetNetworkAddress(string ipAddress)
        {
            string[] octets = ipAddress.Split(',');
            return $"{octets[0]},{octets[1]},{octets[2]},0";
        }
    }
}
