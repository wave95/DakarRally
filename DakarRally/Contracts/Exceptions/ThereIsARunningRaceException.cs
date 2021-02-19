using System;

namespace DakarRally.Contracts.Exceptions
{
    public class ThereIsARunningRaceException : Exception
    {
        public ThereIsARunningRaceException() : base(Domain.Constants.Races.ThereIsARunninRace)
        { }
    }
}

