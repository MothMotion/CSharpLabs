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
    using(StreamReader sr = new StreamReader(path)) {
      HashSet<char> uniqueSym = new HashSet<char>();
      char temp = (char)sr.Read();
      while(temp != null) {
        if(!uniqueSym.Contains(temp))
          uniqueSym.Add(temp);
      }
      Console.WriteLine("В файле есть следующие уникальные символы:");
      foreach(char c in uniqueSym)
        Console.Write($"{c} ");
      Console.WriteLine();
    }
  }

  private static bool hasVal<T>(List<T> L, T v) {
    foreach (T val in L)
      if(v.Equals(val))
        return true;
    return false;
  }
}
