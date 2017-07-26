using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCalc
{
    public class Fraction
    {
        public static readonly Fraction Zero = new Fraction();
        public static readonly Fraction One = new Fraction(1);

        public long A { get; set; }
        public long B { get; set; }

        public Fraction() : this(0) { }
        public Fraction(long A) : this(A, 1) { }
        public Fraction(long A, long B)
        {
            this.A = A;
            this.B = B;
        }

        public static Fraction operator +(Fraction A, Fraction B)
        {
            return FRMath.Add(A, B);
        }

        public static Fraction operator -(Fraction A, Fraction B)
        {
            return FRMath.Substract(A, B);
        }

        public static Fraction operator *(Fraction A, Fraction B)
        {
            return FRMath.Multiply(A, B);
        }

        public static Fraction operator /(Fraction A, Fraction B)
        {
            return FRMath.Divide(A, B);
        }

        public static bool operator <(Fraction A, Fraction B)
        {
            return FRMath.LessThen(A, B);
        }

        public static bool operator >(Fraction A, Fraction B)
        {
            return FRMath.BiggerThen(A, B);
        }

        public static implicit operator Fraction(string Value)
        {
            int S = Value.IndexOf("/");
            string A = S == -1 ? Value : Value.Substring(0, S);
            string B = S == -1 ? "1" : Value.Substring(S + 1);

            int An = int.Parse(A);
            int Bn = int.Parse(B);

            return new Fraction(An, Bn);
        }

        public static implicit operator double(Fraction Value)
        {
            return Value.A / Value.B;
        }

        public static implicit operator Fraction(double Value)
        {
            string VStr = Value.ToString();
            int I = VStr.IndexOf(',');

            Fraction F;
            if (I > -1)
            {
                int N = VStr.Substring(I).Length;
                long L = Convert.ToInt64(Math.Pow(10, N));
                long A = Convert.ToInt64(Value * L);

                F = new Fraction(A, L);
            }
            else
                F = new Fraction(Convert.ToInt64(Value));
            return FRMath.AutoFactor ? FRMath.Factor(F) : F;
        }

        public override bool Equals(object obj)
        {
            Fraction F = obj as Fraction;
            if (F != null)
                return F.A == A && F.B == B;
            return false;
        }

        public override int GetHashCode()
        {
            return A.GetHashCode() ^ B.GetHashCode();
        }

        public override string ToString()
        {
            if (A == 0)
                return "0";

            string V = A < 0 && B > 0 || A > 0 && B < 0 ? "-" : "";
            V += Convert.ToString(Math.Abs(A));
            if (B != 1)
                V += "/" + Convert.ToString(Math.Abs(B));
            return V;
        }
    }
}