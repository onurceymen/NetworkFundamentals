using System.Net.NetworkInformation;

namespace NetworkFundamentals.Testers
{
    public class IcmpFloodTest
    {
        private readonly string _target;
        private readonly int _delay;

        // ICMP Flood Test sınıfının kurucusu, hedef adres ve gecikme süresi alır
        public IcmpFloodTest(string target, int delay = 50)
        {
            _target = target;
            _delay = delay; // Test aralıklarını belirleyen gecikme süresi (milisaniye cinsinden)
        }
        public void StartFlood()
        {
            Ping pingsender = new Ping();
            int packetCount = 0;
            

            while (true)
            {
                try
                {
                    PingReply reply = pingsender.Send(_target);
                    packetCount++;
                    if (reply.Status == IPStatus.Success)
                    {
                        Console.WriteLine($"Ping {reply.Address}, Gecikme: {reply.RoundtripTime} ms, Paket No: {packetCount}");

                    }
                    else
                    {
                        Console.WriteLine($"Ping başarısız: {reply.Status}, Paket No: {packetCount}");

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");

                }
                Thread.Sleep(_delay); // Testin yoğunluğunu azaltmak için kısa bir bekleme süresi

            }
        }

    }
}
