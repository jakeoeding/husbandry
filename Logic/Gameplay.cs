using System;
using System.Collections.Generic;
using Husbandry.Objects;

namespace Husbandry.Logic
{
    public class Gameplay
    {
        public int Day { get; set; }
        public int FamilyMembersCount { get; set; }
        public bool DaySpoiled { get; set; }
        public bool GameOver { get; set; }

        public Dictionary<string, int> supplies;
        public List<Food> crops = new List<Food>();
        public RandomEvent eventGenerator = new RandomEvent();

        public Gameplay()
        {
            Day = 0;
            FamilyMembersCount = 4;
            supplies = new Dictionary<string, int>(3)
            {
                {"corn", 10},
                {"wheat", 10},
                {"milk", 4}
            };
            DaySpoiled = false;
            GameOver = false;
        }

        /// <summary>
        /// Loop continusously through days until user's supplies run out and the game ends
        /// </summary>
        public void Play()
        {
            string userInput;

            while (!GameOver)
            {
                // Update each day
                userInput = string.Empty;
                DaySpoiled = false;
                Day++;

                Console.WriteLine($"\nDay {Day}");

                // Generate a random event and account for effects
                eventGenerator.GenerateEvent(this);

                if (!GameOver)
                    Console.WriteLine($"\nYou currently have: {supplies["corn"]} corn, {supplies["wheat"]} wheat, and {supplies["milk"]} milk");

                if (!DaySpoiled)
                {
                    Console.WriteLine("\nWhat would you like to do?");

                    Console.WriteLine("Press \"1\" to plant corn, \"2\" to plant wheat, or \"3\" to milk the cow.");

                    // Take action once user enters valid input
                    while ((Array.IndexOf(new string[] { "1", "2", "3" }, userInput)) < 0)
                    {
                        userInput = Console.ReadLine();
                    }

                    if (userInput == "1")
                    {
                        crops.Add(new Corn(Day));
                        Console.WriteLine("\nYou plant some corn.");
                    }
                    else if (userInput == "2")
                    {
                        crops.Add(new Wheat(Day));
                        Console.WriteLine("\nYou plant some wheat.");
                    }
                    else
                    {
                        supplies["milk"] += 2;
                        Console.WriteLine("\nYou milk your cow.");
                    }
                }

                // Increase supplies if crops have matured
                HarvestCrops();

                // Consume daily food and milk requirements
                EatDinner();

                // Check to see if user ran out of supplies and the game has ended
                GameOver = CheckSurvivalCriteria();

                Console.WriteLine("\nPress \"c\" to continue to the next day.");

                while (userInput != "c")
                {
                    userInput = Console.ReadLine();
                }

                Console.WriteLine("-----------------------------------------------");
            }

            Console.WriteLine($"Game Over! You ran out of goods. You made it to day {Day}!");
        }

        /// <summary>
        /// Harvests all crops in crop list if they are appropriate age
        /// </summary>
        public void HarvestCrops()
        {
            foreach (var crop in crops)
            {
                supplies[crop.Description] += crop.TryHarvest(Day);
            }

            // Remove all crops once harvest age has been reached (regardless of success)
            crops.RemoveAll(crop => crop.CurrentAge == crop.AgeRequiredForHarvest);
        }

        /// <summary>
        /// Consumes 1 piece of food for each family member (starting with corn)
        /// and 1 milk for the entire family
        /// </summary>
        public void EatDinner()
        {
            Console.WriteLine("You sit down to eat dinner with your family.");

            for (int i = 0; i < FamilyMembersCount; i++)
            {
                if (supplies["corn"] > 0)
                    supplies["corn"]--;
                else
                {
                    supplies["wheat"]--;
                }
            }

            supplies["milk"]--;
        }

        public bool CheckSurvivalCriteria()
        {
            // If you ran out of food or milk, the game ends
            return (supplies["corn"] + supplies["wheat"] < 0 || supplies["milk"] < 0);
        }
    }
}
