using System.Net.NetworkInformation;

namespace NetworkFundamentals.Testers
{
    public class TtlTester
    {
        private readonly string _target;

        public TtlTester(string target)
        {
            _target = target;
        }

        public void CheckTtl()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions(1, true); // TTL değeri 1 olarak ayarlanır
            PingReply reply = pingSender.Send(_target, 1000, new byte[32], options);

            if (reply != null && reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"TTL değeri: {options.Ttl}, Hedef Adres: {reply.Address}");
            }
            else
            {
                Console.WriteLine("Ping başarısız veya hedefe ulaşılamadı.");
            }
        }
    }
}
