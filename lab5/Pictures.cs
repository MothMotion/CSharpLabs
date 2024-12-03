class Pictures{
  private int id;
  private readonly string name;
  private int artist_id;
  private int part;
  private int year;
  private int style_id;

  public int ID {get => id;}
  public string Name {get => name;}
  public int ArtistID {get => artist_id;}
  public int Part {get => part;}
  public int Year {get => year;}
  public int StyleID {get => style_id;}

  Pictures(int _ID, string _name, int _artist_id, int _part, int _year, int _style_id) {
    id = _ID;
    name = _name;
    artist_id = _artist_id;
    part = _part;
    year = _year;
    style_id = _style_id;
  }

  public override string ToString() {
    return $"{id}\t{name}\t{artist_id}\t{part}\t{year}\t{style_id}";
  }
}
