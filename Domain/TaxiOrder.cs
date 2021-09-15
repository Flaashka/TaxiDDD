using System;
using System.Collections.Generic;

namespace Ddd.Taxi.Domain
{
	//типичная анемичная модель
	public class TaxiOrder
	{
		public int Id;
		public string ClientFirstName;
		public string ClientLastName;
		public string StartStreet;
		public string StartBuilding;
		public string DestinationStreet;
		public string DestinationBuilding;
		public int DriverId;
		public string DriverFirstName;
		public string DriverLastName;
		public string CarColor;
		public string CarModel;
		public string CarPlateNumber;
		public TaxiOrderStatus Status;
		public DateTime CreationTime;
		public DateTime DriverAssignmentTime;
		public DateTime CancelTime;
		public DateTime StartRideTime;
		public DateTime FinishRideTime;
	}
}