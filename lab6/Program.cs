using System.Formats.Asn1;
using System.Reflection.Metadata;

internal class Programm {
    private static void Main(string[] args) {
        Cat barsik = new("Барсик");
        barsik.meow();
        barsik.meow(3);
        Lion leo = new("Лео");
        leo.meow();
        leo.meow(3);
        Console.WriteLine("\nНачнём учёт количества мяу и ррр.");
        MeowTrack bar_mt = new(barsik);
        MeowTrack leo_mt = new(leo);
        IMeow.meowAll([bar_mt, leo_mt]);

        Console.WriteLine($"Кол-во мяу у Барсика: {bar_mt.Count}\nКол-во ррр у Лео: {leo_mt.Count}");

        Fraction fr1 = new(0, 2);
        Console.WriteLine(fr1==4);
        Fraction fr2 = (Fraction)fr1.Clone();
        Console.WriteLine(fr1);
    }
    private int securedInt(string mes = "", bool isZ = false) {
        int res;
        do {
            Console.WriteLine(mes);
        } while(int.TryParse(Console.ReadLine(), out res) || (isZ && res<=0));
        return res;
    }
}