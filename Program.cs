using System.Data;
using System.Linq.Expressions;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new List<SweEngGloss>();
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
            public void WordPrint()
            {
                Console.WriteLine($"{word_swe,-10} - {word_eng,-10}");
            }
        }
        static void Main(string[] args)
        {
            string[] argument;
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            PrintHelp();
            do
            {
                argument = Input("> ").Split();
                string command = argument[0];
                if (command.ToLower() == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command.ToLower() == "load")
                {
                    FileLoader(defaultFile, argument);
                }
                else if (command.ToLower() == "list")
                {
                    PrintList();
                }
                else if (command.ToLower() == "new")
                {
                    AddNewItem(argument);
                }
                else if (command.ToLower() == "delete")
                {
                    DeleteItem(argument);
                }
                else if (command.ToLower() == "translate")
                {
                    WordTranslation(argument);
                }
                else if (command.ToLower() == "help")
                {
                    PrintHelp();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (argument[0].ToLower() != "quit");
        }
        static string Input(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }//Metod för att skriva ut en prompt och returnera en Console.ReadLine.

        private static void PrintList()
        {
            int printCounter = 0;
            foreach (SweEngGloss gloss in dictionary)
            {
                printCounter++;
                if (gloss != null)
                    gloss.WordPrint();
            }
            if (printCounter == 0)
            {
                Console.WriteLine("The dictionary is empty, load a file before trying to print.");
            }
        }//Metod för skriva ut listan.

        private static void AddNewItem(string[] argument)
        {
            if (argument.Length == 3)
            {
                dictionary.Add(new SweEngGloss(argument[1], argument[2]));
            }
            else if (argument.Length == 1)
            {
                string swedishWord = Input("Write word in Swedish: ");
                string englishWord = Input("Write word in English: ");
                dictionary.Add(new SweEngGloss(swedishWord, englishWord));
            }
        }//Metod för att lägga till ett nytt ord i listan.

        private static void FileLoader(string defaultFile, string[] argument)
        {
            try
            {
                if (argument.Length == 2)
                {
                    using (StreamReader sr = new StreamReader(argument[1]))
                    {
                        dictionary = new List<SweEngGloss>(); // Empty it!
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            dictionary.Add(gloss);
                            line = sr.ReadLine();
                        }
                    }
                }
                else if (argument.Length == 1)
                {
                    using (StreamReader sr = new StreamReader(defaultFile))
                    {
                        dictionary = new List<SweEngGloss>(); // Empty it!
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            dictionary.Add(gloss);
                            line = sr.ReadLine();
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException exc)
            {
                Console.WriteLine($"Could not find file {argument[1]}");
            }
        }//Metod för att läsa in fil, antingen default eller en egen fil.

        private static void DeleteItem(string[] argument)
        {
            if (argument.Length == 3)
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                {
                    dictionary.RemoveAt(index);
                    Console.WriteLine("The entry has been removed!");

                }
                else
                {
                    Console.WriteLine("Could not find a matching entry!");
                }
            }
            else if (argument.Length == 1)
            {
                if (dictionary.Count > 0)
                {
                    string swedishWord = Input("Write word in Swedish: ");
                    string englishWord = Input("Write word in English: ");
                    int index = -1;
                    for (int i = 0; i < dictionary.Count; i++)
                    {
                        SweEngGloss gloss = dictionary[i];
                        if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index != -1)
                    {
                        dictionary.RemoveAt(index);
                        Console.WriteLine("The entry has been removed!");
                    }
                    else
                    {
                        Console.WriteLine("Couldn't find your entry in the dictionary.");
                    }
                }
                else
                {
                    Console.WriteLine("The dictionary is already empty.");
                }
            }
            else
            {
                Console.WriteLine("Input is invalid. Use Delete Swedish word English word");
            }
        }//Metod för att radera en sak ifrån listan.
        private static void PrintHelp()
        {
            Console.WriteLine("You can choose between these commands:");
            Console.WriteLine(" Load                        # Loads a database into the program.");
            Console.WriteLine(" List                        # Prints a list of the Dictionary.");
            Console.WriteLine(" New                         # Add a new item to the Dictionary with 2 steps.");
            Console.WriteLine(" New Swedish / English       # Adds a new item to the Dictionary instantly.");
            Console.WriteLine(" Translate                   # Prints a translation for a word of your liking.");
            Console.WriteLine(" Delete                      # Deletes an item from the Dictionary with 2 steps");
            Console.WriteLine(" Delete Swedish / English    # Deletes the item from the Dictionary instantly.");
            Console.WriteLine(" Quit                        # Quits the program.");
        }//Metod för skriv ut hjälp kommandot.

        private static void WordTranslation(string[] argument)
        {
            if (argument.Length == 2)
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    if (gloss.word_swe == argument[1])
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == argument[1])
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
            else if (argument.Length == 1)
            {
                string userTranslate = Input("Write the word to be translated: ");
                foreach (SweEngGloss gloss in dictionary)
                {
                    if (gloss.word_swe == userTranslate)
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == userTranslate)
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
        }//Metod för Translate kommandot.
    }
}