using Renci.SshNet;
using Renci.SshNet.Common;

namespace NetworkFundamentals.NetworkProtokols._2.FileTransferProtocols.SFTP
{
    public class SFTPDownloadHandler
    {
        public void Execute()
        {
            Console.WriteLine("SFTP Sunucu Adresini girin (örn: sftp.example.com):");
            string host = Console.ReadLine();

            Console.WriteLine("Kullanıcı adınızı girin:");
            string username = Console.ReadLine();

            Console.WriteLine("Şifrenizi girin:");
            string password = Console.ReadLine();

            Console.WriteLine("İndirmek istediğiniz uzak dosyanın yolunu girin (örn: /remote/path/to/file.txt):");
            string remoteFilePath = Console.ReadLine();

            Console.WriteLine("Dosyanın kaydedileceği yerel yol (örn: C:\\indirilenler\\file.txt):");
            string localFilePath = Console.ReadLine();

            // Girdi kontrolleri (boş girişleri kontrol et)
            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(remoteFilePath) || string.IsNullOrEmpty(localFilePath))
            {
                Console.WriteLine("Gerekli bilgilerin tümünü girmelisiniz.");
                return;
            }

            try
            {
                Console.WriteLine("SFTP sunucusuna bağlanılıyor...");

                using (var sftp = new SftpClient(host, username, password))
                {
                    sftp.Connect();
                    Console.WriteLine("Bağlantı Başarılı.!");

                    using (var fileStream = new FileStream(localFilePath,FileMode.Create,FileAccess.Write, FileShare.None,4096,FileOptions.SequentialScan))
                    {
                        sftp.DownloadFile(remoteFilePath, fileStream);
                    }
                    Console.WriteLine("Dosya başarıyla indirildi!");
                    sftp.Disconnect();
                }
;

            }
            catch (SshException sshEx)
            {
                // SSH bağlantı hatalarını işleme
                Console.WriteLine($"SSH Hatası: {sshEx.Message}");
            }
            catch (IOException ioEx)
            {
                // Dosya ile ilgili hataları işleme
                Console.WriteLine($"Dosya Hatası: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                // Diğer genel hataları işleme
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}