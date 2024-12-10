class Fraction : IFraction {
    private int numerator;
    private int denumerator;
    private double cachedValue;
    public int Numerator{get => numerator; set {numerator = denumerator; simplify();}}
    public int Denumerator{get => denumerator; set {
        if(value == 0) throw new Exception("0 не может быть знаменателем");
        if(value < 0) {
            numerator *= -1;
            value *= -1;
        }
        denumerator = value;
        simplify();
        }}
    
    public Fraction(int _numerator, int _denumerator) {
        if(_denumerator == 0)
            throw new Exception("0 не может быть знаменателем");
        numerator = _numerator;
        if(_denumerator < 0) {
            numerator *= -1;
            _denumerator *= -1;
        }
        denumerator = _denumerator;
        simplify();
    }

    public Fraction(int a) {
        numerator = a;
        denumerator = 1;
        cachedValue = a;
    }

    private void simplify() {
        int nod = GCD(Math.Abs(numerator), denumerator);
        numerator /= nod;
        denumerator /= nod;
        cachedValue = numerator/denumerator;
    }

    private static int GCD(int a, int b) { // НОД
        while(a != b && a != 0 && b != 0)
            if(a>b) a %= b;
            else b %= a;
        return (a != 0) ? a : b;
    }

    private static int LCM(int a, int b) { // НОК
        return a*b/GCD(a,b);   
    }

    private Fraction reverse() {
        if(numerator == 0)
            return this;
        if(numerator < 0)
            return new Fraction(-denumerator, numerator);
        return new Fraction(denumerator, numerator);
    }

    public static Fraction operator+(Fraction fr1, Fraction fr2) {
        int lcm = LCM(fr1.denumerator, fr2.denumerator);
        return new Fraction(fr1.numerator * lcm / fr1.denumerator + fr2.numerator * lcm / fr2.denumerator, lcm);
    }
    public static Fraction operator*(Fraction fr1, Fraction fr2) {
        return new Fraction(fr1.numerator * fr2.numerator, fr1.denumerator * fr2.denumerator);
    }
    public static Fraction operator-(Fraction fr1, Fraction fr2) {
        return fr1 + (-fr2);
    }
    public static Fraction operator/(Fraction fr1, Fraction fr2) {
        return fr1 * fr2.reverse();
    }

    public static Fraction operator-(Fraction fr) {
        return -1 * fr;
    }

    public static implicit operator Fraction(int a) {
        return new Fraction(a);
    }

    public static bool operator==(Fraction fr1, Fraction fr2) {
        return (fr1.numerator == fr2.numerator) && (fr1.denumerator == fr2.denumerator);
    }
    public static bool operator!=(Fraction fr1, Fraction fr2) {
        return !(fr1 == fr2);
    }

    public object Clone() {
        return new Fraction(numerator, denumerator);
    }

    public double Value() {
        return 0;
    }

    public override string ToString() {
        return $"{numerator}/{denumerator}";
    }
}