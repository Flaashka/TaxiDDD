using System;
using System.Globalization;
using System.Linq;
// ReSharper disable IdentifierTypo

namespace Ddd.Taxi.Domain
{
    public class TaxiApi : ITaxiApi<TaxiOrder>
    {
        private readonly DriversRepository driversRepo;
        private readonly Func<DateTime> currentTime;
        private int idCounter;

        public TaxiApi(DriversRepository driversRepo, Func<DateTime> currentTime)
        {
            this.driversRepo = driversRepo;
            this.currentTime = currentTime;
        }

        public TaxiOrder CreateOrderWithoutDestination(string firstName, string lastName, string street, string building)
        {
            var taxiOrder = new TaxiOrder();
            taxiOrder.SetTaxiOrderId(idCounter++);
            taxiOrder.SetClient(firstName, lastName);
            taxiOrder.SetStartAddress(street, building);
            taxiOrder.SetCreationTime(currentTime());

            return taxiOrder;
        }

        public void UpdateDestination(TaxiOrder order, string street, string building)
        {
            order.UpdateDestination(street, building);
        }

        public void AssignDriver(TaxiOrder order, int driverId)
        {
            driversRepo.FillDriverToOrder(driverId, order, currentTime());
            //order.AssignDriver();
        }

        public void UnassignDriver(TaxiOrder order)
        {
            order.UnassignDriver();
        }

        public string GetDriverFullInfo(TaxiOrder order)
        {
            if (order.Status == TaxiOrderStatus.WaitingForDriver) return null;
            return string.Join(" ",
                "Id: " + order.Driver?.DriverId,
                "DriverName: " + FormatName(order.Driver?.DriverName?.FirstName, order.Driver?.DriverName?.LastName),
                "Color: " + order.Driver?.Car?.CarColor,
                "CarModel: " + order.Driver?.Car?.CarModel,
                "PlateNumber: " + order.Driver?.Car?.CarPlateNumber);
        }

        public string GetShortOrderInfo(TaxiOrder order)
        {
            return string.Join(" ",
                "OrderId: " + order.Id,
                "Status: " + order.Status,
                "Client: " + FormatName(order.Client.ClientName.FirstName, order.Client.ClientName.LastName),
                "Driver: " + FormatName(order.Driver?.DriverName?.FirstName, order.Driver?.DriverName?.LastName),
                "From: " + FormatAddress(order.StartAddress.Street, order.StartAddress.Building),
                "To: " + FormatAddress(order.DestinationAddress?.Street, order.DestinationAddress?.Building),
                "LastProgressTime: " + GetLastProgressTime(order).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
        }

        private DateTime GetLastProgressTime(TaxiOrder order)
        {
            if (order.Status == TaxiOrderStatus.WaitingForDriver) return order.CreationTime;
            if (order.Status == TaxiOrderStatus.WaitingCarArrival) return order.DriverAssignmentTime;
            if (order.Status == TaxiOrderStatus.InProgress) return order.StartRideTime;
            if (order.Status == TaxiOrderStatus.Finished) return order.FinishRideTime;
            if (order.Status == TaxiOrderStatus.Canceled) return order.CancelTime;
            throw new NotSupportedException(order.Status.ToString());
        }

        private string FormatName(string firstName, string lastName)
        {
            return string.Join(" ", new[] { firstName, lastName }.Where(n => n != null));
        }

        private string FormatAddress(string street, string building)
        {
            return string.Join(" ", new[] { street, building }.Where(n => n != null));
        }

        public void Cancel(TaxiOrder order)
        {
            order.Cancel(currentTime());
        }

        public void StartRide(TaxiOrder order)
        {
            order.StartRide(currentTime());
        }

        public void FinishRide(TaxiOrder order)
        {
            order.FinishRide(currentTime());
        }
    }
}