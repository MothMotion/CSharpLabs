class Styles {
  private int id;
  private readonly string name;

  public int ID {get => id;}
  public string Name {get => name;}

  Styles(int _id, string _name) {
    id = _id;
    name = _name;
  }

  public override string ToString() {
    return $"{id}\t{name}";
  }
}
