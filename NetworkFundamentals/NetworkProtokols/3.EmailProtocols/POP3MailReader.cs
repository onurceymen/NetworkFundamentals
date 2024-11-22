using OpenPop.Mime;
using OpenPop.Pop3;

namespace NetworkFundamentals.NetworkProtokols._3.EmailProtocols
{
    public class POP3MailReader
    {
        public void Execute()
        {
            Console.WriteLine("POP3 Sunucu Adresini girin (örn: pop.gmail.com):");
            string pop3Server = Console.ReadLine();

            Console.WriteLine("POP3 Sunucu Portunu girin (örn: 995):");
            int pop3Port = int.Parse(Console.ReadLine());

            Console.WriteLine("E-posta adresinizi girin:");
            string email = Console.ReadLine();

            Console.WriteLine("E-posta şifrenizi girin:");
            string password = Console.ReadLine();

            try
            {
                Console.WriteLine("POP3 sunucusuna bağlanılıyor...");
                using (var client = new Pop3Client())
                {
                    client.Connect(pop3Server, pop3Port, true);
                    client.Authenticate(email, password);

                    Console.WriteLine("Bağlantı başarılı! Gelen mesajlar yükleniyor...");

                    int messageCount = client.GetMessageCount();
                    Console.WriteLine($"Toplam {messageCount} mesaj bulundu.");

                    for (int i = 1; i <= Math.Min(messageCount, 10); i++) 
                    {
                        Message message = client.GetMessage(i);
                        Console.WriteLine($"- {i}. Mesaj: {message.Headers.Subject} (Gönderen: {message.Headers.From})");
                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
