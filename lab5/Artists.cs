class Artists {
  private int id;
  private readonly string name;

  public int ID {get => ID;}
  public string Name {get => name;}

  Artists(int _ID, string _name) {
    id = _ID;
    name = _name;
  }

  public override string ToString() {
    return $"{id}\t{name}";
  }
}
