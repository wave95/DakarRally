using System;

namespace DakarRally.Contracts.Exceptions
{
    public class RaceNotFoundException : Exception
    {
        public RaceNotFoundException() : base(Domain.Constants.Races.RaceNotFound)
        {
        }
    }
}
