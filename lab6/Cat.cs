class Cat : IMeow {
    private readonly string name;
    public string Name {get => name;}

    public Cat(string _name) {
        name = _name;
    }

    public void meow(int amount = 1) {
        if(amount < 1)
            return;
        Console.Write($"{name}: ");
        for(int i=0; i<amount-1; ++i)
            Console.Write("мяу-");
        Console.WriteLine("мяу!");
    }
}