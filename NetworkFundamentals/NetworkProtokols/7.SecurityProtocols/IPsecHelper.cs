using System.Security.Cryptography;
using System.Text;

namespace NetworkFundamentals.NetworkProtokols._7.SecurityProtocols
{
    public class IPsecHelper
    {
        private readonly byte[] encryptionKey;
        private readonly HMACSHA256 hmac;

        public IPsecHelper()
        {
            // Şifreleme anahtarı ve HMAC için rastgele bir anahtar oluştur
            encryptionKey = Encoding.UTF8.GetBytes("MySecureEncryptionKey123!");
            hmac = new HMACSHA256(Encoding.UTF8.GetBytes("MySecureEncryptionKey123!"));
        }

        // Veriyi şifrele
        public byte[] EncryptData(string data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.GenerateIV();

                // Veriyi şifrelemek için bir şifreleyici oluştur
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key,aes.IV);
                byte[] encryptedData = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(data), 0, data.Length);

                // IV'yi şifreli verinin başına ekle
                byte[] result = new byte[aes.IV.Length + encryptedData.Length];
                Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length); // IV'yi başa ekle
                Buffer.BlockCopy(encryptedData, 0, result, aes.IV.Length, encryptedData.Length); // Şifrelenmiş veriyi ekle

                return result;
            }
        }

        // Şifreyi çöz
        public string DecryptData(byte[] encryptedData)
        {
            using (Aes aes = Aes.Create())
            {
                // Şifreleme anahtarını ayarla
                aes.Key = encryptionKey;

                // Şifreli veriden IV'yi ayır
                byte[] iv = new byte[16]; // IV uzunluğu AES için 16 bayttır
                Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length); // IV'yi şifreli veriden al

                // Gerçek şifrelenmiş veriyi ayır
                byte[] actualData = new byte[encryptedData.Length - iv.Length];
                Buffer.BlockCopy(encryptedData, iv.Length, actualData, 0, actualData.Length);

                // Çözücü oluştur ve veriyi çöz
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] decryptedData = decryptor.TransformFinalBlock(actualData, 0, actualData.Length);
                return Encoding.UTF8.GetString(decryptedData); // Çözümlenmiş veriyi geri döndür
            }
        }

        // Veriyi HMAC ile imzala
        public byte[] SignData(string data)
        {
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        // HMAC doğrulaması yap
        public bool VerifyData(string data, byte[] sigature)
        {
            byte[] computedSignature = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(computedSignature) == Convert.ToBase64String(sigature);

        }
    }

    public class IPsecSimulation
    {
        public void Start()
        {
            var ipsecHelper = new IPsecHelper();

            Console.WriteLine("IPsec Simülasyonu Başlatılıyor...");
            Console.WriteLine("Gönderilecek mesajı yazın:");
            string originalMessage = Console.ReadLine();

            // 1. Veriyi şifrele
            Console.WriteLine("\n1. Veriyi Şifreleme...");
            byte[] encryptedData = ipsecHelper.EncryptData(originalMessage);
            Console.WriteLine($"Şifrelenmiş Veri: {Convert.ToBase64String(encryptedData)}");

            // 2. Veriyi imzala
            Console.WriteLine("\n2. Veriyi İmzalama...");
            byte[] signature = ipsecHelper.SignData(originalMessage);
            Console.WriteLine($"İmza: {Convert.ToBase64String(signature)}");

            // 3. Veriyi ilet ve doğrula
            Console.WriteLine("\n3. Veriyi Çözümleme ve Doğrulama...");
            string decryptedMessage = ipsecHelper.DecryptData(encryptedData); // Şifreli veriyi çöz
            bool isVerified = ipsecHelper.VerifyData(decryptedMessage, signature); // Çözümlenen veriyi imza ile doğrula

            Console.WriteLine($"Çözümlenmiş Mesaj: {decryptedMessage}");
            Console.WriteLine($"Doğrulama Sonucu: {(isVerified ? "Başarılı" : "Başarısız")}");
        }
    }
}
