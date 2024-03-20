using System.Diagnostics;




internal class Task
{
    public static string Rational (int numerator, int denominator)
    {
        Debug.Assert(denominator != 0 && numerator < denominator && numerator >= 0);

        if (numerator == 0)
        {
            return "0";
        }

        string result = "0.";
        int remainder = numerator;
        int position = 0;
        Dictionary<int, int> seenRemainders = new Dictionary<int, int>();

        while (true)
        {
            remainder *= 10;
            if (remainder == 0)
            {
                break;
            }
            else if (seenRemainders.ContainsKey(remainder))
            {
                int repeatingPosition = seenRemainders[remainder];

                return result.Substring(0, repeatingPosition + 2) + "(" + result.Substring(repeatingPosition + 2) + ")";
            }

            seenRemainders.Add(remainder, position);
            result += (remainder / denominator).ToString();
            remainder %= denominator;
            position++;
        }
        return result;
    }

public static void Main()
        {
            Console.WriteLine(Rational(2, 5));
            Console.WriteLine(Rational(1, 6));
            Console.WriteLine(Rational(1, 3));
            Console.WriteLine(Rational(1, 7));
            Console.WriteLine(Rational(1, 77));
        }
}

    

