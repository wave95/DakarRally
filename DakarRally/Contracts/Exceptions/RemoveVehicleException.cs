using System;

namespace Contracts.Exceptions
{
    public class RemoveVehicleException : Exception
    {
        public RemoveVehicleException() : base(DakarRally.Domain.Constants.Vehicles.CantRemoveVehicle)
        {
        }

        public RemoveVehicleException(Exception inner) : base(DakarRally.Domain.Constants.Vehicles.CantRemoveVehicle, inner)
        {
        }
    }
}
