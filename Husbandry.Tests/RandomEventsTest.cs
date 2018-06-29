using NUnit.Framework;
using Husbandry.Logic;

namespace Husbandry.Tests
{
    [TestFixture]
    public class RandomEventTest
    {
        Gameplay gameplay;
        RandomEvent random;

        [SetUp]
        public void TestSetup()
        {
            gameplay = new Gameplay();
            random = new RandomEvent();
        }

        [TearDown]
        public void TestTeardown()
        {
            gameplay = null;
            random = null;
        }

        [Test]
        public void AnotherChild_General_FamilyMembersCountIncreasesByOne()
        {
            int familyMemberCountBefore = gameplay.FamilyMembersCount;

            random.AnotherChild(gameplay);

            Assert.That(familyMemberCountBefore, Is.EqualTo(gameplay.FamilyMembersCount - 1));
        }

        [Test]
        public void MilkSpoiled_MilkGreaterThanZero_MilkSuppliesDecreasesByOne()
        {
            int milkCountBefore = gameplay.supplies["milk"];

            random.MilkSpoiled(gameplay);

            Assert.That(milkCountBefore, Is.EqualTo(gameplay.supplies["milk"] + 1));
        }

        [Test]
        public void MilkSpoiled_MilkEqualToZero_MilkSuppliesRemainsSameAndDaySpoiled()
        {
            gameplay.supplies["milk"] = 0;

            random.MilkSpoiled(gameplay);

            Assert.That(0, Is.EqualTo(gameplay.supplies["milk"]));
            Assert.That(true, Is.EqualTo(gameplay.DaySpoiled));
        }

        [Test]
        public void ToolBroke_General_DayBecomesSpoiled()
        {
            random.ToolBroke(gameplay);

            Assert.That(true, Is.EqualTo(gameplay.DaySpoiled));
        }

        [Test]
        public void BadWeather_General_DayBecomesSpoiled()
        {
            random.BadWeather(gameplay);

            Assert.That(true, Is.EqualTo(gameplay.DaySpoiled));
        }

        [Test]
        public void Sick_General_DayBecomesSpoiled()
        {
            random.Sick(gameplay);

            Assert.That(true, Is.EqualTo(gameplay.DaySpoiled));
        }

        [Test]
        public void LoseChild_UserHasChild_FamilyMembersCountDecreasesByOneAndDaySpoiled()
        {
            int familyMembersCountBefore = gameplay.FamilyMembersCount;

            random.LoseChild(gameplay);

            Assert.That(familyMembersCountBefore, Is.EqualTo(gameplay.FamilyMembersCount + 1));
            Assert.That(true, Is.EqualTo(gameplay.DaySpoiled));
        }

        [Test]
        public void LoseChild_UserHasNoChild_FamilyMembersCountRemainsSameAndDayNotSpoiled()
        {
            gameplay.FamilyMembersCount = 2;

            random.LoseChild(gameplay);

            Assert.That(2, Is.EqualTo(gameplay.FamilyMembersCount));
            Assert.That(false, Is.EqualTo(gameplay.DaySpoiled));
        }

        [Test]
        public void ExtraMilk_General_MilkSuppliesIncreasesByOne()
        {
            int milkCountBefore = gameplay.supplies["milk"];

            random.ExtraMilk(gameplay);

            Assert.That(milkCountBefore, Is.EqualTo(gameplay.supplies["milk"] - 1));
        }

        [Test]
        public void ExtraFood_General_WheatSuppliesIncreasesByTwo()
        {
            int wheatCountBefore = gameplay.supplies["wheat"];

            random.ExtraFood(gameplay);

            Assert.That(wheatCountBefore, Is.EqualTo(gameplay.supplies["wheat"] - 2));
        }

        [Test]
        public void UpdatePossibilities_Day1_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(1);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count));
        }

        [Test]
        public void UpdatePossibilities_Day4_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(4);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count - 1));
        }

        [Test]
        public void UpdatePossibilities_Day5_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(5);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count - 1));
        }

        [Test]
        public void UpdatePossibilities_Day6_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(6);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count - 1));
        }

        [Test]
        public void UpdatePossibilities_Day7_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(7);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count - 1));
        }

        [Test]
        public void UpdatePossibilities_Day9_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(9);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count - 1));
        }

        [Test]
        public void UpdatePossibilities_Day11_PotentialRandomEventsCountIncreasesByOne()
        {
            int potentialRandomEventsCountBefore = random.potentialRandomEvents.Count;

            random.UpdatePossibilities(11);

            Assert.That(potentialRandomEventsCountBefore, Is.EqualTo(random.potentialRandomEvents.Count - 1));
        }
    }
}