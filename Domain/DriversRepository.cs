using System;

namespace Ddd.Taxi.Domain
{
    // In real aplication it whould be the place where database is used to find driver by its Id.
    // But in this exercise it is just a mock to simulate database
    public class DriversRepository
    {
        public void FillDriverToOrder(int driverId, TaxiOrder order)
        {
            if (driverId == 15)
            {
                order.DriverId = driverId;
                order.DriverFirstName = "Drive";
                order.DriverLastName = "Driverson";
                order.CarModel = "Lada sedan";
                order.CarColor = "Baklazhan";
                order.CarPlateNumber = "A123BT 66";
            }
            else
                throw new Exception("Unknown driver id " + driverId);
        }
    }
}