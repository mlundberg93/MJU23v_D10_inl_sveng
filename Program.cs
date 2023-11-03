namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
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
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            PrintHelp();
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command.ToLower() == "quit") //NYI: Programmet avslutas inte när man kör kommando Quit.
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command.ToLower() == "load")
                {
                    FileLoader(defaultFile, argument);
                }
                else if (command.ToLower() == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)  //FIXME: Unhandled exception. System.NullReferenceException, när jag kör list före load i console.
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
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
            while (true);
        }

        private static void AddNewItem(string[] argument)
        {
            if (argument.Length == 3)
            {
                dictionary.Add(new SweEngGloss(argument[1], argument[2]));
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string swedishWord = Console.ReadLine();
                Console.Write("Write word in English: ");
                string englishWord = Console.ReadLine();
                dictionary.Add(new SweEngGloss(swedishWord, englishWord)); //FIXME: Unhandled exception. System.NullReferenceException: Object reference not set to an instance of an object.
            }
        }

        private static void FileLoader(string defaultFile, string[] argument)
        {
            if (argument.Length == 2)    //Unhandled exception. System.IO.FileNotFoundException: Could not find file, felhantera fel filläsning.
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
                        index = i;
                }
                dictionary.RemoveAt(index); //FIXME: Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range.
                                            //Ifall man inte stavar ordet rätt vid delete.
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string swedishWord = Console.ReadLine();
                Console.Write("Write word in English: ");
                string englishWord = Console.ReadLine();
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++) //FIXME: Unhandled exception. System.NullReferenceException: Object reference not set to an instance of an object.
                                                           //Om man kör Delete utan ladda listan.
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord)
                        index = i;
                }
                dictionary.RemoveAt(index);
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
                Console.WriteLine("Write word to be translated: ");
                string userTranslate = Console.ReadLine();
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