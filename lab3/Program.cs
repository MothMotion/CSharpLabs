using System;

internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("Задание 4:");
    Binary.t4_Create(100);
    Binary.t4_Read(100); // Проблемы кодировки. В консоли UTF-8, здесь ASCII, из-за чего PeekChar работает неправильно.
    Console.WriteLine("\n\n\nЗадание 5:");
    Binary.t5_Create();
    Binary.t5_Read();
    Console.WriteLine("\n\n\nЗадание 6:");
    Binary.t6_Create(100);
    Binary.t6_Read();
    Console.WriteLine("\n\n\nЗадание 7:");
    Binary.t7_Create(10);
    Binary.t7_Read();
    Console.WriteLine("\n\n\nЗадание 8:");
    Binary.t8_Read("t8_inp.txt", "t8_out.txt");
    Console.WriteLine($"Массив.\n\n\n");

    int x = securedInt("Размерность массива 1: nxm. Далее будет использован в задании 2. Введите n: ");
    int y = securedInt("Размерность массива 1: nxm. Далее будет использован в задании 2. Введите m: ");
    Massive M1 = new Massive(x,y);
    M1.print();
    x = securedInt("Размерность массива 2: nxn. Введите n: ");
    bool b = securedBool("Выбор из подзадач 2/3. false для 2, true для 3: ");
    Massive M2 = new Massive(x,x,b);
    M2.print();

    System.Console.WriteLine("Задание 2 для матрицы:"); M1.print();
    M1.checkMarks();

    // A^T - B^T + 2*C
    x = securedInt("Размерность массива 3, 4, 5: nxn. Введите n: ");
    System.Console.WriteLine("Массив A:");
    Massive A = new Massive(x, x); A.print();
    System.Console.WriteLine("Массив B:");
    Massive B = new Massive(x, x); B.print();
    System.Console.WriteLine("Массив C:");
    Massive C = new Massive(x, x); C.print();

    System.Console.WriteLine("Результат операции: A^T - B^T + 2*C:");
    (Massive.transform(A) - Massive.transform(B) + 2*C).print();
  }

  private static int securedInt(string mes) {
    int res;
    bool valid;
    string input;

    do {
      System.Console.WriteLine(mes);
      input = System.Console.ReadLine();

      valid = int.TryParse(input, out res);
    } while(!valid || res<=0);
    return res;
  }
  
  private static bool securedBool(string mes) {
    bool res;
    bool valid;
    string input;

    do {
      System.Console.WriteLine(mes);
      input = System.Console.ReadLine();

      valid = bool.TryParse(input, out res);
    } while(!valid);
    return res;
  }
}
