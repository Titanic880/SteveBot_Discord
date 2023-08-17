using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
            char[] ops = {'^', '*', '/', '-', '+' };
            //Break and check for math information
            List<string> tmp = new List<string>();
            string bracket = "";
            bool brack = false;
            string placeholder = "";
            foreach (char a in input)
            {
                //Check for Bracket
                if (a == '(')
                {
                    brack = true;
                    //x() auto complete
                    if (placeholder != "")
                    {
                        tmp.Add(placeholder);
                        placeholder = "";
                        tmp.Add('*'.ToString());
                    }
                    continue;
                }
                if (brack || a == ')')
                {
                    if (a == ')')
                    {
                        brack = false;
                        //Recursively solve the equation
                        tmp.Add(Complex_Equation(bracket).ToString());
                        continue;
                    }
                    else
                    {
                        bracket += a;
                        continue;
                    }
                }
                //Check for operator (allows bigger than 0-9 operations)
                if (ops.Contains(a))
                {
                    if (placeholder != "")
                    {
                        tmp.Add(placeholder);
                        placeholder = "";
                    }
                    tmp.Add(a.ToString());
                    continue;
                }
                placeholder += a;

            }
            //Catch Missing Values (Better option?)
            if (placeholder != "")
                tmp.Add(placeholder);


            if (tmp.Count == 0)
                return double.NaN;
            string value1, value2;


            while (tmp.Count > 1)
            {
                //Pemdas order (minus the Brackets)
                foreach (char op in ops)
                {
                    int index = tmp.IndexOf(op.ToString());
                    if (index < 0) continue;
                    //Fetch Values
                    value1 = tmp[index + 1];
                    value2 = tmp[index - 1];

                    tmp[index] = Calcsimple(value2,value1,op);
                    //Clean up Left over values
                    tmp.RemoveAt(index + 1);
                    tmp.RemoveAt(index - 1);
                }
            }
            return Convert.ToDouble(tmp[0]);
        }
        private static string Calcsimple(string value2, string value1, char op)
        {
            string result = "";
            switch (op)
            {
                case '+':
                    result = (Convert.ToDouble(value1) + Convert.ToDouble(value2)).ToString();
                    break;
                case '-':
                    result = (Convert.ToDouble(value1) - Convert.ToDouble(value2)).ToString();
                    break;
                case '*':
                    result = (Convert.ToDouble(value1) * Convert.ToDouble(value2)).ToString();
                    break;
                case '/':
                    result = (Convert.ToDouble(value2) / Convert.ToDouble(value1)).ToString();
                    break;
                case '^':
                    result = Math.Pow(Convert.ToDouble(value2), Convert.ToDouble(value1)).ToString();
                    break;
            }
            return result;
        }

        #region Conversions
        /// <summary>
        /// Takes a number and converts it to Hexcidecimal
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Dec2hex(int Input)
        {
            string tmp = Input.ToString("X");
            return tmp;
        }
        /// <summary>
        /// Takes a string and returns a decimal type
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static int Hex2dec(string Input)
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