//Author: William Örnquist
using System.Numerics;

public class Program
{
    private List<string> wordList = new();

    private static void Main(string[] args)
    {
        Console.WriteLine("Anagram Calculator 1.0 (by William Ornquist)");
        Console.WriteLine("Add words to calculate the number of anagrams for each one (upper- and lower-cases are distinct).");
        Console.WriteLine("Input one word at a time, then input an empty string to start calculation.");
        Console.WriteLine();

    RestartPoint:
        Program program = new();
        program.Start();

        Console.WriteLine("Restart? (Y/N)");
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.KeyChar.Equals('y') || key.KeyChar.Equals('Y'))
            {
                goto RestartPoint;
            }
            else if (key.KeyChar.Equals('n') || key.KeyChar.Equals('N'))
            {
                Environment.Exit(0);
            }
        }
    }

    /// <summary>
    /// Starts the process by taking reading inputs.
    /// </summary>
    private void Start()
    {
        string input;
        int inputNr = 1;
        Console.Write("Input 1:");
        while ((input = Console.ReadLine()) != null && input != "")
        {
            wordList.Add(input);
            inputNr++;
            Console.Write("Input " + inputNr + ":");
        }

        Console.WriteLine("-- Results --");
        foreach (string word in wordList)
        {
            Console.Write(word + ": ");
            Console.Write(GetAnagramCount(word));
            Console.WriteLine(" Anagrams");
        }
    }

    /// <summary>
    /// Returns the number of unique anagrams of a word.
    /// </summary>
    private static BigInteger GetAnagramCount(string word)
    {
        //! 'BigInteger' struct is used as moderately long words can cause the factorial result to be larger than what a 'ulong' type can hold.
        BigInteger lengthFac = GetFactorial(word.Length);
        var chDict = GetDistinctChars(word);

        // Divides the factorial of the word's length with the factorial of
        // the number of times each distinct character is present in the word.
        foreach (int cVal in chDict.Values)
        {
            lengthFac /= GetFactorial(cVal);
        }

        return lengthFac;
    }

    /// <summary>
    /// Returns the factorial of a given number, which is for this program, the length of a word.
    /// </summary>
    /// <param name="wordLength">The length of a word</param>
    private static BigInteger GetFactorial(int wordLength)
    {
        // Follows the factorial formula (n! = n × (n - 1))
        BigInteger factorial = 1;
        for (BigInteger i = 2; i <= (BigInteger)wordLength; i++)
        {
            factorial *= i;
        }

        return factorial;
    }

    /// <summary>
    /// Returns a dictionary that adds the number of times each distinct character has been encountered.
    /// </summary>
    private static Dictionary<char, int> GetDistinctChars(string word)
    {
        var dict = new Dictionary<char, int>();

        foreach (char c in word)
        {
            // Adds char to dictionary with the value 0 if it did not exist before.
            if (!dict.ContainsKey(c))
            {
                dict[c] = 0;
            }

            // Increments the value of 'c' in the dictionary for every duplicate found.
            dict[c]++;
        }

        return dict;
    }
}
