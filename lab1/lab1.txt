using System;

internal class Program {
  private static void Main(string[] args) {
    Program x = new Program();

    double a1;
    System.Console.WriteLine("fraction"); System.Console.Write("Введите действительное число и получите дробную часть:");
    a1 = Convert.ToDouble(System.Console.ReadLine());
    System.Console.WriteLine($"Дробная часть {a1} = {x.fraction(a1)}\n====\n");

    String a2 = "";
    System.Console.WriteLine("charToInt\nВведите цифру, и она будет переведена из char в int:");
    a2 = Convert.ToString(System.Console.ReadLine());
    if(a2.Length == 1)
      System.Console.WriteLine($"{a2} >> {x.charToInt(a2[0])}\n====\n");
    else
      System.Console.WriteLine($"Строка {a2} длинее 1 символа.\n");

    int a3;
    System.Console.WriteLine("is2Digits\nВведите число и будет выведено true, если число двузначное:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Число {a3} является двузначным?\n{x.is2Digits(a3)}\n====\n");

    int a4, a5;
    System.Console.Write("isInRange\nВведите 3 числа и будет выведено true, если 3-е число находится между первым и вторым.\nОт:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.Write("До:"); a4 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.Write("Число:"); a5 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"{x.isInRange(a3,a4,a5)}\n====\n");

    System.Console.Write("isEqual\nВведите 3 числа и будет выведено true, если все они равны.\n1 число:");
    a3 = Convert.ToInt32(System.Console.ReadLine()); System.Console.Write("2 число:");
    a4 = Convert.ToInt32(System.Console.ReadLine()); System.Console.Write("3 число:");
    a5 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"{a3} == {a4} == {a5}: {x.isEqual(a3, a4, a5)}\n");

    System.Console.Write("abs\nВведите число и оно будет выведено в модуле:\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"|{a3}| = {x.absol(a3)}\n");

    System.Console.Write("is35\nВведите число и будет выведено True, если число делится либо на 3, либо на 5.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Вывод: {x.is35(a3)}\n");

    System.Console.Write("max3\nВведите три числа и будет выведено максимальное из них.\nЧисло 1:");
    a3 = Convert.ToInt32(System.Console.ReadLine()); System.Console.Write("Число 2:");
    a4 = Convert.ToInt32(System.Console.ReadLine()); System.Console.Write("Число 3:");
    a5 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Наибольшее число: {x.max3(a3, a4, a5)}\n");

    System.Console.Write("sum2\nВведите 2 числа и будет выведено 20, если их сумма из промежутка [10,19], иначе их сумму\nЧисло 1:");
    a3 = Convert.ToInt32(System.Console.ReadLine()); System.Console.Write("Число 2:");
    a4 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Сумма: {x.sum2(a3, a4)}\n");

    System.Console.Write("day\nВведите число, обозначающее день недели и будет выведено название дня недели.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"{x.day(a3)}\n");

    System.Console.Write("listNums\nВведите число, и будет выведен список чисел оn 0 до него включительно.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"{x.listNums(a3)}\n");

    System.Console.Write("chet\nВведите число, и будет выведен список из чётных чисел от 0 включительно до этого числа.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"{x.chet(a3)}\n");

    long a6;
    System.Console.Write("numLen\nВведите число, и будет выведена длина этого числа.\nЧисло:");
    a6 = Convert.ToInt64(System.Console.ReadLine());
    System.Console.WriteLine($"Длина числа {a6}: {x.numLen(a6)}\n");

    System.Console.Write("square\nВведите число, и будет отображён квадрат с стороной равной этому числу.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine()); x.square(a3);
    System.Console.WriteLine("\n");

    System.Console.Write("rightTriangle\nВведите число, и будет отображён прямоугольный треугольник с стороной равной этому числу.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine()); x.rightTriangle(a3);
    System.Console.WriteLine("\n");


    do {
      System.Console.Write("findFirst\nВведите размер массива, он будет сгенерирован автоматически:\nРазмер:");
      a3 = Convert.ToInt32(System.Console.ReadLine());
    } while(a3 <= 0);
    int[] arr1 = new int[a3]; fillRandom(ref arr1, -100, 100); System.Console.Write($"Массив заполнен следующими значениями:\n{printArr(arr1)}\nВведите число, которое нужно найти, если число не удалось найти, выведится -1:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Вывод: {x.findFirst(arr1, a3)}");

    do {
      System.Console.Write("maxAbs\nВведите размер массива, и он будет сгенерирован автоматически:\nРазмер:"); 
      a3 = Convert.ToInt32(System.Console.ReadLine());
    } while(a3 <= 0);
    int[] arr2 = new int[a3]; fillRandom(ref arr2, -100, 100);
    System.Console.WriteLine($"Массив заполнен следующими значениями:\n{printArr(arr2)}\nМаксимальный модуль в этом массиве: {x.maxAbs(arr2)}\n");

    do {
      System.Console.Write("add\nВведите размер первого массива, и он будет сгенерирован автоматически:\nРазмер:");
      a3 = Convert.ToInt32(System.Console.ReadLine());
    } while(a3 <= 0);
    int[] arr3 = new int[a3]; fillRandom(ref arr3, -100, 100); System.Console.Write($"Массив заполнен следующими значениями:\n {printArr(arr3)}");
    do {
      System.Console.Write($"\nВведите размер второго массива, и он будет сгенерирован автоматически:\nРазмер:");
      a3 = Convert.ToInt32(System.Console.ReadLine());
    } while(a3 <= 0);
    int[] arr4 = new int[a3]; fillRandom(ref arr4, -100, 100); System.Console.Write($"Массив заполнен следующими значениями:\n {printArr(arr4)}\nВведите позицию, в которую нужно вставить второй массив.\nПозиция будет преобразована в clamp(позиция, 0, длины первого массива).\nПозиция:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Итоговый массив выглядит как:\n{printArr(x.add(arr3, arr4, a3))}");

    do {
      System.Console.Write("reverseBack\nВведите размер массива, и он будет сгенерирован автоматически:\nРазмер:");
      a3 = Convert.ToInt32(System.Console.ReadLine());
    } while(a3 <= 0);
    int[] arr5 = new int[a3]; fillRandom(ref arr5, -100, 100); System.Console.WriteLine($"Массив заполнен следущими значениями:\n{printArr(arr5)}\nИнверсированный массив:\n{printArr(x.reverseBack(arr5))}\n");

    do {
      System.Console.Write("findAll\nВведите размер массива, и он будет сгенерирован автоматически:\nРазмер:");
      a3 = Convert.ToInt32(System.Console.ReadLine());
    } while(a3 <= 0);
    int[] arr6 = new int[a3]; fillRandom(ref arr6, -10, 10); System.Console.Write($"Массив заполнен следующими значениями:\n{printArr(arr6)}\nВведите число, которое нужно найти, и будет выведены все позиции данного числа в массиве, или -1, если они не найдены.\nЧисло:");
    a3 = Convert.ToInt32(System.Console.ReadLine());
    System.Console.WriteLine($"Вывод: ${printArr(x.findAll(arr6, a3))}");
  }
  static public double absol(double x) {
    if(x < 0)
      return -x;
    return x;
  }

  static public long absol(long x) {
    if(x < 0)
      return -x;
    return x;
  }

  static public void fillRandom(ref int[] arr, int min, int max) {
    if(min > max) {
      int t = min;
      min = max;
      max = t;
    }
    Random rnd = new Random();
    for(int i=0; i<arr.Length; ++i)
      arr[i] = rnd.Next(min, max);
  }

  static public String printArr(int[] arr) {
    String result = "";
    for(int i=0; i<arr.Length; ++i)
      result += ($" {arr[i]} ");

    return result;
  }

  static public int clamp(int x, int min, int max) {
    if(x < min)
      return min;
    if(x > max)
      return max;
    return x;
  }



  public double fraction(double x) {
    x = absol(x);
    if(Convert.ToInt32(x) > x)
      return x - Convert.ToInt32(x) + 1;
    return x - Convert.ToInt32(x);
  }

  public int charToInt(char x) {
    if(Convert.ToInt32(x) - Convert.ToInt32('0') > 9 || Convert.ToInt32(x) - Convert.ToInt32('0') < 0)
      return -1;
    return Convert.ToInt32(x) - Convert.ToInt32('0');
  }

  public bool is2Digits(int x) {
    return absol(x)>9 && absol(x)<100;
  }

  public bool isInRange(int a, int b, int num) {
    if(a > b)
      return isInRange(b, a, num);
    return a <= num && num <= b;
  }

  public bool isEqual(int a, int b, int c) {
    return a == b && b == c;
  }

  public int absol(int x) {
    if(x<0)
      return -x;
    return x;
  }

  public bool is35(int x) {
    return !(absol(x)%3 > 0) ^ !(absol(x)%5 > 0);
  }

  public int max3(int x, int y, int z) {
    if(x >= y && x >= z)
      return x;
    if(y >= x && y >= z)
      return y;
    return z;
  }

  public int sum2(int x, int y) {
    if(x+y <= 19 && x+y >= 10)
      return 20;
    return x+y;
  }

  public String day(int x) {
    switch(x) {
      case 1 : return "Понедельник"; break;
      case 2 : return "Вторник"; break;
      case 3 : return "Среда"; break;
      case 4 : return "Четверг"; break;
      case 5 : return "Пятница"; break;
      case 6 : return "Суббота"; break;
      case 7 : return "Воскресенье"; break;
      default : return "Не день недели"; break;
    }
  }

  public String listNums(int x) {
    String result = ""; 
    for(int i=0; i<=x; ++i)
      result += $" {i}";

    return result;
  }

  public String chet(int x) {
    if(x < 0)
      return "";
    String result = "0";

    for(int i=2; i<=x; i+=2)
      result += $" {i}";
    
    return result;
  }

  public int numLen(long x) {
    int len = 0;
    x = absol(x);

    while(x != 0) {
      x /= 10;
      ++len;
    }

    return len;
  }

  public void square(int x) {
    if(x <= 0) {
      System.Console.Write("\nНеверно введёная сторона.\n");
      return;
    }
    for(int i=0; i<x; ++i) {
      for(int j=0; j<x; ++j)
        System.Console.Write("*");
      System.Console.WriteLine();
    }
  }

  public void rightTriangle(int x) {
    if(x <= 0) {
      System.Console.Write("\nНеверно введёная сторона.\n");
      return;
    }
    for(int i=0; i<x; ++i) {
      for(int j=0; j<x; ++j) {
        if(i+j >= x-1)
          System.Console.Write("*");
        else
          System.Console.Write(" ");
      }
      System.Console.WriteLine();
    }
  }

  public int findFirst(int[] arr, int x) {
    for(int i=0; i<arr.Length; ++i)
      if(arr[i] == x)
        return i;
    return -1;
  }

  public int maxAbs(int[] arr) {
    int max = 0;
    for(int i=0; i<arr.Length; ++i)
      if(absol(arr[i]) > absol(max))
        max = arr[i];
  
    return max;
  }

  public int[] add(int[] arr, int[] ins, int pos) {
    pos = clamp(pos, 0, arr.Length);
    int[] result = new int[arr.Length + ins.Length];
    for(int i=0; i<pos; ++i)
      result[i] = arr[i];
  
    for(int i=0; i<ins.Length; ++i)
      result[pos + i] = ins[i];
  
    for(int i=0; i+pos<arr.Length; ++i)
      result[pos+ins.Length + i] = arr[pos + i];

    return result;
  }

  public int[] reverseBack(int[] arr) {
    int[] result = new int[arr.Length];

    for(int i=arr.Length-1; i>=0; --i)
      result[arr.Length-1 - i] = arr[i]; 
    
    return result;
  }

  public int[] findAll(int[] arr, int x) {
    int len = 0;
    for(int i=0; i<arr.Length; ++i)
      if(arr[i] == x)
        ++len;
    if(len == 0)
     return new int[1] {-1}; 

    int[] result = new int[len];
    len = 0;
    for(int i=0; i<arr.Length; ++i)
      if(arr[i] == x)
        result[len++] = i;

    return result;
  }
}
