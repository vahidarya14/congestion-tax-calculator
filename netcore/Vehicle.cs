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
}