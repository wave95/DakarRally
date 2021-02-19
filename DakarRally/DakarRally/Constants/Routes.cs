namespace API.Constants
{
    public class Routes
    {
        /// <summary>
        /// Contains the races controller routes.
        /// </summary>
        internal static class Races
        {
            /// <summary>
            /// Create race.
            /// </summary>
            internal const string CreateRace = "races/create";

            /// <summary>
            /// Start race.
            /// </summary>
            internal const string StartRace = "races/{raceId:int}/start";

            /// <summary>
            /// Get race by id.
            /// </summary>
            internal const string GetRaceById = "races/{raceId:int}";

            /// <summary>
            /// Get race status.
            /// </summary>
            internal const string GetRaceStatus = "races/{raceId:int}/status";

            /// <summary>
            /// Get race leaderboard.
            /// </summary>
            internal const string GetLeaderboard = "races/{raceId:int}/leaderboard";

            /// <summary>
            /// Race leaderboard for vehicle type.
            /// </summary>
            internal const string GetLeaderboardForType = "races/leaderboard-type";
        }

        /// <summary>
        /// Contains the vehicles controller routes.
        /// </summary>
        internal static class Vehicles
        {
            /// <summary>
            /// Create vehicle.
            /// </summary>
            internal const string AddVehicle = "vehicles/add";

            /// <summary>
            /// Update vehicle.
            /// </summary>
            internal const string UpdateVehicle = "vehicles/update";

            /// <summary>
            /// Remove vehicle.
            /// </summary>
            internal const string RemoveVehicle = "vehicles/remove/{vehicleId:int}";

            /// <summary>
            /// Get vehicle by id.
            /// </summary>
            internal const string GetVehicleById = "vehicles/{vehicleId:int}";

            /// <summary>
            /// Get vehicle statistics.
            /// </summary>
            internal const string GetVehicleStatistics = "vehicles/{vehicleId:int}/statistics";

            /// <summary>
            /// Get vehicles.
            /// </summary>
            internal const string GetVehicles = "vehicles";

            /// <summary>
            /// Get vehicle types.
            /// </summary>
            internal const string GetVehicleTypes = "vehicles/types";

            /// <summary>
            /// Get vehicle subtypes.
            /// </summary>
            internal const string GetVehicleSubtypes = "vehicles/subtypes";

            /// <summary>
            /// Get vehicle statuses.
            /// </summary>
            internal const string GetVehicleStatuses = "vehicles/statuses";
        }
    }
}
