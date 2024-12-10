// class DoubleFraction : IFraction {
//     private Fraction fraction;
//     private double cachedValue;

//     public DoubleFraction(int numerator, int denumerator) {
//         fraction = new(numerator, denumerator);
//         calcVal();
//     }

//     public int Numerator{get => fraction.Numerator; set {
//         fraction.Numerator = value;
//         calcVal();
//     }}

//     public int Denumerator{get => fraction.Denumerator; set {
//         fraction.Denumerator = value;
//         calcVal();
//     }}

//     public double Value() => cachedValue;

//     public object Clone() {
//         return new DoubleFraction(fraction.Numerator, fraction.Denumerator);
//     }

//     private void calcVal() {
//         cachedValue = fraction.Numerator/fraction.Denumerator;
//     }
// }