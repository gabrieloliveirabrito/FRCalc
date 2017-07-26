using System;

namespace FRCalc
{
    public static class FRMath
    {
        public static bool AutoFactor { get; set; }

        static FRMath()
        {
            AutoFactor = true;
        }

        public static Fraction Add(Fraction First, Fraction Second)
        {
            if (Second < Fraction.Zero)
                return Substract(First, Absolute(Second));

            long An = First.A * Second.B + First.B * Second.A;
            long Bn = First.B * Second.B;

            Fraction F = new Fraction(An, Bn);
            return AutoFactor ? Factor(F) : F;
        }

        public static Fraction Substract(Fraction First, Fraction Second)
        {
            if (First.A == Second.A && First.B == Second.B)
                return Fraction.Zero;
            else
            {
                long An = First.A * Second.B - First.B * Second.A;
                long Bn = First.B * Second.B;

                Fraction F = new Fraction(An, Bn);
                return AutoFactor ? Factor(F) : F;
            }
        }

        public static Fraction Multiply(Fraction First, Fraction Second)
        {
            if (First.A == 0 || Second.A == 0)
                return Fraction.Zero;

            long An = First.A * Second.A;
            long Bn = First.B * Second.B;

            Fraction F = new Fraction(An, Bn);
            return AutoFactor ? Factor(F) : F;
        }

        public static Fraction Divide(Fraction First, Fraction Second)
        {
            if (First == Second)
                return Fraction.One;

            return Multiply(First, Inverse(Second));
        }

        public static Fraction Inverse(Fraction F)
        {
            return new Fraction(F.B, F.A);
        }

        public static Fraction Absolute(Fraction F)
        {
            return new Fraction(Math.Abs(F.A), Math.Abs(F.B));
        }

        public static double GCM(Fraction F)
        {
            return F.B == 0 ? F.A : GCM(new Fraction(F.B, F.A % F.B));
        }

        public static Fraction Factor(Fraction F)
        {
            if (F.A == 0)
                return Fraction.Zero;
            else if (F.A == F.B)
                return Fraction.One;

            double gcm = GCM(F);
            return new Fraction(Convert.ToInt64(F.A / gcm), Convert.ToInt64(F.B / gcm));
        }

        public static bool LessThen(Fraction First, Fraction Second)
        {
            return First.A / First.B < Second.A / Second.B;
        }

        public static bool BiggerThen(Fraction First, Fraction Second)
        {
            return First.A / First.B > Second.A / Second.B;
        }
    }
}