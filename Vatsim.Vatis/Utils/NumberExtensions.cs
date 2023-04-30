using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vatsim.Vatis.Utils;

public static class NumberExtensions
{
    /// <summary>
    /// Converts the number to group form for added clarity
    /// Example: 10,000...Ten thousand, 11,500...Eleven thousand five hundred
    /// </summary>
    /// <param name="number">The number to convert</param>
    /// <returns>Returns the number in group word form</returns>
    public static string ToGroupForm(this int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + Math.Abs(number).ToGroupForm();

        string words = "";

        if (number / 1000000 > 0)
        {
            words += (number / 1000000).ToGroupForm() + " million ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += (number / 1000).ToGroupForm() + " thousand ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            words += (number / 100).ToGroupForm() + " hundred ";
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
    /// Converts number into word string. Pronounce each digit in the number of hundreds 
    /// or thousands followed by the word "hundred" or "thousand", as appropriate.
    /// For example, 5,000...Five thousand, 11,500...One one thousand five hundred
    /// </summary>
    /// <param name="number">The number to be converted</param>
    /// <returns>Returns the number in group word form</returns>
    public static string ToWordString(this int number)
    {
        bool isNegative = number < 0;

        number = Math.Abs(number);

        if (number == 0)
            return "zero";

        if (isNegative)
            return "minus " + Math.Abs(number).ToWordString();

        string words = "";

        if (number / 1000000 > 0)
        {
            words += (number / 1000000).ToWordString() + " million ";
            number %= 1000000;
        }

        if (number / 1000 > 0)
        {
            words += (number / 1000).ToWordString() + " thousand ";
            number %= 1000;
        }

        if (number / 100 > 0)
        {
            words += (number / 100).ToWordString() + " hundred ";
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
    /// Converts number to serial numbers (separate digits)
    /// For example, 1500...One Five Zero Zero
    /// </summary>
    /// <param name="input">Number to be converted</param>
    /// <param name="useDecimalTerminology">Whether or not to use "decimal" terminology for deciaml number</param>
    /// <returns>Returns the serial formatted word string</returns>
    public static string ToSerialForm(this string input, bool useDecimalTerminology = false)
    {
        var group = new List<string>();
        var numberMatch = Regex.Match(input, @"[0-9]\d*(\.\d+)?");

        if (numberMatch.Success)
        {
            foreach (var number in numberMatch.Value.Split('.'))
            {
                var temp = new List<string>();
                foreach (var digit in number.ToString().Select(q => new string(q, 1)).ToArray())
                {
                    temp.Add(int.Parse(digit).ToWordString());
                }
                group.Add(string.Join(" ", temp).Trim(' '));
            }

            return string.Join(useDecimalTerminology ? " decimal " : " point ", group);
        }

        return input;
    }

    /// <summary>
    /// Converts number to serial numbers (separate digits)
    /// For example, 1500...One Five Zero Zero
    /// </summary>
    /// <param name="input">Number to be converted</param>
    /// <returns>Returns the serial formatted word string</returns>
    public static string ToSerialForm(this int number)
    {
        bool isNegative = number < 0;
        List<string> temp = new();
        foreach (var x in Math.Abs(number).ToString().Select(q => new string(q, 1)).ToArray())
        {
            temp.Add(int.Parse(x).ToWordString());
        }
        return $"{(isNegative ? "minus " : "")}{string.Join(" ", temp)}";
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

    public static int ApplyMagVar(this int degrees, int? magvar = null)
    {
        if (magvar == null)
            return degrees;

        if (degrees == 0)
            return degrees;

        if (magvar > 0)
        {
            degrees += magvar.Value;
        }
        else
        {
            degrees -= Math.Abs(magvar.Value);
        }

        return degrees.NormalizeHeading();
    }

    public static int ToFsdFrequencyFormat(this decimal value)
    {
        return (int)((value - 100m) * 1000m);
    }
}