using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Husbandry.Logic
{
    public class Game
    {
        public Dictionary<string, int> highScores;
        public Gameplay gameplay;

        public Game()
        {
            highScores = new Dictionary<string, int>(6);
            gameplay = new Gameplay();
        }

        public void LoadTitle(string titlePath = @"./Resources/Title.txt")
        {
            string title = string.Empty;

            // Display ASCII art title after loading from text file
            if (File.Exists(titlePath))
            {
                using (TextReader reader = File.OpenText(titlePath))
                {
                    title = reader.ReadToEnd();
                }

                Console.WriteLine(title);
            }
        }

        public void LoadHighScores(string highScorePath = @"./Resources/HusbandryHS.txt")
        {
            // Load the highscores from text file
            if (File.Exists(highScorePath))
            {
                using (TextReader reader = File.OpenText(highScorePath))
                {
                    string[] line;
                    for (int i = 0; i < 5; i++)
                    {
                        line = reader.ReadLine().Split(';');
                        highScores.Add(line[1], Convert.ToInt16(line[0]));
                    }
                }
            }
        }

        public void PromptUserToBegin()
        {
            string userInput = string.Empty;

            Console.WriteLine("\nWelcome to the game! Press \"p\" to play, or \"h\" to view highscores!");

            // Read input until user enters valid input
            while (userInput != "p" && userInput != "h")
            {
                userInput = Console.ReadLine();
            }

            // Display highscores if user directed
            if (userInput == "h")
            {
                if (highScores != null)
                {
                    foreach (var kvp in highScores)
                    {
                        Console.WriteLine($"\t{kvp.Value}\t{kvp.Key}");
                    }
                }
                else
                {
                    Console.WriteLine("Sorry! Couldn't load highscores at this time.");
                }

                Console.WriteLine("\nPress \"p\" to play");
                while (userInput != "p")
                {
                    userInput = Console.ReadLine();
                }
            }
        }

        public void LoadInstructions(string instructionPath = @"./Resources/Instructions.txt")
        {
            string instructions = string.Empty;

            if (File.Exists(instructionPath))
            {
                using (TextReader reader = File.OpenText(instructionPath))
                {
                    instructions = reader.ReadToEnd();
                }    
            }

            Console.WriteLine(instructions);
        }

        /// <summary>
        /// Prepares user for gameplay 
        /// </summary>
        public void Setup()
        {
            LoadTitle();
            LoadHighScores();
            PromptUserToBegin();
            LoadInstructions();
        }

        public void UpdateHighscores(string highScorePath = @"./Resources/HusbandryHS.txt")
        {
            // Store the prior lowest score in the highscores dictionary
            int minScore = highScores.Values.ToArray().Min();

            // Compare current user's score to prior lowest highscore
            if (gameplay.Day > minScore)
            {
                string userInput = string.Empty;
                DateTime timeOfEntry = DateTime.Now;
                string writeText = string.Empty;

                Console.WriteLine("\nPlease enter your initials (must be 2 characters).");

                while (userInput.Length != 2)
                {
                    userInput = Console.ReadLine();
                }

                // Add date to user's initials to ensure uniqueness
                userInput += "\t" + timeOfEntry.ToString();

                // Add highscore to highscores dictionary
                highScores.Add(userInput, gameplay.Day);

                // Select top 5 scores from highscores dictionary (bumping prior lowest out)
                var query = (from kvp in highScores
                             orderby kvp.Value descending
                             select kvp).Take(5);

                foreach (var kvp in query)
                {
                    writeText += $"{kvp.Value};{kvp.Key}\n";
                }

                File.WriteAllText(highScorePath, writeText);
            }
            Console.WriteLine("\nThanks for playing!");
        }

    }
}
