using System;

namespace DakarRally.Contracts.Exceptions
{
    public class RaceAlreadyStartedException : Exception
    {
        public RaceAlreadyStartedException() : base(Domain.Constants.Races.RaceAlreadyStarted)
        {
        }
    }
}
