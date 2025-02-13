using System.Net;

namespace NetworkFundamentals.NetworkProtokols.FtpProtokols
{
    public class FTPDownloadHandler
    {
        public void Execute()
        {
            Console.WriteLine("FTP Sunucu URL'sini girin (örn: ftp://example.com/dosya.txt):");
            string ftpUrl = Console.ReadLine();

            Console.WriteLine("Dosyanın kaydedileceği yerel yol (örn: C:\\indirilenler\\dosya.txt):");
            string localPath = Console.ReadLine();

            Console.WriteLine("Kullanıcı adınızı girin (boş bırakabilirsiniz):");
            string username = Console.ReadLine();

            Console.WriteLine("Şifrenizi girin (boş bırakabilirsiniz):");
            string password = Console.ReadLine();


            try
            {
                Console.WriteLine("FTP Sunucusuna Bağlanılıyor...");
                WebClient client = new WebClient();
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    client.Credentials = new NetworkCredential(username, password);
                }

                client.DownloadFile(ftpUrl, localPath);
                Console.WriteLine("Dosya başarıyla indirildi!");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
