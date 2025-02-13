using MailKit;
using MailKit.Net.Imap;

namespace NetworkFundamentals.NetworkProtokols._3.EmailProtocols
{
    public class IMAPMailReaderV2
    {
        public void Execute()
        {
        Start:
            Console.WriteLine("IMAP Sunucu Adresini girin (örn: imap.gmail.com):");
            string imapServer = Console.ReadLine();

            Console.WriteLine("IMAP Sunucu Portunu girin (örn: 993):");
            int imapPort = int.Parse(Console.ReadLine());

            Console.WriteLine("E-posta adresinizi girin:");
            string email = Console.ReadLine();

            Console.WriteLine("E-posta şifrenizi girin:");
            string password = Console.ReadLine();

            try
            {
                Console.WriteLine("IMAP sunucusuna bağlanılıyor...");
                using (var client = new ImapClient())
                {
                    client.Connect(imapServer, imapPort, true);

                    client.Authenticate(email, password);

                    if (client.IsAuthenticated)
                    {
                        Console.WriteLine("Bağlantı Başarılı.! Gelen Kutusu yükleniyor...");
                    }
                    else
                    {
                        Console.WriteLine("Bağlantı Başarısız.!");
                        goto Start;
                    }

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    Console.WriteLine($"Gelen Kutusu: {inbox.Count} mesaj bulundu.");
                    for (int i = 0; i < Math.Min(inbox.Count,10); i++)
                    {
                        var message = inbox.GetMessage(i);
                        Console.WriteLine($"- {i + 1}. Mesaj: {message.Subject} (Gönderen: {message.From})");
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
