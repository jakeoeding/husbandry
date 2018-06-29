using NUnit.Framework;
using Husbandry.Objects;

namespace Husbandry.Tests
{
    [TestFixture]
    public class CornTests
    {
        Corn corn;

        [SetUp]
        public void TestSetup()
        {
            corn = new Corn(0);
        }

        [TearDown]
        public void TestTeardown()
        {
            corn = null;
        }

        [Test]
        public void TryHarvest_AgeRequiredForHarvestMet_HarvestAmountGreaterThanZero()
        {
            int harvestAmount = corn.TryHarvest(corn.AgeRequiredForHarvest);

            Assert.That(harvestAmount, Is.GreaterThan(0));
        }

        [Test]
        public void TryHarvest_AgeRequiredForHarvestUnmet_HarvestAmountEqualsZero()
        {
            int harvestAmount = corn.TryHarvest(corn.AgeRequiredForHarvest - 1);

            Assert.That(harvestAmount, Is.EqualTo(0));
        }
    }
}
