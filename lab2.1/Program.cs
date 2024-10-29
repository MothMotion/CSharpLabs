using System;

internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("Automatic testing\n");

    TwoBool b1 = new TwoBool(true,false);
    Console.WriteLine($"Testing TwoBool:\n{b1}");
    b1.hasPas = true;
    Console.WriteLine($"He has got a passport:\n{b1}");
    Console.WriteLine($"dis(b1) = {b1.dis()}\n");
    TwoBool b2 = new TwoBool(b1);
    Console.WriteLine($"Testing copying TwoBool:\nb1 = {b1};\nb2 = {b2}\n\n\n");

    Console.WriteLine("Creating civil:\n");
    Civilian c1 = new Civilian("TestName", 15, false, false);
    Console.WriteLine($"Civilian: {c1}\nIs he should get passport: {c1.shouldGetPass()}\nIs he really civil: {c1.isCivil()}\n");
  
    Console.WriteLine("Creating civil with incorrect age:\n");
    c1 = new Civilian("TestName2", -8, true, false);
    Console.WriteLine($"Civilian: {c1}\nIs he should get passport: {c1.shouldGetPass()}\nIs he really civil: {c1.isCivil()}\n");
    Console.WriteLine("Automatic test complete\n\n\n");
  }
}
