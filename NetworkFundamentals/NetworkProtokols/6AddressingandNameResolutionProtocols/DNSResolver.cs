using System.Net;

namespace NetworkFundamentals.NetworkProtokols._6.AddressingandNameResolutionProtocols
{
    public class DNSResolver
    {
        public void Start()
        {
            Console.WriteLine("DNS Çözümleyiciye Hoş Geldiniz!");
            Console.WriteLine("1. Alan adını IP adresine çevir");
            Console.WriteLine("2. IP adresini alan adına çevir");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ResolveDomainToIP();
                    break;
                case "2":
                    ResolveIPToDomain();
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }
        }

        private void ResolveDomainToIP()
        {
            Console.Write("Çözümlemek istediğiniz alan adını girin (örn: google.com): ");
            string domainName = Console.ReadLine();

            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(domainName);
                Console.WriteLine($"'{domainName}' için IP adres(ler)i:");
                foreach (var ip in hostEntry.AddressList)
                {
                    Console.WriteLine($"- {ip}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }

        private void ResolveIPToDomain()
        {
            Console.Write("Ters çözümlemek istediğiniz IP adresini girin (örn: 8.8.8.8): ");
            string ipAddress = Console.ReadLine();

            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
                Console.WriteLine($"'{ipAddress}' için alan adı: {hostEntry.HostName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }

    }
}
