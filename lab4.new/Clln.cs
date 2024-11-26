using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Clln<T> {
  public static List<T> intersect(List<T> L1, List<T> L2) {
    List<T> res = new List<T>();
    foreach (T val in L1)
      if(hasVal(L2, val) && !hasVal(res, val))
        res.Add(val);
    return res;
  }

  public static LinkedList<T> addReverse(LinkedList<T> LL) {
    LinkedList<T> res = new LinkedList<T>(LL);
    for(LinkedListNode<T> i=LL.Last; i != null; i=i.Previous)
      res.AddLast(i.Value);
    return res;
  }

  public static void checkAtten(HashSet<string> mall, HashSet<string>[] stud) {
    bool valid;

    foreach(string ml in mall) {
      valid = true;
      foreach(HashSet<string> hs in stud) {
        if(!hs.Contains(ml)) {
          valid = false;
          break;
        }
      }
      if(valid)
        Console.WriteLine($"В ТРЦ {ml} ходили все.");
    }

    foreach(string ml in mall) {
      valid = false;
      foreach(HashSet<string> hs in stud) {
        if(hs.Contains(ml)) {
          valid = true;
          break;
        }
      }
      if(valid)
        Console.WriteLine($"В ТРЦ {ml} ходил хотя-бы один студент.");
      if(!valid)
        Console.WriteLine($"В ТРЦ {ml} никто не ходил.");
    }
  }

  public static void checkSym(string path) {
    using(TextReader sr = new StreamReader(path)) {
      if(sr == null) {
        Console.WriteLine("Невозможно открыть файл.");
        return;
      }
      
      HashSet<char> uniqueSym = new HashSet<char>();
      string temp = sr.ReadLine(); // <-- костыль
      while(temp != null) {
        foreach(char c in temp)
          if(isValid(c) && !uniqueSym.Contains(c))
            uniqueSym.Add(c);
        temp = sr.ReadLine();
      }

      Console.WriteLine($"Количество уникальных символов в файле: {uniqueSym.Count}:");
      foreach(char c in uniqueSym)
        Console.Write($"\"{c}\" ");
      Console.WriteLine();
    }
  }

  private static bool hasVal<T>(List<T> L, T v) {
    foreach (T val in L)
      if(v.Equals(val))
        return true;
    return false;
  }

  private static bool isValid(char c) {
    if((int)'а' <= (int)c && (int)c <= (int)'Я' || c == 'ё' || c == 'Ё')
      return true;
    return false;
  }
}
