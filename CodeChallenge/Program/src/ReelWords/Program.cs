using System;

namespace ReelWords
{
    static class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;

            while (playing)
            {
                string input = Console.ReadLine();

                if (input == "quit") playing = false;
                // Run game logic here using the user input string

                // Create simple unit tests to test your code in the ReelWordsTests project,
                // don't worry about creating tests for everything, just important functions as
                // seen for the Trie tests
            }
        }
    }
}