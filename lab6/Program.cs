using System.Formats.Asn1;
using System.Reflection.Metadata;
using System.Security;

internal class Programm {
    private static void Main(string[] args) {
        Cat barsik = new("Барсик");
        barsik.meow();
        barsik.meow(3);
        Console.WriteLine("Проверяем второй класс, Lion и MeowTrack:");
        Lion l = new(securedString("Введите имя льва: "));
        MeowTrack mw = new(l);
        int inp;
        do {
            inp = securedInt($"Сколько раз должен прорычать лев {l.Name}? 0 - выход: ");
            mw.meow(inp);
        } while(inp != 0);
        Console.WriteLine($"Количество рыков: {mw.Count}");

        Fraction fr1 = new(1,2);
        Fraction fr2 = new(2);
        Fraction fr3 = new(2,3);

        Console.WriteLine($"{fr1} * {fr2} = {fr1*fr2}\n{fr1} / {fr3} = {fr1/fr3}\n({fr1} + {fr2}) / {fr3} - {5} = {(fr1+fr2)/fr3-5}");
        Console.WriteLine($"{fr1} =?= {fr2}: {fr1 == fr2}\n{fr1*fr3} =?= {fr3/fr2}: {fr1*fr3 == fr3/fr2}");

        Fraction fr = new(securedInt("Введите числитель: "), securedInt("Введите знаменатель: ", true));
        DoubleFraction df = new(fr);

        Console.WriteLine($"Значение дроби: {df.Value()}");
    }
    private static int securedInt(string mes = "", bool isZ = false) {
        int res;
        do {
            Console.WriteLine(mes);
        } while(!int.TryParse(Console.ReadLine(), out res) || (isZ && res<=0));
        return res;
    }

    private static string securedString(string mes = "") {
        string res;
        do {
            Console.WriteLine(mes);
            res = Console.ReadLine();
        } while(res == null || res == "");
        return res;
    }
}