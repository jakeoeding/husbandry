using System;
using System.Collections.Generic;

namespace Husbandry.Objects
{
    public class Food
    {
        public int PlantDate { get; set; }
        public int AgeRequiredForHarvest { get; set; }
        public int ProbabilityOfSuccess { get; set; }
        public int HarvestAmount { get; set; }
        public string Description { get; set; }
        public int CurrentAge { get; set; }

        /// <summary>
        /// Harvests crop if it is the appropriate age.
        /// Amount harvested depends on probability of success.
        /// </summary>
        public int TryHarvest(int currentDate)
        {
            CurrentAge = currentDate - PlantDate;

            if (CurrentAge < AgeRequiredForHarvest)
            {
                Console.WriteLine($"Your {Description} is not ready for harvest.");
                return 0;
            }
            else
            {
                var harvestProbabilityGenerator = new Random();

                // If the random number falls in our success range, return full harvest amount
                if (harvestProbabilityGenerator.Next(101) < ProbabilityOfSuccess)
                {
                    Console.WriteLine($"You harvested {HarvestAmount} {Description}.");
                    return HarvestAmount;
                }
                // Otherwise, return half of harvest amount;
                else
                    Console.WriteLine($"You clumsily harvested {HarvestAmount / 2} {Description}, ruining the other half.");
                return HarvestAmount / 2;
            }
        }
    }
}