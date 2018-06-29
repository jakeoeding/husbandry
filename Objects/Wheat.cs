using System;
using System.Collections.Generic;

namespace Husbandry.Objects
{
    public class Wheat : Food
    {
        public Wheat(int currentDate, int ageRequiredForHarvest = 1, int probabilityOfSuccess = 55, int harvestAmount = 8, string description = "wheat")
        {
            PlantDate = currentDate;
            AgeRequiredForHarvest = ageRequiredForHarvest;
            ProbabilityOfSuccess = probabilityOfSuccess;
            HarvestAmount = harvestAmount;
            Description = description;
        }
    }
}
