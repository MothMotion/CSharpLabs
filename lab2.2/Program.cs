using System;

internal class Program {
  private static void Main(string[] args) {
    double d1 = getDouble("Введите x для первого LineSegment:");
    double d2 = getDouble("Введите y для первого LineSegment:");
    LineSegment ls1 = new LineSegment(d1, d2);
    Console.WriteLine($"Первый LineSegment имеет следующие значения:\n{ls1}");
    d1 = getDouble("Введите x для второго LineSegment:");
    d2 = getDouble("Введите y для второго LineSegment:");
    LineSegment ls2 = new LineSegment(d1, d2);
    Console.WriteLine($"Второй LineSegment имеет следующие значения:\n{ls2}"); 

    Console.WriteLine($"Пересекаются ли отрезки:\nМетод intersect(): {LineSegment.intersect(ls1, ls2)}\nls1<ls2: {ls1<ls2}\n\nДлина ls1: {!ls1}\n; Расширяем ls2: {++ls2}\n");
    int x = ls2;
    Console.WriteLine($"Неявное преобразование в int для ls2: {x};\nЯвное преобразование в double ls2: {(double)ls2}\n");
    d1 = getDouble("Введите вещественное число для левостороннего вычитания из ls1:");
    d2 = getDouble("Введите вещественное число для правостороннего вычитания из ls1:");
    Console.WriteLine($"ls1-d1: {ls1-d1}\nd2-ls1: {d2-ls1}");
  }

  private static double getDouble(string message) {
    String inp;
    bool isValid;
    bool dotWas;
    
    do {
      isValid = true;
      dotWas = false;
      Console.WriteLine(message);

      inp = Console.ReadLine();

      for(int i=Convert.ToInt32(inp[0] == '-'); i<inp.Length && isValid; ++i) {
        if(!checkSym(inp[i]) || dotWas && inp[i] == '.') {
          isValid = false;
          if(inp[i] == '.')
            Console.WriteLine("В числе не может быть 2 знака \".\"");
        }
        if(inp[i] == '.')
          dotWas = true;
        }
    } while(!isValid && inp.Length > Convert.ToInt32(inp[0] == '-' ));

    return Convert.ToDouble(inp);
  }

  private static bool checkSym(char ch) {
    if(ch == '.' || Convert.ToInt32(ch) >= Convert.ToInt32('0') && Convert.ToInt32(ch) <= Convert.ToInt32('9'))
      return true;
    Console.WriteLine($"Cимвол {ch} - не является цифрой, \"-\" в начале или \".\"\n");
    return false;
  }
}
