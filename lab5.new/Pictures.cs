using NPOI.SS.UserModel;

class Pictures : ITable {
  private int id;
  private readonly string name;
  private int artist_id;
  private int part;
  private readonly string year;
  private int style_id;

  public int ID {get => id;}
  public string Name {get => name;}
  public int ArtistID {get => artist_id;}
  public int Part {get => part;}
  public string Year {get => year;}
  public int StyleID {get => style_id;}

  public Pictures(int _ID, string _name, int _artist_id, int _part, string _year, int _style_id) {
    id = _ID;
    name = _name;
    artist_id = _artist_id;
    part = _part;
    year = _year;
    style_id = _style_id;
  }
  public Pictures(IRow row) {
    if(row.GetCell(0) != null)
      id = int.Parse(row.GetCell(0).ToString());
    if(row.GetCell(1) != null)
      name = row.GetCell(1).ToString();
    if(row.GetCell(2) != null)
      artist_id = int.Parse(row.GetCell(2).ToString());
    if(row.GetCell(3) != null)
    part = int.Parse(row.GetCell(3).ToString());
    if(row.GetCell(4) != null)
      year = row.GetCell(4).ToString();
    if(row.GetCell(5) != null)
      style_id = int.Parse(row.GetCell(5).ToString());
  }

  public Pictures(int _id, string data) {
    id = _id;
    name = data.Split(';')[0];
    artist_id = int.Parse(data.Split(';')[1]);
    part = int.Parse(data.Split(';')[2]);
    if(data.Split(';').Count() >= 4)
      year = data.Split(';')[3];
    if(data.Split(';').Count() == 5)
      style_id = int.Parse(data.Split(';')[4]);
  }

  public override string ToString() {
    return $"{id}\t{name}\t{artist_id}\t{part}\t{year}\t{style_id}";
  }
}
