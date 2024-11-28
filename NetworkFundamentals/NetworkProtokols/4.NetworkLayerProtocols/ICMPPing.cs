using System.Net.NetworkInformation;

namespace NetworkFundamentals.NetworkProtokols._4.NetworkLayerProtocols
{
    public class ICMPPing
    {
        public void Execute()
        {
            Console.WriteLine("ICMP Ping Testi'ne Hoş Geldiniz!");
            Console.WriteLine("Hedef IP adresini veya alan adını girin (örn: 8.8.8.8 veya google.com):");
            string target = Console.ReadLine();

            try
            {
                using (Ping ping = new Ping())
                {
                    Console.WriteLine($"'{target}' adresine ping gönderiliyor...");
                    PingReply reply = ping.Send(target);

                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"Ping başarılı! {target} adresine ulaşıldı.");
                        Console.WriteLine($"IP Adresi: {reply.Address}");
                        Console.WriteLine($"Gecikme: {reply.RoundtripTime} ms");
                        Console.WriteLine($"TTL (Time to Live): {reply.Options.Ttl}");
                    }
                    else
                    {
                        Console.WriteLine($"Ping başarısız: {reply.Status}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}