using NPOI.SS.UserModel;

class Artists : ITable {
  private int id;
  private readonly string name;

  public int ID {get => id;}
  public string Name {get => name;}

  public Artists(int _ID, string _name) {
    id = _ID;
    name = _name;
  }

  public Artists(IRow row) {
    if(row.GetCell(0) != null)
      id = int.Parse(row.GetCell(0).ToString());
    if(row.GetCell(1) != null)
      name = row.GetCell(1).ToString();
  }

  public override string ToString() {
    return $"{id}\t{name}";
  }
}
