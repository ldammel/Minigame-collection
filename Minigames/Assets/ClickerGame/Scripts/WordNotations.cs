using System;
using System.Collections.Generic;

namespace ClickerGame.Scripts
{
    public class WordNotations
    {
    
        public static object WordNotation(double number, string digits)
        {
            double digitsTemp = Math.Floor(Math.Log10(number));
            IDictionary<double, string> prefixes = new Dictionary<double, string>()
            {
                {3,"K"},
                {6,"M"},
                {9,"B"},
                {12,"T"},
                {15,"Qa"},
                {18,"Qi"},
                {21,"Se"},
                {24,"Sep"}
            };
            double digitsEvery3 = 3 * Math.Floor(digitsTemp / 3);
            if (number >= 1000) return (number / Math.Pow(10, digitsEvery3)).ToString(digits) + prefixes[digitsEvery3];
            return number.ToString(digits);
        }
    }
}
