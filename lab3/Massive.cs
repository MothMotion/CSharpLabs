using System;

class Massive {
  public int[,] m;

  public Massive(int x, int y) {
    m = new int[x,y];

    for(int j=0; j < m.GetLength(1); ++j)
      for(int i=m.GetLength(0)-1; i >= 0; --i)
        m[i,j] = securedInt("Введите целое число:");
  }

  public Massive(int x, int y, bool tot) {
    m = new int[x,y];

    if(!tot) {
      Random rng = new Random();
      for(int i=0; i < m.GetLength(0); ++i)
        for(int j=0; j < m.GetLength(1); ++j)
          m[i,j] = rng.Next(m[max(i-1, 0), j]+1, m[max(i-1, 0), j]+101);
    } else {
      int counter = x*y;
      int num = 2;

      while(counter > 0) {
        m[(x*y-counter)/y, (x*y-counter)%y] = num;
        num = getNext(num+1);
        --counter; 
      }
    }
  }

  public void checkMarks() {
    double average;
    for(int i=0; i<m.GetLength(0); ++i) {
      average = 0;
      for(int j=0; j<m.GetLength(1); ++j)
        average += m[i,j];
      average /= m.GetLength(1);

      if(average > 4.5)
        System.Console.WriteLine($"Номер школьника: {i}");
    }
    System.Console.WriteLine();
  }

  static public Massive operator*(int a, Massive M) {
    for(int i=0; i<M.m.GetLength(0); ++i)
      for(int j=0; j<M.m.GetLength(1); ++j)
        M.m[i,j] *= a;
    return M;
  }

  static public Massive operator+(Massive M1, Massive M2) {
    if(M1.m.GetLength(0) != M2.m.GetLength(0) || M1.m.GetLength(1) != M2.m.GetLength(1)) {
      System.Console.WriteLine("Невозможная операция.");
      return M1;
    }
    for(int i=0; i<M1.m.GetLength(0); ++i)
      for(int j=0; j<M1.m.GetLength(1); ++j)
        M1.m[i,j] += M2.m[i,j];
    return M1;
  }

  static public Massive operator-(Massive M1, Massive M2) {
    return M1 + ( (-1) * M2 );
  }

  public static Massive transform(Massive M) {
    if(M.m.GetLength(0) != M.m.GetLength(1)) {
      System.Console.WriteLine("Неверная размерность матрицы для трансформации.");
      return M;
    }
    int temp;
    for(int i=0; i<M.m.GetLength(0); ++i)
      for(int j=i+1; j<M.m.GetLength(1); ++j) {
        temp = M.m[i,j];
        M.m[i,j] = M.m[j,i];
        M.m[j,i] = temp;
      }
    return M;
  }

  private static int max(int a, int b) {
    if(a>b)
      return a;
    return b;
  }

  private static int getNext(int num) {
    bool flag = false;
    while(!flag) {
      flag = true;
      for(int i=2; i*i<=num && flag; ++i)
        if(num%i == 0) {
          flag = false;
          ++num;
        }
    }
    return num;
  }
  
  public void print() {
    for(int i=0; i<m.GetLength(0); ++i) {
      for(int j=0; j<m.GetLength(1); ++j)
        System.Console.Write($"{m[i,j]}\t");
      System.Console.WriteLine();
    }
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
