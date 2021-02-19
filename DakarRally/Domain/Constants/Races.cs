namespace DakarRally.Domain.Constants
{
    public static class Races
    {
        #region Default Values
        public const decimal DefaultRaceLenght = 10000m;
        #endregion

        #region Validation Error Messages
        public const string YearInThePast = "Race years can not be in the past.";
        public const string LengthLessThanZero = "Race length must have a positive value.";
        public const string IdRequired = "Race identifier is required.";
        public const string InvalidRaceId = "Invalid race identifier.";
        public const string InvalidVehicleType = "Invalid vehicle type";
        public const string InvalidVelicleSubtype = "Invalid vehicle subtype.";
        public const string RaceIdMustBePositive = "Race identifier must be a positive integer.";
        public const string RaceMustHaveAtLeastTwoVehicles = "Race needs to have at least two vehicles to start.";
        public const string ThereIsARunninRace = "There is already a running race";
        #endregion

        #region Exceptions Error Messages
        public const string RaceNotFound = "The race with specified identifier was not found.";       
        public const string RaceAlreadyStarted = "The race has already started.";
        #endregion
    }
}
