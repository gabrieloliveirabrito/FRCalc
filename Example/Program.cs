using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FRCalc;
using System.ComponentModel;
using System.Reflection;

namespace Example
{
    class Program
    {
        static List<Tuple<string, Action>> Operations = new List<Tuple<string, Action>>();
        static void Main(string[] args)
        {
            foreach (var Method in typeof(Program).GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(M => M.GetParameters().Length == 0))
            {
                DescriptionAttribute Desc = (Method.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute);
                if (Desc != null)
                    Operations.Add(new Tuple<string, Action>(Desc.Description, () => Method.Invoke(null, null)));
            }

            Show();
            while (true)
            {
                Console.WriteLine();
                Console.Write(">> Enter an valid option: ");
                try
                {
                    int OP = -1;
                    if (!int.TryParse(Console.ReadLine(), out OP) || OP < 0 || OP > Operations.Count)
                        Console.WriteLine("Enter a valid operation!");
                    else if (OP == 0)
                        break;
                    else
                        Operations[OP - 1].Item2.Invoke();
                }
                catch (Exception ex)
                {
                    string Message = ex.Message;
                    if (ex.InnerException != null)
                        Message = ex.InnerException.Message;
                    Console.WriteLine(Message);
                }
            }
        }

        static Fraction Read()
        {
            Console.Write("Enter a number or a fraction: ");
            return Console.ReadLine();
        }

        [Description("Show this menu")]
        static void Show()
        {
            Console.WriteLine("Automatic factor it is {0}", FRMath.AutoFactor ? "enabled" : "disabled");

            int i = 1;
            foreach (var Operation in Operations)
                Console.WriteLine("{0} - {1}", i++, Operation.Item1);
            Console.WriteLine("0 - Exit");
        }

        [Description("Add number")]
        static void Add()
        {
            Fraction A = Read(), B = Read();
            Console.WriteLine("Result: {0}", A + B);
        }

        [Description("Subtract number")]
        static void Sub()
        {
            Fraction A = Read(), B = Read();
            Console.WriteLine("Result: {0}", A - B);
        }

        [Description("Multiply number")]
        static void Mul()
        {
            Fraction A = Read(), B = Read();
            Console.WriteLine("Result: {0}", A * B);
        }

        [Description("Divide number")]
        static void Div()
        {
            Fraction A = Read(), B = Read();
            Console.WriteLine("Result: {0}", A / B);
        }

        [Description("Factor number")]
        static void Fact()
        {
            Fraction A = Read();
            Console.WriteLine("Result: {0}", FRMath.Factor(A));
        }

        [Description("Absolute number")]
        static void Abs()
        {
            Fraction A = Read();
            Console.WriteLine("Result: {0}", FRMath.Absolute(A));
        }

        [Description("Toggle factor")]
        static void ToggleFactor()
        {
            FRMath.AutoFactor = !FRMath.AutoFactor;
            Console.WriteLine("Automatic factor it is {0}", FRMath.AutoFactor ? "enabled" : "disabled");
        }

        [Description("Convert an decimal to fraction")]
        static void ConvertDecimal()
        {
            Console.Write("Enter a valid decimal(ex 0,5): ");
            double Decimal = 0;

            if (!double.TryParse(Console.ReadLine(), out Decimal))
                Console.WriteLine("Invalid decimal!");
            else
                Console.WriteLine("Result: {0}", (Fraction)Decimal);
        }
    }
}