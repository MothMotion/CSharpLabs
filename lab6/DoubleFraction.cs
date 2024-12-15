class DoubleFraction : IFraction {
    private Fraction fraction;
    private double cachedValue;

    public DoubleFraction(Fraction _fraction) {
        fraction = _fraction;
        cachedValue = 0d;
    }

    public int Numerator{set {
        fraction.Numerator = value;
        calcVal();
    }}

    public int Denumerator{set {
        fraction.Denumerator = value;
        calcVal();
    }}

    public double Value() {
        if(cachedValue == 0)
            calcVal();
        return cachedValue;
    }

    private void calcVal() {
        cachedValue = fraction.Numerator/fraction.Denumerator;
    }
}