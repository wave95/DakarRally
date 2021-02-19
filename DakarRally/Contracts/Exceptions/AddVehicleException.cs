using System;

namespace DakarRally.Contracts.Exceptions
{
    public class AddVehicleException : Exception
    {
        public AddVehicleException() : base(Domain.Constants.Vehicles.CantAddVehicle)
        {
        }

        public AddVehicleException(Exception inner) : base(Domain.Constants.Vehicles.CantAddVehicle, inner)
        {
        }
    }
}
