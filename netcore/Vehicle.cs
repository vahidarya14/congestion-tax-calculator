namespace congestion.calculator
{
    public class Vehicle
    {
        public VehicleTypes VehicleType { get; private set; }
        public Vehicle(VehicleTypes vehicleType)
        {
            VehicleType = vehicleType;
        }

        public string GetVehicleType() => VehicleType.ToString();
    }
}