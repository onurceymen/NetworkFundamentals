using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFundamentals.NetworkProtokols.FtpProtokols
{
    public class FTPDeleteHandler
    {
        public void Execute()
        {
            Console.WriteLine("Silmek istediğiniz dosya veya klasörün FTP URL'sini girin (örn: ftp://example.com/dosya.txt):");
            string ftpUrl = Console.ReadLine();

            Console.WriteLine("Kullanıcı adınızı girin (boş bırakabilirsiniz):");
            string username = Console.ReadLine();

            Console.WriteLine("Şifrenizi girin (boş bırakabilirsiniz):");
            string password = Console.ReadLine();

            try
            {
                Console.WriteLine("FTP sunucusuna bağlanılıyor ve silme işlemi yapılıyor...");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    request.Credentials = new NetworkCredential(username, password);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Dosya/Klasör silindi! Durum: {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
