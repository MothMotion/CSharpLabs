using System;
using System.Xml.Serialization;

class Sprt {
  private string surname;
  private string name;
  private int[] points; // <-- Стандартный класс SortedSet тут не канает, потому что "ДОДЕЛАЙ САМ"

  public string Surname {get => surname; set => surname = value;} // Без set XMLSerializer не работает
  public string Name {get => name; set => name = value;} // Или писать свой сериализатор, или через костыли
  public int[] Point {get => points; set {Array.Sort(value); points = value;}}

  public Sprt(string _surname, string _name, int[] _points) {
    surname = _surname;
    name = _name;
    Array.Sort(_points); 
    points = _points;
  }

  public Sptr() {
    surname = "";
    name = "";
    points = null;
  }



  public static void FillFile(int amount, int amountCat, string pathTo) {
    Sprt[] sports = new Sprt[amount];
    Random rng = new Random();

    for(int i=0; i<amount; ++i)
      sports[i] = new Sprt(rngString(rng.Next(4,21)), rngString(rng.Next(4,13)), rngSet(amountCat));

    SerializableSprt(sports, pathTo);
  } 



  public static int[,] GetPlaces(Sprt[] sports) {
    if(sports.Length < 0)
      return null;
    int[,] res = new int[sports.Length, 3];
    SortedList<int, int> places = [];

    for(int i=0; i<sports.Length; ++i)
      places.Add(sports[i].getSum(), i);
    
    res[0,0] = places.Last().Value; res[0,1] = places.Last().Key; res[0,2] = 1;
    for(int i=2; i<=sports.Length; ++i) {
      res[i-1, 0] = places.ElementAt(sports.Length - i).Value; res[i-1, 1] = places.ElementAt(sports.Length - i).Key;
      if(places.ElementAt(sports.Length - i).Key == res[i-2,1])
        res[i-1, 2] = res[i-2, 2];
      else
        res[i-1, 2] = res[i-2, 2] + 1;
    }
    return res;
  }



  public static void SerializableSprt(Sprt[] sports, string pathTo) {
    using (FileStream fs = new FileStream(pathTo, FileMode.Create)) {
      XmlSerializer xmlSer = new XmlSerializer(typeof(Sprt[]));
      xmlSer.Serialize(fs, sports);
    }
  }

  public static Sprt[] DeserializeSprt(string path) {
    XmlSerializer xmlS = new(typeof(Sprt[]));
    using FileStream fs = new FileStream(path, FileMode.Open);
    return (Sprt[])xmlS.Deserialize(fs);
  }



  public override string ToString() {
    string res = $"{surname} {name} ";

    foreach(int val in points)
      res += $"{val} ";

    return res;
  }



  private static string rngString(int amountSym) {
    if(amountSym <= 0)
      return "";

    Random rng = new Random();
    string res = Convert.ToString((char)rng.Next( (int)'A', (int)'Z'+1 ));

    for(int i=1; i<amountSym; ++i)
      res += Convert.ToString((char)rng.Next( (int)'a', (int)'z'+1 ));
 
    return res;
  }

  private static int[] rngSet(int size) {
    int[] res = new int[size];
    Random rng = new Random();
    for(int i=0; i<size; ++i)
      res[i] = rng.Next(0,101);

    return res;
  }

  public int getSum() {
    int res = 0;
    foreach (int v in points)
      res += v;
    return res;
  }
}