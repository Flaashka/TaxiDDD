using System;
using System.Collections.Generic;
// ReSharper disable IdentifierTypo

namespace Ddd.Taxi.Domain
{
	//типичная анемичная модель
	public class TaxiOrder
	{
        public int Id { get; private set; }
        public void SetTaxiOrderId(int taxiOrderId)
        {
            Id = taxiOrderId;
        }

        public Client Client { get; private set; }
		public void SetClient(string firstName, string lastName, int clientId = default(int))
        {
            Client = new Client(firstName, lastName, clientId);
        }

        public Address StartAddress { get; private set; }
        public void SetStartAddress(string startStreet, string startBuilding)
        {
            StartAddress = new Address(startStreet, startBuilding);
        }

		public Address DestinationAddress { get; private set; }
        private void SetDestinationAddress(string destinationStreet, string destinationBuilding)
        {
            DestinationAddress = new Address(destinationStreet, destinationBuilding);
        }

        public Driver Driver { get; private set; }
        private void SetDriver(string firstName, string lastName, string carColor, string carModel, string carPlateNumber, int driverId = default(int))
        {
            Driver = new Driver(firstName, lastName, driverId);
            Driver.SetCar(carColor, carModel, carPlateNumber);
        }


        public TaxiOrderStatus Status { get; private set; }
        private void SetTaxiOrderStatus(TaxiOrderStatus status)
        {
            Status = status;
        }


        public DateTime CreationTime { get; private set; }
        public void SetCreationTime(DateTime creationTime)
        {
            CreationTime = creationTime;
        }


        public DateTime DriverAssignmentTime { get; private set; }
        private void SetDriverAssignmentTime(DateTime driverAssignmentTime)
        {
            DriverAssignmentTime = driverAssignmentTime;
        }


        public DateTime CancelTime { get; private set; }
        public void SetCancelTime(DateTime cancelTime)
        {
            CancelTime = cancelTime;
        }


        public DateTime StartRideTime { get; private set; }
        public void SetStartRideTime(DateTime startRideTime)
        {
            StartRideTime = startRideTime;
        }


        public DateTime FinishRideTime { get; private set; }
        public void SetFinishRideTime(DateTime finishRideTime)
        {
            FinishRideTime = finishRideTime;
        }


        //public TaxiOrder CreateOrderWithoutDestination(string firstName, string lastName, string street,
        //    string building, DateTime creationTime, int taxiOrderId)
        //{
        //    var taxiOrder = new TaxiOrder();
        //    SetTaxiOrderId(taxiOrderId);
        //    SetClient(firstName, lastName);
        //    SetStartAddress(street, building);
        //    SetCreationTime(creationTime);

        //    return taxiOrder;
        //}


        public void AssignDriver(string firstName, string lastName, string carColor, string carModel, string carPlateNumber, DateTime driverAssignmentTime, int driverId = default(int))
        {
            SetDriver(firstName, lastName, carColor, carModel, carPlateNumber, driverId);
            SetDriverAssignmentTime(driverAssignmentTime);
            SetTaxiOrderStatus(TaxiOrderStatus.WaitingCarArrival);
        }

        public void UnassignDriver()
        {
            if (Driver == null)
                throw new InvalidOperationException("WaitingForDriver");

            SetDriver(null, null, null, null, null);
            SetTaxiOrderStatus(TaxiOrderStatus.WaitingForDriver);
        }

        public void Cancel(DateTime cancelTime)
        {
            SetTaxiOrderStatus(TaxiOrderStatus.Canceled);
            SetCancelTime(cancelTime);
        }

        public void UpdateDestination(string street, string building)
        {
            SetDestinationAddress(street, building);
        }

        public void StartRide(DateTime startRideTime)
        {
            SetTaxiOrderStatus(TaxiOrderStatus.InProgress);
            SetStartRideTime(startRideTime);
        }

        public void FinishRide(DateTime finishRideTime)
        {
            SetTaxiOrderStatus(TaxiOrderStatus.Finished);
            SetFinishRideTime(finishRideTime);
        }
    }
}