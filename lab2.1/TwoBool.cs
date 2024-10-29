public class TwoBool{
  private bool isloc;
  private bool hasps;

  public bool isLocal {get => isloc;}
  public bool hasPas {get => hasps; set => hasps = value;}

  public TwoBool(bool _isLocal, bool _hasPass) {
    isloc = _isLocal;
    hasps = _hasPass;
  }

  public TwoBool(TwoBool tb) {
    isloc = tb.isLocal;
    hasps = tb.hasPas;
  }

  public bool dis() {
    return isloc || hasps;
  }

  public override string ToString() {
    return $"{isloc}, {hasps}";
  }
}
