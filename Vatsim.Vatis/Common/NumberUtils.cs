using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vatsim.Vatis.Common;

public static class NumberUtils
{
    /// <summary>
    /// Translates numeric digit into word string.
    /// 1,500 = one thousand five hundred.
    /// 15,000 = fifteen thousand
    /// </summary>
    /// <param name="number">Numeric digit</param>
    /// <returns></returns>
    public static string NumbersToWordsGroup(this int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumbersToWordsGroup(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumbersToWordsGroup(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumbersToWordsGroup(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumbersToWordsGroup(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    /// <summary>
    /// Translates numeric digit into word string.
    /// 10,000 = One zero thousand
    /// 500 = Five hundred
    /// 9,500 = Niner thousand five hundred
    /// </summary>
    /// <param name="number">Integer number</param>
    /// <returns></returns>
    public static string NumbersToWords(this int number)
    {
        bool isNegative = number < 0;

        number = Math.Abs(number);

        if (number == 0)
            return "zero";

        if (isNegative)
            return "minus " + NumbersToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumbersToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumbersToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumbersToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "niner", "one zero", "one one", "one two", "one three", "one four", "one five", "one six", "one seven", "one eight", "one niner" };
            var tensMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "niner" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) >= 0)
                    words += " " + unitsMap[number % 10];
            }
        }

        return words.TrimEnd(' ');
    }

    /// <summary>
    /// Translates whole number (from string) into a single word.
    /// 1500 = One Five Zero Zero
    /// 100 = One Zero Zero
    /// </summary>
    /// <param name="number">String number</param>
    /// <returns></returns>
    public static string NumberToSingular(this string input, bool useDecimalTerminology = false)
    {
        var group = new List<string>();

        var punctuationMatch = Regex.Match(input, @"[.!?\\-]");
        var numberMatch = Regex.Match(input, @"[0-9]\d*(\.\d+)?");

        if (numberMatch.Success)
        {
            foreach (var number in numberMatch.Value.Split('.'))
            {
                var temp = new List<string>();
                foreach (var digit in number.ToString().Select(q => new string(q, 1)).ToArray())
                {
                    temp.Add(int.Parse(digit).NumbersToWords());
                }
                group.Add(string.Join(" ", temp).Trim(' '));
            }

            return punctuationMatch.Success
                ? string.Join(useDecimalTerminology ? " decimal " : " point ", group) + punctuationMatch.Value
                : string.Join(useDecimalTerminology ? " decimal " : " point ", group);
        }

        return input;
    }

    /// <summary>
    /// Translates whole number (from integer) into a single word.
    /// 1500 = One Five Zero Zero
    /// 100 = One Zero Zero
    /// </summary>
    /// <param name="number">Integer number</param>
    /// <returns></returns>
    public static string NumberToSingular(this int number)
    {
        bool isNegative = number < 0;
        List<string> temp = new List<string>();
        foreach (var x in Math.Abs(number).ToString().Select(q => new string(q, 1)).ToArray())
        {
            temp.Add(int.Parse(x).NumbersToWords());
        }
        return $"{(isNegative ? "minus " : "")}{string.Join(" ", temp)}";
    }

    /// <summary>
    /// Translates whole number (from integer) into a single word.
    /// 1500 = One Five Zero Zero
    /// 100 = One Zero Zero
    /// </summary>
    /// <param name="number">Integer number</param>
    /// <returns></returns>
    public static string NumberToSingular(this double number)
    {
        List<string> temp = new List<string>();
        foreach (var x in number.ToString().Select(q => new string(q, 1)).ToArray())
        {
            if (int.TryParse(x, out int i))
            {
                temp.Add(i.NumbersToWords());
            }
        }
        return string.Join(" ", temp);
    }

    /// <summary>
    /// Convert number to proper meters or kilos conversion
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string MetricToString(this double number)
    {
        if (number > 5000)
        {
            return $"{Convert.ToInt32(Math.Round(number / 1000)).NumbersToWordsGroup()} kilometers";
        }
        return $"{Convert.ToInt32(number).NumbersToWordsGroup()} meters";
    }

    /// <summary>
    /// Translates a decimal into its human readable string variant for the speech synthesizer.
    /// For example, 121.75 would translate to "one two one point seven five"
    /// </summary>
    /// <param name="input">Decimal to translate</param>
    /// <param name="isIcao">If true, use "decimal". If false, use "point"</param>
    /// <returns></returns>
    public static string DecimalToWordString(this double input, bool isIcao = true)
    {
        try
        {
            string result = "";

            string s = input.ToString();
            string[] parts = s.Split('.');
            var values = input.ToString(System.Globalization.CultureInfo.InvariantCulture).Split('.');

            result = $"{values[0]} {(isIcao ? "decimal" : "point")} {values[1]}";

            result = result.Replace("0", "zero ");
            result = result.Replace("1", "one ");
            result = result.Replace("2", "two ");
            result = result.Replace("3", "three ");
            result = result.Replace("4", "four ");
            result = result.Replace("5", "five ");
            result = result.Replace("6", "six ");
            result = result.Replace("7", "seven ");
            result = result.Replace("8", "eight ");
            result = result.Replace("9", "niner ");
            result = result.Replace("  ", " ");

            return result;
        }
        catch
        {
            return input.ToString();
        }
    }

    /// <summary>
    /// Normalizes heading degrees
    /// </summary>
    /// <param name="heading"></param>
    /// <returns></returns>
    public static double NormalizeHeading(this double heading)
    {
        if (heading <= 0.0)
        {
            heading += 360.0;
        }
        else if (heading > 360.0)
        {
            heading -= 360.0;
        }

        return heading;
    }

    public static int NormalizeHeading(this int heading)
    {
        if (heading <= 0)
        {
            heading += 360;
        }
        else if (heading > 360)
        {
            heading -= 360;
        }
        return heading;
    }

    public static double ApplyMagVar(this double degrees, int? magvar = null)
    {
        if (magvar == null)
            return degrees;

        if (degrees == 0)
            return degrees;

        if (magvar > 0)
        {
            return (degrees += magvar.Value).NormalizeHeading();
        }
        else
        {
            return (degrees -= Math.Abs(magvar.Value)).NormalizeHeading();
        }
    }

    public static int ToVatsimFrequencyFormat(this decimal value)
    {
        return (int)((value - 100m) * 1000m);
    }
}