namespace DakarRally.Domain.Constants
{
    public static class Vehicles
    {
        public const int ModelNameMaximumLength = 50;
        public const int TeamNameMaximumLength = 50;


        #region Validation Error Messages
        public const string TeamNameRequired = "Vehicle team name is required";
        public const string TeamNameTooLong = "Vehicle team name is too long.";
        public const string ModelNameRequired = "Vehicle model name is required.";
        public const string ModelNameTooLong = "Vehicle team name is too long.";
        public const string VehicleIdMustBePositive = "Vehicle identifier must be a positive integer.";
        public const string InvalidSortingCriteria = "Invalid sorting criteria.";
        public const string AtLeastOneFilter = "Enter at least one filtering criteria.";

        #endregion

        #region Exceptions Error Messages
        public const string VehicleNotFound = "Vehicle with specified identifier was not found.";
        public const string CantAddVehicle = "Can not add vehicle.";
        public const string CantUpdateVehicle = "Can not update vehicle.";
        public const string CantRemoveVehicle = "Can not remove vehicle.";
        #endregion
    }
}
