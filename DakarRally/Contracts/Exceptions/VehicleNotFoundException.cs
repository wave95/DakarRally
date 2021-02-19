using System;

namespace DakarRally.Contracts.Exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() : base(Domain.Constants.Vehicles.VehicleNotFound)
        {
        }
    }
}
