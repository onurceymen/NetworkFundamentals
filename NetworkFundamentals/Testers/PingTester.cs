using System.Net.NetworkInformation;

namespace NetworkFundamentals.Testers
{
    public class PingTester
    {
        private readonly string _host;
        private readonly int _packetSize;
        private readonly int _packetCount;

        public PingTester(string host, int packetSize, int packetCount)
        {
            _host = host;
            _packetSize = packetSize;
            _packetCount = packetCount;
        }

        public async Task RunPingTestAsync()
        {
            Ping ping = new Ping();

            int successfulPings = 0; // Başarılı ping sayısı
            long totalLatency = 0; // Toplam Geçikme süresi

            Console.WriteLine($"Ping test başlatıldı: {_host}");

            for (int i = 0; i < _packetCount; i++)
            {
                try
                {
                    //Ping gönderme (1000 ms timeout, kullanıcı tarafından belirtilen paket boyutu) 
                    PingReply reply = await ping.SendPingAsync(_host,1000,new byte[_packetSize]);
                    if (reply.Status == IPStatus.Success)
                    {
                        //Ping Başarılı olduğunda başarılı ping sayısını ve toplam gecikmeyi artır.
                        successfulPings++;
                        totalLatency += reply.RoundtripTime;
                        Console.WriteLine($"Ping {i + 1} : {reply.RoundtripTime} ms");
                    }
                    else
                    {
                        Console.WriteLine($"Ping {i + 1} Başarısız");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Ping {i + 1} hata: {ex.Message}");
                }
            }

            //Latency Hesaplama
            // Ortalama gecikme süresini hesapla (başarılı pingler varsa)
            long averageLatency = successfulPings > 0 ? totalLatency / successfulPings : 0;
            Console.WriteLine($"\n Ortalama Gecikme (Latency) : {averageLatency} ms ");

            //Paket Kaybı Hesaplama
            double packetLoss = ((double)(_packetCount - successfulPings) / _packetCount) * 100;
            Console.WriteLine($"Paket Kaybı: %{packetLoss}");

            // Throughput örneği
            // Throughput hesaplama (başarılı pinglerle gönderilen toplam veri boyutu ve toplam gecikme süresi kullanılarak)
            long dataSize = _packetSize * successfulPings;
            double throughput = dataSize / (double)(totalLatency / 1000.0);
            Console.WriteLine($"Tahmini Throughput: {throughput / 1024.0} KB/s");

        }
    }
}
