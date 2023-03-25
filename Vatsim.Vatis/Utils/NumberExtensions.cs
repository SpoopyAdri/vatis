using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vatsim.Vatis.Utils;

public static class NumberExtensions
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
            return "minus " + Math.Abs(number).NumbersToWordsGroup();

        string words = "";

        if (number / 1000000 > 0)
        {
            words += (number / 1000000).NumbersToWordsGroup() + " million ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += (number / 1000).NumbersToWordsGroup() + " thousand ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            words += (number / 100).NumbersToWordsGroup() + " hundred ";
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
                if (number % 10 > 0)
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
            return "minus " + Math.Abs(number).NumbersToWords();

        string words = "";

        if (number / 1000000 > 0)
        {
            words += (number / 1000000).NumbersToWords() + " million ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += (number / 1000).NumbersToWords() + " thousand ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            words += (number / 100).NumbersToWords() + " hundred ";
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
                if (number % 10 >= 0)
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

            return string.Join(useDecimalTerminology ? " decimal " : " point ", group);
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

    public static double ApplyMagVar(this int degrees, int? magvar = null)
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