namespace NetworkFundamentals.HTTPDesingPaterns
{
    //Osi Model 7 Katmandan oluşan bir tasarıma Sahip desing dir. Bu katmanlar örnekler ile işlenmiştir
    public class OsiModel
    {
        public async Task MakeRequestAsync()
        {
            //Uygulama Katmanı (Application Layer) : Kullanıcının Veri İsteğinin Başlatması
            Console.WriteLine("Bir Web İsteği Gönderiliyor");

            //Taşıma Katmanı (Transport Layer) : Veriyi Hedefe Yönlendirmek için HTTP istemcisini Oluşturma
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Ağ katmanı (NetWork Layer): IP adresine Göre Hedef Sunucuya ulaşma
                    HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

                    //Veri Bağlantısı (Data Link Layer) ve Fiziksel Katman (Physical Layer) Bu işlemi sistemde Otomatik olarak Yapar,
                    //Biz burada cihazin donanımı ile veri iletimi sağlarız, ama bunları manuel olarak belirtmeyiz.
                    if (response.IsSuccessStatusCode)
                    {
                        //Sunum Katmanı (Presentation Layer) : Veriyi Jason Formatında Anlamlı hale getirme
                        string result = await response.Content.ReadAsStringAsync();

                        //Uygulama Katmanı (Application Layer) : Veriyi ekrana yazdırma
                        Console.WriteLine("Gelen Veri");
                        Console.WriteLine(result);

                    }
                    else
                    {
                        Console.WriteLine("Bir hata oluştu" + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluşursa, uygulama katmanında kullanıcıya hata mesajı gösterme
                    Console.WriteLine("İstek Gönderilmeden Önce Herhangi Bir hata oluştu" + ex.Message);
                }
            }
        }
    }
}

//var osiExample = new OsiHttpExample();
//await osiExample.MakeRequestAsync();