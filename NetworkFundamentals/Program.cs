using NetworkFundamentals.Testers;

Console.Write("TTL değeri için IP adresi veya ana bilgisayar adı girin: ");
string target = Console.ReadLine();

if (string.IsNullOrEmpty(target))
{
    Console.WriteLine("Geçerli bir hedef adres girilmelidir.");
    return;
}

// TtlTester sınıfını oluştur ve TTL kontrolünü başlat
var ttlTester = new TtlTester(target);
ttlTester.CheckTtl();