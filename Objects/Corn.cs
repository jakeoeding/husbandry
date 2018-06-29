using System;
using System.Collections.Generic;

namespace Husbandry.Objects
{
    public class Corn : Food
    {
        public Corn(int currentDate, int ageRequiredForHarvest = 3, int probabilityOfSuccess = 90, int harvestAmount = 4, string description = "corn")
        {
            PlantDate = currentDate;
            AgeRequiredForHarvest = ageRequiredForHarvest;
            ProbabilityOfSuccess = probabilityOfSuccess;
            HarvestAmount = harvestAmount;
            Description = description;
        }
    }
}