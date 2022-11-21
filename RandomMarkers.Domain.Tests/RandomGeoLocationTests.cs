namespace RandomMarkers.Domain.Tests
{
    public class RandomGeoLocationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldBe_RandomLocation_isInvalid()
        {
            var randomGeoLocation = RandomGeoLocation.Create("40.744656-74.00596", "34.052234, -118.243685");

            Assert.IsTrue(randomGeoLocation.Failure);
        }
        [Test]
        public void ShouldBe_RandomLocation_isValid()
        {
            var randomGeoLocation = RandomGeoLocation.Create("40.744656, -74.00596", "34.052234, -118.243685");
            var value = randomGeoLocation.Value;
            Assert.IsTrue(randomGeoLocation.Success);
        }
    }
}