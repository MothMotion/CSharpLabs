using System;
using System.Diagnostics.Metrics;

internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("Введите списки L1, L2, и будет выведено их пересечение L.");
    List<int> L1 = new List<int>(securedList(securedInt("Введите размер списка L1:", true)));
    List<int> L2 = new List<int>(securedList(securedInt("Введите размер списка L2:", true)));
    Console.WriteLine("Список L:");
    foreach (int v in Clln<int>.intersect(L1, L2))
      Console.Write($"{v} ");
    
    Console.WriteLine("\n\n\n\nВведите список L, и в конец его будут добавлены его элементы в обратном порядке.");
    LinkedList<int> LL = new LinkedList<int>(securedList(securedInt("Введите размер списка LL:", true)));
    Console.WriteLine("Список LL с элементами в обратном порядке в конце:");
    foreach(int v in Clln<int>.addReverse(LL))
      Console.Write($"{v} ");

    Console.WriteLine("\n\n\n\nПосещение ТРЦ:\nВведите все ТРЦ через пробел:");
    HashSet<string> trz = [.. Console.ReadLine().Split(' ')];
    int size = securedInt("Введите количество студентов", true);
    HashSet<string>[] studs = new HashSet<string>[size];
    for(int i=0; i<size; ++i) {
      Console.WriteLine($"Студент номер {i+1} посещал:");
      studs[i] = [ .. Console.ReadLine().Split(' ')];
    }
    Clln<int>.checkAtten(trz, studs);
    
    Sprt.FillFile(securedInt("Введите количество спортсменов:", true),
    securedInt("Введите количество соревнований:", true), "sprt.xml");

    Sprt[] sports = Sprt.DeserializeSprt("sprt.xml");

    Console.WriteLine("Получаем места для файла \"sprt.xml\":");
    int[,] places = Sprt.GetPlaces(sports);
    for(int i=0; i<places.GetLength(0); ++i) {
      Console.WriteLine($"{sports[places[i,0]].Surname} {sports[places[i,0]].Name} {places[i,1]} {places[i,2]}");
    }
  }
  private static int securedInt(string mes, bool isZ) {
    int res;

    do {
      Console.WriteLine(mes);
    } while(!(int.TryParse(Console.ReadLine(), out res)) || (isZ && res <= 0)); // try convert input to int as res

    return res;
  }

  private static int[] securedList(int size, string mes = "Введите список через пробел:") {
    int[] res = new int[size];
    int i=0;

    do {
      Console.WriteLine(mes);

      foreach(string val in Console.ReadLine().Split(' ')) { // reading string and splitting it by spaces
        if(i >= size || !int.TryParse(val, out res[i++])) { // if val is valid as int; write it in res[i] if valid, or reset
          i=0; break;
        }
      }
    } while(!(i == size)); // check if is valid

    return res;
  }
}
