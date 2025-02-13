using System.Net;
using System.Net.Mail;

namespace NetworkFundamentals.NetworkProtokols._3.EmailProtocols
{
    public class SMTPMailSender
    {
        public void Execute()
        {
            Console.WriteLine("SMTP Sunucu Adresini girin (örn: smtp.gmail.com):");
            string smtpServer = Console.ReadLine();

            Console.WriteLine("SMTP Sunucu Portunu girin (örn: 587):");
            int smtpPort = int.Parse(Console.ReadLine());

            Console.WriteLine("E-posta adresinizi girin:");
            string senderEmail = Console.ReadLine();

            Console.WriteLine("E-posta şifrenizi girin:");
            string password = Console.ReadLine();

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
                    Credentials = new NetworkCredential(senderEmail, password)
                };

                MailMessage mail = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = messageBody
                };

                smtpClient.Send(mail);
                Console.WriteLine("E-Posta Başarıyla gönderildi!");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
