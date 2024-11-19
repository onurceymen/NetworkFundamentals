using System.Net.NetworkInformation;
using System.Text;
//"Traceroute = İnternetimin hedefe giderken yaptığı yolculuk"
public class Traceroute
{
    private readonly string _target;

    // Traceroute sınıfının kurucusu, hedef adresi alır

    public Traceroute(string target)
    {
        _target = target;
    }

    // Traceroute işlemini başlatan metot
    public void RunTraceroute()
    {
        // Traceroute ayarları
        const int maxHops = 30; // Maksimum 30 geçiş (hop) denenecek
        const int timeout = 3000; // Her deneme için zaman aşımı (3000 ms)
        const int bufferSize = 32; // ICMP paketi için veri boyutu (32 Byte)

        // ICMP paketini oluşturmak için bir veri tamponu
        byte[] buffer = Encoding.ASCII.GetBytes(new string('a', bufferSize));
        Ping pingSender = new Ping();

        Console.WriteLine($"Traceroute başlatılıyor: {_target}");
        Console.WriteLine("------------------------------------------------");

        // TTL (Time To Live) değeri 1'den başlayarak maksimum geçiş sayısına kadar artırılır
        for (int ttl = 1; ttl <= maxHops; ttl++)
        {
            // PingOptions ile TTL ayarlanır, bu sayede paketin kaç geçiş yapabileceği belirlenir
            PingOptions options = new PingOptions(ttl, true);

            // Ping isteği gönderilir
            try
            {
                PingReply reply = pingSender.Send(_target, timeout, buffer, options);

                if (reply == null)
                {
                    // Eğer ping cevabı alınamazsa, zaman aşımı mesajı yazdırılır
                    Console.WriteLine($"{ttl}	 * * * Zaman Aşımı");
                    continue;
                }

                // Geçiş (hop) bilgilerini yazdır
                Console.WriteLine($"{ttl}	 {reply.Address}	 Gecikme: {reply.RoundtripTime} ms");

                // Eğer hedefe ulaşıldıysa, işlem sonlandırılır
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Hedefe ulaşıldı.");
                    break;
                }
                // TTL süresi dolmuşsa ve geçiş noktası bilgisi alınabiliyorsa devam edilir
                else if (reply.Status != IPStatus.TtlExpired && reply.Status != IPStatus.TimedOut)
                {
                    // Eğer TTL dışında başka bir hata varsa işlem sonlandırılır
                    Console.WriteLine("Bir hata oluştu: " + reply.Status);
                    break;
                }
            }
            catch (PingException ex)
            {
                // Ping sırasında oluşabilecek hataları yakalar ve kullanıcıya bilgi verir
                Console.WriteLine($"{ttl}	 * * * Hata: {ex.Message}");
            }
        }
    }
}