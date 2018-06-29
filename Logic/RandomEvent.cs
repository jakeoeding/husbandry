using System;
using System.Collections.Generic;

namespace Husbandry.Logic
{
    public class RandomEvent
    {
        public delegate void RandomEventFunction(Gameplay gameplay);
        Random rand = new Random();
        public List<RandomEventFunction> potentialRandomEvents = new List<RandomEventFunction>();

        public RandomEvent()
        {
            // In the beginning, only good/neutral events can occur
            potentialRandomEvents.Add(GoodWeather);
            potentialRandomEvents.Add(ExtraMilk);
            potentialRandomEvents.Add(ExtraFood);
        }

        /// <summary>
        /// Adds more bad potential random event possiblities as user progresses.
        /// </summary>
        public void UpdatePossibilities(int day)
        {
            switch (day)
            {
                case 4:
                    potentialRandomEvents.Add(Sick);
                    break;
                case 5:
                    potentialRandomEvents.Add(BadWeather);
                    break;
                case 6:
                    potentialRandomEvents.Add(ToolBroke);
                    break;
                case 7:
                    potentialRandomEvents.Add(MilkSpoiled);
                    break;
                case 9:
                    potentialRandomEvents.Add(AnotherChild);
                    break;
                case 11:
                    potentialRandomEvents.Add(LoseChild);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Generates a random event and calls the corresponding function.
        /// </summary>
        public void GenerateEvent(Gameplay gameplay)
        {
            // Add new possible events if appropriate
            UpdatePossibilities(gameplay.Day);

            // Generate a random number to index the function list
            int randomNumber = rand.Next(0, potentialRandomEvents.Count);

            // Call the chosen function
            potentialRandomEvents[randomNumber].Invoke(gameplay);
        }

        public void AnotherChild(Gameplay gameplay)
        {
            Console.WriteLine("Your wife has another child. You now have another mouth to feed.");
            gameplay.FamilyMembersCount++;
        }

        public void MilkSpoiled(Gameplay gameplay)
        {
            // If you have milk, decrement your milk count.
            if (gameplay.supplies["milk"] > 0)
            {
                Console.WriteLine("You left one bottle of milk out overnight and it spoiled.");
                gameplay.supplies["milk"]--;
            }
            // If you don't have milk, your tool broke
            else
            {
                ToolBroke(gameplay);
            }
        }

        public void ToolBroke(Gameplay gameplay)
        {
            Console.WriteLine("Your hoe breaks and you spend the rest of the day fixing it.");
            gameplay.DaySpoiled = true;
        }

        public void BadWeather(Gameplay gameplay)
        {
            Console.WriteLine("It is storming, making it unsafe to go outside. You must wait until tomorrow to get back to work.");
            gameplay.DaySpoiled = true;
        }

        public void Sick(Gameplay gameplay)
        {
            Console.WriteLine("You feel ill and are unable to work today.");
            gameplay.DaySpoiled = true;
        }

        public void LoseChild(Gameplay gameplay)
        {
            // If you have a kid, your day is ruined and your family count drops by 1
            if (gameplay.FamilyMembersCount > 2)
            {
                Console.WriteLine("One of your children passed away. You have one less mouth to feed. You are unable to work, and spend the day mourning.");
                gameplay.DaySpoiled = true;
                gameplay.FamilyMembersCount--;
            }
            // Otherwise, you get a pass with a neutral day
            else
            {
                GoodWeather(gameplay);
            }
        }

        public void GoodWeather(Gameplay gameplay)
        {
            Console.WriteLine("It is a beautiful day. You get right to work.");
        }

        public void ExtraMilk(Gameplay gameplay)
        {
            Console.WriteLine("Your cow had some extra milk this morning.");
            gameplay.supplies["milk"]++;
        }

        public void ExtraFood(Gameplay gameplay)
        {
            Console.WriteLine("Your neighbor had extra supplies and brought you some wheat.");
            gameplay.supplies["wheat"] += 2;
        }
    }
}
