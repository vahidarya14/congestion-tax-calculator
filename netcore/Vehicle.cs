namespace congestion.calculator
{
    public class Vehicle
    {
        public VehicleTypes VehicleType { get;  set; }
        public Vehicle(VehicleTypes vehicleType)
        {
            VehicleType = vehicleType;
        }

        public Vehicle()
        {
            
        }

        public string GetVehicleType() => VehicleType.ToString();
    }

    //---second way--------------------Polymorphism In Web API
    public class Car: Vehicle
    {
        public Car():base(VehicleTypes.Car){}
    }

    public class Tractor : Vehicle
    {
        public Tractor() : base(VehicleTypes.Tractor) { }
    }
}