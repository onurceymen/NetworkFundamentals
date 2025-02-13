using System.Net;

namespace NetworkFundamentals.NetworkProtokols.FtpProtokols
{
    public class FTPListFilesHandler
    {
        public void Execute()
        {
            // Kullanıcıdan FTP sunucu URL'sini girmesini iste
            Console.WriteLine("FTP Sunucu URL'sini girin (örn: ftp://example.com/):");
            string ftpUrl = Console.ReadLine()?.Trim(); // Girdiği değeri okuyun ve fazla boşlukları temizleyin

            // Kullanıcıdan kullanıcı adını girmesini iste (isteğe bağlı)
            Console.WriteLine("Kullanıcı adınızı girin (boş bırakabilirsiniz):");
            string username = Console.ReadLine()?.Trim();

            // Kullanıcıdan şifresini girmesini iste (isteğe bağlı)
            Console.WriteLine("Şifrenizi girin (boş bırakabilirsiniz):");
            string password = Console.ReadLine()?.Trim();

            // FTP URL'sinin geçerli olup olmadığını kontrol et
            if (string.IsNullOrEmpty(ftpUrl))
            {
                Console.WriteLine("FTP URL'si geçerli değil.");
                return;
            }

            try
            {
                Console.WriteLine("FTP sunucusundaki dosyalar listeleniyor...");

                // Dizindeki içerikleri listelemek için bir FTP isteği oluştur
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory; // Dizini listelemek için yöntemi ayarla

                // Kullanıcı adı ve şifre sağlanmışsa kimlik bilgilerini ayarla
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    request.Credentials = new NetworkCredential(username, password);
                }

                // İstek ayarlarını yapılandır
                request.UseBinary = true; // Veri aktarımı için ikili modu kullan
                request.UsePassive = true; // FTP bağlantısı için pasif modu kullan
                request.KeepAlive = false; // İstekten sonra bağlantıyı açık tutma

                // FTP sunucusundan yanıt al
                using FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using StreamReader reader = new StreamReader(response.GetResponseStream() ?? Stream.Null);
                {
                    // Yanıt akışını oku ve dizin içeriğini görüntüle
                    string files = reader.ReadToEnd();
                    Console.WriteLine(files);
                }

                // Yanıtın durumunu görüntüle
                Console.WriteLine("Dosya listeleme tamamlandı! ({0})", response.StatusDescription);
            }
            catch (WebException webEx)
            {
                // FTP yanıtları için özel olarak web hatalarını işle
                if (webEx.Response is FtpWebResponse ftpResponse)
                {
                    Console.WriteLine($"FTP Hatası: {ftpResponse.StatusDescription}");
                }
                else
                {
                    Console.WriteLine($"Web Hatası: {webEx.Message}");
                }
            }
            catch (Exception ex)
            {
                // Diğer genel hataları işle
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
