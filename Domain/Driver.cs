using Ddd.Infrastructure;

namespace Ddd.Taxi.Domain
{
    public class Driver : Entity<int>
    {
        public Driver(PersonName driverName, Car car, int driverId = default(int)) : base(driverId)
        {
            DriverName = driverName;
            SetCar(car);
            DriverId = driverId;
        }

        public int DriverId { get; }
        public PersonName DriverName { get; }
        public Car Car { get; private set; }

        public void SetCar(string carColor, string carModel, string carPlateNumber)
        {
            Car = new Car(carColor, carModel, carPlateNumber);
        }
        public void SetCar(Car car)
        {
            Car = car;
        }
    }
}