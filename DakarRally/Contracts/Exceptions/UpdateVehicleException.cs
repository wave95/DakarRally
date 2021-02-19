using DakarRally.Contracts.Exceptions;
using System;

namespace DakarRally.Contracts.Exceptions
{
    public class UpdateVehicleException : Exception
    {
        public UpdateVehicleException() : base(Domain.Constants.Vehicles.CantAddVehicle)
        {
        }

        public UpdateVehicleException(Exception inner) : base(Domain.Constants.Vehicles.CantAddVehicle, inner)
        {
        }
    }
}
