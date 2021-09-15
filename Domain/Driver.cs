using Ddd.Infrastructure;

namespace Ddd.Taxi.Domain
{
    public class Driver : Entity<int>
    {
        public Driver(string firstName, string lastName, int driverId = default(int)) : base(driverId)
        {
            DriverName = new PersonName(firstName, lastName);
            DriverId = driverId;
        }

        public Driver(string firstName, string lastName, string carColor, string carModel,
            string carPlateNumber, int driverId = default(int)) : this(firstName, lastName, driverId)
        {
            SetCar(carColor, carModel, carPlateNumber);
        }

        public int DriverId { get; }
        public PersonName DriverName { get; }
        public Car Car { get; private set; }

        public void SetCar(string carColor, string carModel, string carPlateNumber)
        {
            Car = new Car(carColor, carModel, carPlateNumber);
        }
    }
}