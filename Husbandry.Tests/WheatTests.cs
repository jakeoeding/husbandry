using NUnit.Framework;
using Husbandry.Objects;

namespace Husbandry.Tests
{
    [TestFixture]
    public class WheatTests
    {
        Wheat wheat;

        [SetUp]
        public void TestSetup()
        {
            wheat = new Wheat(0);
        }

        [TearDown]
        public void TestTeardown()
        {
            wheat = null;
        }

        [Test]
        public void TryHarvest_AgeRequiredForHarvestMet_HarvestAmountGreaterThanZero()
        {
            int harvestAmount = wheat.TryHarvest(wheat.AgeRequiredForHarvest);

            Assert.That(harvestAmount, Is.GreaterThan(0));
        }

        [Test]
        public void TryHarvest_AgeRequiredForHarvestUnmet_HarvestAmountEqualsZero()
        {
            int harvestAmount = wheat.TryHarvest(wheat.AgeRequiredForHarvest - 1);

            Assert.That(harvestAmount, Is.EqualTo(0));
        }
    }
}
