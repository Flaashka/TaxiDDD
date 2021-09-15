using System;

// ReSharper disable StringLiteralTypo
// ReSharper disable CheckNamespace
namespace Ddd.Taxi.Domain
{
    // In real aplication it whould be the place where database is used to find driver by its Id.
    // But in this exercise it is just a mock to simulate database
    public class DriversRepository
    {
        public Driver GetDriverById(int driverId)
        {
            if (driverId != 15) 
                throw new Exception("Unknown driver id " + driverId);

            var driverFirstName = "Drive";
            var driverLastName = "Driverson";
            var carModel = "Lada sedan";
            var carColor = "Baklazhan";
            var carPlateNumber = "A123BT 66";

            var driverName = new PersonName(driverFirstName, driverLastName);
            var car = new Car(carColor, carModel, carPlateNumber);

            return new Driver(driverName, car, driverId);
        }
    }
}