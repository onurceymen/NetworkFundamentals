namespace NetworkFundamentals.NetworkProtokols.HttpProtokols
{
    public class HttpProtocolHandler
    {
        public async Task Execute()
        {
            Console.WriteLine("Hangi siteyi ziyaret etmek istersiniz? (Örn: https://example.com)");
            string url = Console.ReadLine();

            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine("URL boş bırakılamaz!");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Siteye bağlanılıyor...");
                    HttpResponseMessage response = await client.GetAsync(url);

                    Console.WriteLine($"Durum Kodu: {response.StatusCode}");
                    Console.WriteLine("Site içeriği:");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content.Substring(0, Math.Min(content.Length, 500))); // İlk 500 karakter
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }
        }
    }
}