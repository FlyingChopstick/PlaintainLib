using PlaintainLib;
using System;
using Xunit;

namespace Tests
{
    public static class PlaintainTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(2324)]
        [InlineData(int.MaxValue)]
        public static void PlaintainShouldBeCreatedWithBalance(int startingBalance)
        {
            Plaintain plt = new(startingBalance);

            Assert.Equal(startingBalance, plt.Balance);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-23)]
        [InlineData(-3457)]
        public static void PlaintainShouldThrowOnBadBalance(int startingBalance)
        {
            Plaintain plt;
            Assert.Throws<ArgumentException>(() => plt = new(startingBalance));
        }


        [Theory]
        [InlineData(100, Transport.Ground)]
        [InlineData(2354, Transport.Subway)]
        [InlineData(int.MaxValue, Transport.Commercial)]
        public static void PlaintainShouldBeChargedCorrectly(int startingBalance, Transport transport)
        {
            Plaintain plt = new(startingBalance);

            plt.AddRide(transport);

            int plrCharge = startingBalance - plt.Balance;
            int ridePrice = Plaintain.GetRidePrice(1, transport);

            Assert.Equal(ridePrice, plrCharge);
        }


        [Theory]
        [InlineData(300)]
        [InlineData(21)]
        [InlineData(int.MaxValue)]
        public static void CommersialPriceShouldBeChangeable(int changeTo)
        {
            Plaintain.CommercialPrice = changeTo;
            Assert.Equal(changeTo, Plaintain.CommercialPrice);
        }

        [Theory]
        [InlineData(-23)]
        [InlineData(-2354)]
        [InlineData(int.MinValue)]
        public static void CommercialPriceShouldNotAcceptIncorrectValue(int changeTo)
        {
            Assert.Throws<ArgumentException>(() => Plaintain.CommercialPrice = changeTo);
        }
    }
}
