using System.Net;
using System.Net.Mail;

namespace NetworkFundamentals.NetworkProtokols._3.EmailProtocols
{
    public class SMTPMailSenderLocal
    {
        public void Execute()
        {
            Console.WriteLine("SMTP Sunucu Adresini girin (örn: localhost):");
            string smtpServer = Console.ReadLine();

            Console.WriteLine("SMTP Sunucu Portunu girin (örn: 25):");
            int smtpPort = int.Parse(Console.ReadLine());

            Console.WriteLine("Gönderen e-posta adresini girin (ör: sender@localhost):");
            string senderEmail = Console.ReadLine();

            Console.WriteLine("Alıcının e-posta adresini girin:");
            string receiverEmail = Console.ReadLine();

            Console.WriteLine("E-posta Konusunu girin:");
            string subject = Console.ReadLine();

            Console.WriteLine("E-posta mesajını girin:");
            string messageBody = Console.ReadLine();

            try
            {
                Console.WriteLine("E-posta gönderiliyor...");

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = CredentialCache.DefaultNetworkCredentials,
                    EnableSsl = false // Yerel testlerde SSL gerekmez
                };

                MailMessage mail = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = messageBody
                };

                smtpClient.Send(mail);

                Console.WriteLine("E-posta başarıyla gönderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
