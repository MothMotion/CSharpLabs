using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Numerics;
using System.Xml.Serialization;

public class Binary {
  [Serializable]
  public struct Toy {
    private string name;
    private int price;
    private int ageFrom;
    private int ageTo;

    public string Name {get{return name;} set{name = value;}}
    public int Price {get{return price;} set{price = value;}}
    public int AgeFrom {get{return ageFrom;} set{ageFrom = value;}}
    public int AgeTo {get{return ageTo;} set{ageTo = value;}}

    public Toy(string n, int p, int aF, int aT) {
      name = n;
      price = p;
      ageFrom = aF;
      ageTo = aT;
    }

    public void RNG() {
      Random rng = new Random();
      price = rng.Next(100, 1001);
      ageFrom = rng.Next(2, 6);
      ageTo = rng.Next(ageFrom, 6);
    }
  }

  public static void SerializableToy(Toy[] toys) {
    using (FileStream fs = new FileStream("t5.xml", FileMode.OpenOrCreate)) {
      XmlSerializer xml = new XmlSerializer(typeof(Toy[]));
      xml.Serialize(fs, toys);
    }
  }

  public static Toy[] DeserializeToy() {
    XmlSerializer xmlS = new XmlSerializer(typeof(Toy[]));
    using (FileStream fs = new FileStream("t5.xml", FileMode.Open)) {
      return (Toy[])xmlS.Deserialize(fs);
    }
  }

  public static void t4_Create(int amount) {
    Random rng = new Random();
    using (BinaryWriter writer = new BinaryWriter(File.Open("t4.dat", FileMode.OpenOrCreate))) {
      for(int i=0; i<amount;++i)
        writer.Write(rng.Next(1,101));
    }
  }

  public static void t4_Read(int amount) {
    using (System.IO.BinaryReader reader = new System.IO.BinaryReader(File.Open("t4.dat", FileMode.Open))) {
      int temp;
      int count = 0;
      for(int i=0; i<amount; ++i) {
        temp = reader.ReadInt32();
        System.Console.Write($"{temp} ");
        if(isValid(temp))
          ++count;
      }
      System.Console.WriteLine($"\nКоличество подходящих элементов: {count}");
    }
  }

  public static void t5_Create() {
    using (System.IO.StreamWriter sw = new StreamWriter("t5.xml")) {
      Toy[] toys = new Toy[10];
      toys[0] = new Toy("abc", 100, 1, 5);
      toys[1] = new Toy("acb", 120, 2, 5);
      toys[2] = new Toy("cba", 110, 1, 4);
      toys[3] = new Toy("acb", 130, 4, 5);
      toys[4] = new Toy("cab", 135, 1, 3);
      toys[5] = new Toy("bac", 120, 1, 5);
      toys[6] = new Toy("abcd", 110, 3, 5);
      toys[7] = new Toy("abdc", 130, 1, 4);
      toys[8] = new Toy("adbc", 120, 2, 4);
      toys[9] = new Toy("dabc", 115, 1, 5);

      SerializableToy(toys);
    }
  }

  public static void t5_Read() {
    Toy[] toys = DeserializeToy();
    Toy toyRes = toys[0];
    for(int i=1; i<toys.Length; ++i) {
      if(toys[i].AgeTo == 4 && toys[i].Price > toyRes.Price)
        toyRes = toys[i];
    }
    System.Console.WriteLine($"Самая дорогая игрушка для детей до 4 лет: {toyRes.Name}");
  }

  public static void t6_Create(int amount) {
    Random rng = new Random();
    using (System.IO.StreamWriter sw = new StreamWriter("t6.txt")) {
      for(int i=0; i<amount; ++i)
        sw.WriteLine(rng.Next(0,1000));
    }
  }

  public static void t6_Read() {
    using (System.IO.StreamReader sr = new StreamReader("t6.txt")) {
      int min = 99999999;
      int max = 0;
      int temp;
      string line = sr.ReadLine();
      while(line != null) {
        Console.Write($"{line} ");
        temp = Int32.Parse(line);
        min = Math.Min(temp, min);
        max = Math.Max(temp, max);
        line = sr.ReadLine();
      }
      Console.WriteLine($"min: {min}, max: {max}, arif: {(double)(min+max)/2}");
    }
  }

  public static void t7_Create(int amount) {
    using (System.IO.StreamWriter sw = new StreamWriter("t7.txt")) {
      Random rng = new Random();
      for(int i=0; i<amount/10; ++i) {
        for(int j=0; j<10; ++j)
          if(j<9)
            sw.Write($"{rng.Next(0, 101)} ");
          else
            sw.Write(rng.Next(0,101));
        sw.Write("\n");
      }
    }
  }
  
  public static void t7_Read() {
    using (System.IO.StreamReader sr = new StreamReader("t7.txt")) {
      string line = sr.ReadLine();
      int temp, res = 1;
      while(line != null) {
        foreach(string val in line.Split(' ')) {
          temp = Int32.Parse(val);
          System.Console.Write($"{temp} ");
          if(temp%2 == 0)
            res *= temp;
        }
        System.Console.WriteLine();
        line = sr.ReadLine();
      }
      System.Console.WriteLine($"Ответ на задачу 7: {res}");
    }
  }

  public static void t8_Read(string path, string pathTo) {
    using (System.IO.StreamWriter sw = new StreamWriter(pathTo)) {
      using (System.IO.StreamReader sr = new StreamReader(path)) {
        string line = sr.ReadLine();
        while(line != null) {
          if(isValid(line))
            sw.WriteLine(line);
          line = sr.ReadLine();
        }
      }
    }
  }



  private static bool isValid(int a) {
    int i;
    for(i=1; i*i<a; ++i);
    if(i*i == a && i%2 == 0)
      return true;
    return false;
  }

  private static bool isValid(string str) {
    foreach(char val in str)
      if(val >= 'a' && val <= 'z' || val >= 'A' && val <= 'Z')
        return false;
    return true;
  }

  private static int securedInt(string mes) {
    int res;
    bool valid;
    string input;

    do {
      System.Console.WriteLine(mes);
      input = System.Console.ReadLine();

      valid = int.TryParse(input, out res);
    } while(!valid);
    return res;
  }
}
