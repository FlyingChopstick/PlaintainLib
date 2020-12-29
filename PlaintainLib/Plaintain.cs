using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaintainLib
{
    /// <summary>
    /// Represents the Plaintain transport payment card
    /// </summary>
    public class Plaintain
    {
        /// <summary>
        /// Create an empty Plaintain
        /// </summary>
        public Plaintain()
        {
            Balance = 0;
            ResetRides();
        }
        /// <summary>
        /// Create a Plaintain with a predefined balance
        /// </summary>
        /// <param name="balance">Starting balance</param>
        public Plaintain(int balance)
        {
            if (balance < 0) throw new ArgumentException("Balance should be >=0.");

            Balance = balance;
            ResetRides();
        }
        /// <summary>
        /// Create a Plaintain with predefined ride count
        /// </summary>
        /// <param name="balance">Starting balance</param>
        /// <param name="groundRides">Ground rides count</param>
        /// <param name="subwayRides">Subway rides count</param>
        public Plaintain(int balance, int groundRides, int subwayRides)
        {
            if (balance < 0) throw new ArgumentException("Balance should be >=0.");
            Balance = balance;

            ResetRides();
            GroundRides = groundRides;
            SubwayRides = subwayRides;
        }


        private readonly Dictionary<Transport, int> _rideCounters = new();
        private static int _commercialPrice = 50;
        private List<Transport> _transportTypes;

        /// <summary>
        /// Gets or sets the price of the Commercial ride
        /// </summary>
        public static int CommercialPrice
        {
            get => _commercialPrice;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Commercial price cannot be <0.");
                else
                    _commercialPrice = value;
            }
        }

        /// <summary>
        /// Card balance
        /// </summary>
        public int Balance { get; private set; }
        /// <summary>
        /// Rides since the start of the month
        /// </summary>
        public int GroundRides
        {
            get => _rideCounters[Transport.Ground];
            private set => _rideCounters[Transport.Ground]--;
        }
        /// <summary>
        /// Rides since the start of the month
        /// </summary>
        public int SubwayRides
        {
            get => _rideCounters[Transport.Subway];
            private set => _rideCounters[Transport.Subway]--;
        }


        /// <summary>
        /// Increase balance of the card
        /// </summary>
        /// <param name="balance"><see langword="true"/> if the value is >= 0, <see langword="false"/> otherwise</param>
        public bool TopUp(int balance)
        {
            if (balance < 0)
            {
                Balance += balance;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Reset the Ride Count for each item in Transport enum to 0
        /// </summary>
        public void ResetRides()
        {
            _transportTypes = Enum.GetValues(typeof(Transport)).Cast<Transport>().ToList();
            foreach (var transport in _transportTypes)
            {
                _rideCounters[transport] = 0;
            }
        }
        /// <summary>
        /// Charges the card if the balance is sufficient
        /// </summary>
        /// <param name="transport">Transport type</param>
        /// <returns><see langword="true"/> if the operation is successful, <see langword="false"/> otherwise</returns>
        public bool AddRide(Transport transport)
        {
            int rideCount = _rideCounters[transport] + 1;

            int ridePrice = GetRidePrice(rideCount, transport);

            if (Balance >= ridePrice)
            {
                Balance -= ridePrice;
                _rideCounters[transport]++;
                return true; //successful operation
            }
            else
            {
                return false; //not enough money
            }
        }
        /// <summary>
        /// Adds several rides with the same transport type
        /// </summary>
        /// <param name="rideCount">How many rides to add</param>
        /// <param name="transport">What type of ride to add</param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> otherwise</returns>
        public bool AddSeveral(int rideCount, Transport transport)
        {
            bool working = rideCount != 0;

            int count = 0;
            int cost = 0;
            while (working && rideCount > 0)
            {
                working = AddRide(transport);
                rideCount--;
                count++;
                cost += GetRidePrice(count, transport);
            }

            Console.WriteLine($"Added {count} {transport} rides for a total of {cost} rub.");
            return working;
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
            if (transport == Transport.Commercial) return _commercialPrice;

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
