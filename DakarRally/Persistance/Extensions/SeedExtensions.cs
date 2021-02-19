using DakarRally.Domain.Entities;
using DakarRally.Domain.Enums;
using System;
using System.Collections.Generic;

namespace DakarRally.Persistence.Extensions
{
    /// <summary>
    /// Contains the extension method for seeding the database with initial data.
    /// </summary>
    public static class SeedExtensions
    {
        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public static void SeedDatabase(this DakarRallyDbContext dbContext)
        {
            dbContext.Set<VehicleTypeRepairmentLength>().AddRange(new List<VehicleTypeRepairmentLength>
            {
                new VehicleTypeRepairmentLength()
                {
                    Id = (int)VehicleType.Truck, 
                    RepairmentLengthInHours = 7,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new VehicleTypeRepairmentLength()
                {
                    Id = (int)VehicleType.Car,
                    RepairmentLengthInHours = 5,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new VehicleTypeRepairmentLength()
                {
                    Id = (int)VehicleType.Motorcycle,
                    RepairmentLengthInHours = 3,
                    CreatedOnUtc = DateTime.UtcNow
                },
            });

            dbContext.Set<VehicleSubtypeSpeed>().AddRange(new List<VehicleSubtypeSpeed>
            {
                new VehicleSubtypeSpeed()
                {
                    Id = (int)VehicleSubtype.Truck,
                    SpeedInKilometersPerHour = 80,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new VehicleSubtypeSpeed()
                {
                    Id = (int)VehicleSubtype.TerrainCar,
                    SpeedInKilometersPerHour = 100,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new VehicleSubtypeSpeed()
                {
                    Id = (int)VehicleSubtype.SportsCar,
                    SpeedInKilometersPerHour = 140,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new VehicleSubtypeSpeed()
                {
                    Id = (int)VehicleSubtype.CrossMotorcycle,
                    SpeedInKilometersPerHour = 85,
                    CreatedOnUtc = DateTime.UtcNow
                },
                new VehicleSubtypeSpeed()
                {
                    Id = (int)VehicleSubtype.SportMotorcycle,
                    SpeedInKilometersPerHour = 130,
                    CreatedOnUtc = DateTime.UtcNow
                },
            });

            dbContext.Set<VehicleSubtypeMalfunctionProbability>().AddRange(new List<VehicleSubtypeMalfunctionProbability>
            {
                new VehicleSubtypeMalfunctionProbability()
                {
                    Id = (int)VehicleSubtype.Truck,
                    LightMalfunctionProbability = 0.06m,
                    HeavyMalfunctionProbability = 0.04m
                },
                new VehicleSubtypeMalfunctionProbability()
                {
                    Id = (int)VehicleSubtype.TerrainCar,
                    LightMalfunctionProbability = 0.03m,
                    HeavyMalfunctionProbability = 0.01m,
                },
                new VehicleSubtypeMalfunctionProbability()
                {
                    Id = (int)VehicleSubtype.SportsCar,
                    LightMalfunctionProbability = 0.12m,
                    HeavyMalfunctionProbability = 0.02m,
                },
                new VehicleSubtypeMalfunctionProbability()
                {
                    Id = (int)VehicleSubtype.CrossMotorcycle,
                    LightMalfunctionProbability = 0.03m,
                    HeavyMalfunctionProbability = 0.02m,
                },
                new VehicleSubtypeMalfunctionProbability()
                {
                    Id = (int)VehicleSubtype.SportMotorcycle,
                    LightMalfunctionProbability = 0.06m,
                    HeavyMalfunctionProbability = 0.04m,
                },
            });

            dbContext.SaveChanges();
        }
    }
}
