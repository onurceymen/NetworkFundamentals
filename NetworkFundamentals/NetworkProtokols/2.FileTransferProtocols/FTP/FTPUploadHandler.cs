using System.Net;

namespace NetworkFundamentals.NetworkProtokols.FtpProtokols
{
    public class FTPUploadHandler
    {
        public void Execute()
        {
            Console.WriteLine("FTP Sunucu URL'sini girin (örn: ftp://example.com/yuklenecekDosya.txt):");
            string ftpUrl = Console.ReadLine();

            Console.WriteLine("Yüklemek istediğiniz yerel dosyanın tam yolunu girin (örn: C:\\dosyalarim\\dosya.txt):");
            string localPath = Console.ReadLine();

            Console.WriteLine("Kullanıcı adınızı girin (boş bırakabilirsiniz):");
            string username = Console.ReadLine();

            Console.WriteLine("Şifrenizi girin (boş bırakabilirsiniz):");
            string password = Console.ReadLine();

            try
            {
                if (!File.Exists(localPath))
                {
                    Console.WriteLine("Yerel dosya bulunamadı! Lütfen geçerli bir dosya yolu girin.");
                    return;
                }

                Console.WriteLine("FTP Sunucusuna bağlanıllıyor...");
                WebClient client = new WebClient();

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    client.Credentials = new NetworkCredential(username, password);
                }

                client.UploadFile(ftpUrl, WebRequestMethods.Ftp.UploadFile, localPath);
                Console.WriteLine("Dosya başarıyla yüklendi!");


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
