using System;
// ReSharper disable StringLiteralTypo

namespace Ddd.Taxi.Domain
{
    // In real aplication it whould be the place where database is used to find driver by its Id.
    // But in this exercise it is just a mock to simulate database
    public class DriversRepository
    {
        public void FillDriverToOrder(int driverId, TaxiOrder order, DateTime currentTime)
        {
            if (driverId == 15)
            {
                var driverFirstName = "Drive";
                var driverLastName = "Driverson";
                var carModel = "Lada sedan";
                var carColor = "Baklazhan";
                var carPlateNumber = "A123BT 66";
                order.AssignDriver(driverFirstName, driverLastName, carColor, carModel, carPlateNumber, currentTime, driverId);
            }
            else
                throw new Exception("Unknown driver id " + driverId);
        }
    }
}