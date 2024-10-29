public class Civilian : TwoBool {
  private string name;
  private int age;

  public string Name {get => name;}
  public int Age {get => age;set => age = max(0, value);} 

  public Civilian(string _name, int _age, bool _isLocal, bool _hasPas)
    : base(_isLocal, _hasPas) {
    this.name = _name;
    this.age = max(0, _age);
  }

  public bool isCivil() {
    return dis();
  }

  public bool shouldGetPass() {
    return isLocal && (age >= 14);
  }

  public override string ToString() {
    return $"{name} {age}y";
  }

  private int max(int num1, int num2) {
    if(num1 > num2)
      return num1;
    return num2;
  }
}
