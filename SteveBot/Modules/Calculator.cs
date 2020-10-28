using System;
using System.Collections.Generic;

namespace SteveBot.Modules
{
    
    static class Calculator
    {
        /// <summary>
        /// Takes a full equation and calculates it
        /// </summary>
        /// <param name="_Input"></param>
        /// <returns></returns>
        public static double Complex_Equation(string input)
        {
            //Take The equation, Break it down by bedmas
            //
            input = input.ToLower();
            double output = 0;
            List<double> Numbers = new List<double>();
            List<string> Enumerators = new List<string>();

            /*
            //Comma Seperated; First number is starting Bracket, Second is End Bracket
            List<string> EquationsSorted = new List<string>();
            //Tracks brackets
            int brackStartNum = 0;
            int brackEndNum = 0;
            int EQSPos = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input.Substring(i, 1) == "(")
                {
                    EquationsSorted.Add(i.ToString());
                    brackStartNum++;

                    for (int x = 0; x < input.Length - i; x++)
                    {
                        if (input.Substring(x, 1) == ")")
                        {
                            EquationsSorted[EQSPos] += "," + (x + i);
                            EQSPos++;
                        }
                        else
                            continue;
                    }
                }
                else if (input.Substring(i, 1) == ")")
                {
                    brackEndNum++;
                }
            }
           // if (brackStartNum != brackEndNum)
           //     return 0.0;
            */
            foreach (char seg in input)
            {
                string segment = seg.ToString();
                if (segment == "+" || segment == "-" || segment == "x" || segment == "/" || segment == "^" || segment == "*" || segment == "!")
                {
                    Enumerators.Add(segment);
                }
                else
                {
                    if (double.TryParse(segment, out double tmp))
                        Numbers.Add(tmp);
                }
            }
            for (int i = 0; i < Enumerators.Count; i++)
            {
                if (Enumerators[i] != "!")
                    output = edmas(Enumerators[i], Numbers[i], Numbers[i + 1]);
                else
                    output = edmas(Enumerators[i], Numbers[i]);
            }
            return output;
        }


        private static double edmas(string Enumerators, double Num1, double Num2 = 0)
        {
            if (Enumerators == "^")
            {
                Num2 = Num1 * Num2;
            }
            else if (Enumerators == "/")
            {
                Num2 = div(Num1, Num2);
            }
            else if (Enumerators == "x" || Enumerators == "*")
            {
                Num2 = mult(Num1, Num2);
            }
            else if (Enumerators == "+")
            {
                Num2 = add(Num1, Num2);
            }
            else if (Enumerators == "-")
            {
                Num2 = sub(Num2, Num1);
            }
            else if (Enumerators == "!")
            {
                double.TryParse(Fact(Convert.ToInt16(Num1)),out Num2);
            }
            return Num2;
        }

        #region Addition
        /// <summary>
        /// Takes two Integers and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static int add(int Num1, int Num2)
        {
            int Output = Num1 + Num2;
            return Output;
        }
        /// <summary>
        /// Takes one Integer and one double and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double add(int Num1, double Num2)
        {
            double Output = Num1 + Num2;
            return Output;
        }
        /// <summary>
        /// Takes one double and one Integer and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double add(double Num1, int Num2)
        {
            double Output = Num1 + Num2;
            return Output;
        }
        /// <summary>
        /// Takes two doubles and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double add(double Num1, double Num2)
        {
            double Output = Num1 + Num2;
            return Output;
        }
        #endregion Addition

        #region Subtraction
        /// <summary>
        /// Takes two Integers and returns Num2 minus Num1
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static int sub(int Num1, int Num2)
        {
            int Output = Num2 - Num1;
            return Output;
        }
        /// <summary>
        /// Takes one int and one double and returns Num2 minus Num1
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double sub(int Num1, double Num2)
        {
            double Output = Num2 - Num1;
            return Output;
        }
        /// <summary>
        /// Takes one double and one int and returns Num2 minus Num1
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double sub(double Num1, int Num2)
        {
            double Output = Num2 - Num1;
            return Output;
        }
        /// <summary>
        /// Takes two doubles and returns Num2 minus Num1
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double sub(double Num1, double Num2)
        {
            double Output = Num2 - Num1;
            return Output;
        }
        #endregion Subtraction

        #region Division
        /// <summary>
        /// Takes two ints and returns Num1 over Num2
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static int div(int Num1, int Num2)
        {
            if (Num2 == 0)
                return 0;
            int Output = Num1 / Num2;
            return Output;
        }
        /// <summary>
        /// Takes one int and one double and returns Num1 over Num2
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double div(int Num1, double Num2)
        {
            if (Num2 == 0)
                return 0;
            double Output = Num1 / Num2;
            return Output;
        }
        /// <summary>
        /// Takes one double and one int and returns Num1 over Num2
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double div(double Num1, int Num2)
        {
            if (Num2 == 0)
                return 0;
            double Output = Num1 / Num2;
            return Output;
        }
        /// <summary>
        /// Takes two doubles and and returns Num1 over Num2
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double div(double Num1, double Num2)
        {
            if (Num2 == 0)
                return 0;
            double Output = Num1 / Num2;
            return Output;
        }
        #endregion Division

        #region Multiplication
        /// <summary>
        /// Takes two ints and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static int mult(int Num1, int Num2)
        {
            int Output = Num1 * Num2;
            return Output;
        }
        /// <summary>
        /// Takes one int and one double and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double mult(int Num1, double Num2)
        {
            double Output = Num1 * Num2;
            return Output;
        }
        /// <summary>
        /// Takes one double and one int and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double mult(double Num1, int Num2)
        {
            double Output = Num1 * Num2;
            return Output;
        }
        /// <summary>
        /// Takes two doubles and and returns their sum
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static double mult(double Num1, double Num2)
        {
            double Output = Num1 * Num2;
            return Output;
        }
        #endregion Multiplication

        #region Conversions
        /// <summary>
        /// Takes a number and converts it to Hexcidecimal
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string dec2hex(int Input)
        {
            string tmp = Input.ToString("X");
            return tmp;
        }
        /// <summary>
        /// Takes a string and returns a decimal type
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static int hex2dec(string Input)
        {
            int tmp = Convert.ToInt32(Input, 16);
            return tmp;
        }
        /// <summary>
        /// Returns the Factorial of Input
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Fact(int Input)
        {
            if (Input < 0)
                return "Cannot Factorial a Negative!";
            ulong total = 1;
            for (ulong i = 1; i <= Convert.ToUInt64(Input); i++)
                total *= i;
            return total.ToString();
        }
        #endregion Conversions
    }
}