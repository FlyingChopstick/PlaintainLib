using System;

namespace PlaintainLib
{
    public class Plaintain
    {
        /// <summary>
        /// Create an empty Plaintain
        /// </summary>
        public Plaintain()
        {
            Balance = 0;
            GroundRides = 0;
            SubwayRides = 0;
        }
        /// <summary>
        /// Create a Plaintain with a predefined balance
        /// </summary>
        /// <param name="balance">Starting balance</param>
        public Plaintain(int balance)
        {
            Balance = balance;
            GroundRides = 0;
            SubwayRides = 0;
        }


        /// <summary>
        /// Card balance
        /// </summary>
        public int Balance { get; private set; }
        /// <summary>
        /// Rides since the start of the month
        /// </summary>
        public int GroundRides { get; private set; }
        /// <summary>
        /// Rides since the start of the month
        /// </summary>
        public int SubwayRides { get; private set; }


        /// <summary>
        /// Increase balance of the card
        /// </summary>
        /// <param name="balance"></param>
        public void TopUp(int balance)
        {
            if (balance < 0) balance = -balance;

            this.Balance += balance;
        }
        /// <summary>
        /// Reset the Ride Count
        /// </summary>
        public void ResetRides()
        {
            this.GroundRides = 0;
            this.SubwayRides = 0;
        }
        /// <summary>
        /// Add a ride
        /// </summary>
        /// <param name="transport">Transport type</param>
        public void AddRide(Transport transport)
        {
            switch (transport)
            {
                case Transport.Ground: GroundRides++; break;
                case Transport.Subway: SubwayRides++; break;
            }

            int rideCount = transport switch
            {
                Transport.Ground => GroundRides,
                Transport.Subway => SubwayRides,
                _ => 0
            };
            Balance -= GetRidePrice(rideCount, transport);
        }
        /// <summary>
        /// Display all ride counters
        /// </summary>
        public void DisplayCurrentRides()
        {
            Console.WriteLine($"Ground rides: {GroundRides}");
            Console.WriteLine($"Subway rides: {SubwayRides}");
        }
        /// <summary>
        /// Display the ride counter for the selected transport type
        /// </summary>
        /// <param name="transport">Transport type</param>
        public void DisplayCurrentRides(Transport transport)
        {
            switch (transport)
            {
                case Transport.Ground:
                    Console.WriteLine($"Ground rides: {GroundRides}"); break;
                case Transport.Subway:
                    Console.WriteLine($"Subway rides: {SubwayRides}"); break;
                default: break;
            }
        }

        /// <summary>
        /// Get the price of the ride
        /// </summary>
        /// <param name="rideCount"></param>
        /// <param name="transport"></param>
        /// <returns></returns>
        public static int GetRidePrice(int rideCount, Transport transport)
        {
            if (transport == Transport.Commercial) return 50;
            return rideCount switch
            {
                <= 10 => transport switch
                {
                    Transport.Ground => 33,
                    Transport.Subway => 38,
                    _ => throw new NotImplementedException()
                },

                > 10 and <= 20 => transport switch
                {
                    Transport.Ground => 32,
                    Transport.Subway => 37,
                    _ => throw new NotImplementedException()
                },

                > 20 and <= 30 => transport switch
                {
                    Transport.Ground => 31,
                    Transport.Subway => 36,
                    _ => throw new NotImplementedException()
                },

                > 30 and <= 40 => transport switch
                {
                    Transport.Ground => 30,
                    Transport.Subway => 35,
                    _ => throw new NotImplementedException()
                },

                > 40 => transport switch
                {
                    Transport.Ground => 29,
                    Transport.Subway => 34,
                    _ => throw new NotImplementedException()
                }
            };
        }
        /// <summary>
        /// Get monthly ride prices on the selected type
        /// </summary>
        /// <param name="transport">Transport type</param>
        public static void PrintMonthlyPrices(Transport transport)
        {
            Console.WriteLine($"<=10: {Plaintain.GetRidePrice(1, transport)}");
            Console.WriteLine($"11-20: {Plaintain.GetRidePrice(15, transport)}");
            Console.WriteLine($"21-30: {Plaintain.GetRidePrice(25, transport)}");
            Console.WriteLine($"31-40: {Plaintain.GetRidePrice(35, transport)}");
            Console.WriteLine($">= 41: {Plaintain.GetRidePrice(45, transport)}");
            Console.WriteLine();
        }
    }
}
