using System;
using System.Collections.Generic;

namespace SteveBot.Modules
{
    
    static class Calculator
    {
        /// <summary>
        /// Takes a full equation and calculates it (Splits on comma or space)
        /// </summary>
        /// <param name="_Input"></param>
        /// <returns></returns>
        public static double Complex_Equation(string input)
        {
            string[] tmp = input.Split(',');
            if(tmp.Length == 0)
               return double.NaN;
            if(tmp.Length == 1) //Splits on space
                tmp = input.Split(' ');
            
            string operator1, operator2, result = null;
            Stack<string> stack = new Stack<string>();

            for(int i = 0; i < tmp.Length; i++)
            {
                string Token = tmp[i];
                if (Token == "+" || Token == "-" || Token == "*" || Token == "/")
                {
                    operator2 = stack.Pop();
                    operator1 = stack.Pop();

                    switch (Token)
                    {
                        case "+":
                            result = (Convert.ToDouble(operator1) + Convert.ToDouble(operator2)).ToString();
                            break;
                        case "-":
                            result = (Convert.ToDouble(operator1) - Convert.ToDouble(operator2)).ToString();
                            break;
                        case "*":
                            result = (Convert.ToDouble(operator1) * Convert.ToDouble(operator2)).ToString();
                            break;
                        case "/":
                            result = (Convert.ToDouble(operator1) / Convert.ToDouble(operator2)).ToString();
                            break;
                    }

                    stack.Push(result);
                }
                else if (double.TryParse(Token, out double res))
                    stack.Push(Token);
            }

            return Convert.ToDouble(stack.Pop());
        }

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
        public static string Factorial(int Input)
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