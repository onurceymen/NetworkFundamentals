using NetworkFundamentals.NetworkProtokols.HttpProtokols;

Console.WriteLine("Protokol Eğitimi'ne Hoş Geldiniz!");
Console.WriteLine("Lütfen protokol seçin:");
Console.WriteLine("1. HTTP/HTTPS");
// İlerleyen protokoller buraya eklenecek.

string choice = Console.ReadLine();

switch (choice)
{
    case "1":
        var httpHandler = new HttpProtocolHandler();
        await httpHandler.Execute();
        break;

    default:
        Console.WriteLine("Geçersiz seçim!");
        break;
}

Console.WriteLine("Program sonlandı.");