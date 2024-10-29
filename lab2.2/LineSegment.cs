class LineSegment {
  private double x;
  private double y;

  public double X {
    get => x;
    set {
      x = min(value, y);
      y = max(value, y);
    }
  }
  public double Y {
    get => y;
    set { 
      y = max(value, x);
      x = min(value, x);
    }
  }

  public LineSegment(double _x, double _y) {
    x = min(_x, _y);
    y = max(_x, _y);
  }

  static public bool intersect(LineSegment ls1, LineSegment ls2) {
    if(ls1.X > ls2.X)
      return intersect(ls2, ls1);
    if( !(ls1.Y < ls2.X && ls1.X < ls2.Y) )
      return true;
    return false;
  }

  static public double operator!(LineSegment ls) {
    return ls.Y - ls.X;
  }

  static public LineSegment operator++(LineSegment ls) {
    ls.Y = ls.Y + 1;
    ls.X = ls.X - 1;
    return ls;
  }

  static public LineSegment operator-(int _x, LineSegment ls) {
    return new LineSegment(ls.X-_x, ls.Y);
  }

  static public LineSegment operator-(LineSegment ls, int _y) {
    return new LineSegment(ls.X, ls.Y - _y);
  }

  public override string ToString() {
    return $"({x},{y})";
  }

  static public bool operator<(LineSegment ls1, LineSegment ls2) {
    return intersect(ls1, ls2);
  }
  static public bool operator>(LineSegment ls1, LineSegment ls2) {
    return intersect(ls1, ls2);
  }

  static public implicit operator int(LineSegment ls) {
    double res = ls.X;
    if(res < 0) res = -res;
    if(Convert.ToInt32(res) > res)
      return Convert.ToInt32(res) - 1;
    return Convert.ToInt32(res);
  }

  static public explicit operator double(LineSegment ls) { 
    return ls.Y;
  }

  static private double min(double a, double b) {
    if(a < b)
      return a;
    return b;
  }

  static private double max(double a, double b) {
    if(a > b)
      return a;
    return b;
  }
}
