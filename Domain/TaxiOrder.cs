using Ddd.Infrastructure;
using System;

// ReSharper disable CheckNamespace
// ReSharper disable IdentifierTypo
namespace Ddd.Taxi.Domain
{
    public class TaxiOrder : Entity<int>
    {
        public TaxiOrder(int taxiOrderId) : base(taxiOrderId) { }

        public Client Client { get; private set; }
		private void SetClient(string firstName, string lastName, int clientId = default(int))
        {
            Client = new Client(firstName, lastName, clientId);
        }
        public PersonName ClientName => Client == null ? throw new InvalidOperationException() : Client.ClientName;

        public Address StartAddress { get; private set; }
        private void SetStartAddress(Address startAddress)
        {
            StartAddress = startAddress;
        }
        public Address Start => StartAddress;

        public Address DestinationAddress { get; private set; }
        private void SetDestinationAddress(Address destinationAddress)
        {
            DestinationAddress = destinationAddress;
        }
        public Address Destination => DestinationAddress;

        public Driver Driver { get; private set; }
        private void SetDriver(Driver driver)
        {
            Driver = driver;
        }

        public TaxiOrderStatus Status { get; private set; }
        private void SetTaxiOrderStatus(TaxiOrderStatus status)
        {
            Status = status;
        }

        public DateTime CreationTime { get; private set; }
        private void SetCreationTime(DateTime creationTime)
        {
            CreationTime = creationTime;
        }

        public DateTime DriverAssignmentTime { get; private set; }
        private void SetDriverAssignmentTime(DateTime driverAssignmentTime)
        {
            DriverAssignmentTime = driverAssignmentTime;
        }

        public DateTime CancelTime { get; private set; }
        private void SetCancelTime(DateTime cancelTime)
        {
            CancelTime = cancelTime;
        }

        public DateTime StartRideTime { get; private set; }
        private void SetStartRideTime(DateTime startRideTime)
        {
            StartRideTime = startRideTime;
        }

        public DateTime FinishRideTime { get; private set; }
        private void SetFinishRideTime(DateTime finishRideTime)
        {
            FinishRideTime = finishRideTime;
        }

        public void CreateOrderWithoutDestination(string firstName, string lastName, string street,
            string building, DateTime creationTime)
        {
            SetClient(firstName, lastName);
            SetStartAddress(new Address(street, building));
            SetCreationTime(creationTime);
        }

        #region Driver
        public void AssignDriver(Driver driver, DateTime driverAssignmentTime)
        {
            if (Driver != null)
                throw new InvalidOperationException();

            SetDriver(driver);
            SetDriverAssignmentTime(driverAssignmentTime);
            SetTaxiOrderStatus(TaxiOrderStatus.WaitingCarArrival);
        }

        public void UnassignDriver()
        {
            if (Driver == null)
                throw new InvalidOperationException("WaitingForDriver");
            if (Status != TaxiOrderStatus.WaitingCarArrival)
                throw new InvalidOperationException();

            SetDriver(null);
            SetTaxiOrderStatus(TaxiOrderStatus.WaitingForDriver);
        }

        #endregion


        #region Destination
        public void UpdateDestination(Address destinationAddress)
        {
            SetDestinationAddress(destinationAddress);
        }

        #endregion


        #region Ride

        public void StartRide(DateTime startRideTime)
        {
            if (Status != TaxiOrderStatus.WaitingCarArrival)
                throw new InvalidOperationException();

            SetTaxiOrderStatus(TaxiOrderStatus.InProgress);
            SetStartRideTime(startRideTime);
        }

        public void Cancel(DateTime cancelTime)
        {
            if (Status != TaxiOrderStatus.WaitingCarArrival && Status != TaxiOrderStatus.WaitingForDriver)
                throw new InvalidOperationException();

            SetTaxiOrderStatus(TaxiOrderStatus.Canceled);
            SetCancelTime(cancelTime);
        }

        public void FinishRide(DateTime finishRideTime)
        {
            if (Status != TaxiOrderStatus.InProgress)
                throw new InvalidOperationException();

            SetTaxiOrderStatus(TaxiOrderStatus.Finished);
            SetFinishRideTime(finishRideTime);
        }

        #endregion
    }
}