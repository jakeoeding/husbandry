using NUnit.Framework;
using System.Collections.Generic;
using Husbandry.Objects;
using Husbandry.Logic;

namespace Husbandry.Tests
{
    [TestFixture]
    public class GameplayTests
    {
        Gameplay gameplay;

        [SetUp]
        public void TestSetup()
        {
            gameplay = new Gameplay();
        }

        [TearDown]
        public void TestTeardown()
        {
            gameplay = null;
        }

        [Test]
        public void HarvestCrops_CropReadyForHarvest_FoodSuppliesIncrease()
        {
            int foodSuppliesBefore = gameplay.supplies["corn"] + gameplay.supplies["wheat"];
            int foodSuppliesAfter;

            gameplay.Day = 4;
            gameplay.crops.Add(new Corn(1));

            gameplay.HarvestCrops();

            foodSuppliesAfter = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            Assert.That(foodSuppliesBefore, Is.LessThan(foodSuppliesAfter));
        }

        [Test]
        public void HarvestCrops_NoCrops_FoodSuppliesRemainsSame()
        {
            int foodSuppliesBefore = gameplay.supplies["corn"] + gameplay.supplies["wheat"];
            int foodSuppliesAfter;

            gameplay.crops = new List<Food>();

            gameplay.HarvestCrops();

            foodSuppliesAfter = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            Assert.That(foodSuppliesBefore, Is.EqualTo(foodSuppliesAfter));
        }

        [Test]
        public void HarvestCrops_CropNotReadyForHarvest_FoodSuppliesRemainsSame()
        {
            int foodSuppliesBefore = gameplay.supplies["corn"] + gameplay.supplies["wheat"];
            int foodSuppliesAfter;

            gameplay.Day = 4;
            gameplay.crops.Add(new Corn(4));

            gameplay.HarvestCrops();

            foodSuppliesAfter = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            Assert.That(foodSuppliesBefore, Is.EqualTo(foodSuppliesAfter));
        }

        [Test]
        public void HarvestCrops_CropNotReadyForHarvest_CropCountRemainsSame()
        {
            int cropCountBefore;
            gameplay.Day = 4;
            gameplay.crops.Add(new Corn(4));
            cropCountBefore = gameplay.crops.Count;

            gameplay.HarvestCrops();

            Assert.That(cropCountBefore, Is.EqualTo(gameplay.crops.Count));
        }

        [Test] 
        public void HarvestCrops_CropReadyForHarvest_CropCountDecreases()
        {
            int cropCountBefore;
            gameplay.Day = 4;
            gameplay.crops.Add(new Corn(1));
            cropCountBefore = gameplay.crops.Count;

            gameplay.HarvestCrops();

            Assert.That(cropCountBefore, Is.EqualTo(gameplay.crops.Count + 1));
        }

        [Test]
        public void EatDinner_HaveNoChildren_FoodSuppliesShouldDecreaseByTwo()
        {
            int foodSuppliesBefore;
            int foodSuppliesAfter;

            foodSuppliesBefore = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            gameplay.FamilyMembersCount = 2;

            gameplay.EatDinner();

            foodSuppliesAfter = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            Assert.That(foodSuppliesBefore, Is.EqualTo(foodSuppliesAfter + gameplay.FamilyMembersCount));
        }

        [Test]
        public void EatDinner_HaveOneChild_FoodSuppliesDecreaseByThree()
        {
            int foodSuppliesBefore;
            int foodSuppliesAfter;

            foodSuppliesBefore = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            gameplay.FamilyMembersCount = 3;

            gameplay.EatDinner();

            foodSuppliesAfter = gameplay.supplies["corn"] + gameplay.supplies["wheat"];

            Assert.That(foodSuppliesBefore, Is.EqualTo(foodSuppliesAfter + gameplay.FamilyMembersCount));
        }

        [Test]
        public void EatDinner_HaveNoChildren_MilkSuppliesDecreaseByOne()
        {
            int milkSuppliesBefore = gameplay.supplies["milk"];
            gameplay.FamilyMembersCount = 2;

            gameplay.EatDinner();

            Assert.That(milkSuppliesBefore, Is.EqualTo(gameplay.supplies["milk"] + 1));
        }

        [Test]
        public void EatDinner_HaveOneChild_MilkSuppliesDecreaseByOne()
        {
            int milkSuppliesBefore = gameplay.supplies["milk"];
            gameplay.FamilyMembersCount = 3;

            gameplay.EatDinner();

            Assert.That(milkSuppliesBefore, Is.EqualTo(gameplay.supplies["milk"] + 1));
        }

        [Test]
        public void CheckSurvivalCriteria_FoodAndMilkSuppliesGreaterThanOrEqualToZero_GameOverIsFalse()
        {
            gameplay.supplies = new Dictionary<string, int>() 
            {
                {"corn", 10},
                {"wheat", 10},
                {"milk", 4}
            };

            bool gameover = gameplay.CheckSurvivalCriteria();

            Assert.That(gameover, Is.EqualTo(false));
        }

        [Test]
        public void CheckSurvivalCriteria_FoodAndMilkSuppliesLessThanZero_GameOverIsTrue()
        {
            gameplay.supplies = new Dictionary<string, int>()
            {
                {"corn", 0},
                {"wheat", -1},
                {"milk", -1}
            };

            bool gameover = gameplay.CheckSurvivalCriteria();

            Assert.That(gameover, Is.EqualTo(true));
        } 

        [Test]
        public void CheckSurvivalCriteria_FoodSuppliesLessThanZeroAndMilkSuppliesGreaterThanZero_GameOverIsTrue()
        {
            gameplay.supplies = new Dictionary<string, int>()
            {
                {"corn", 0},
                {"wheat", -1},
                {"milk", 4}
            };

            bool gameover = gameplay.CheckSurvivalCriteria();

            Assert.That(gameover, Is.EqualTo(true));
        }

        [Test]
        public void CheckSurvivalCriteria_FoodSuppliesGreaterThanZeroAndMilkSuppliesLessThanZero_GameOverIsTrue()
        {
            gameplay.supplies = new Dictionary<string, int>()
            {
                {"corn", 10},
                {"wheat", 10},
                {"milk", -1}
            };

            bool gameover = gameplay.CheckSurvivalCriteria();

            Assert.That(gameover, Is.EqualTo(true));
        }
    }
}

