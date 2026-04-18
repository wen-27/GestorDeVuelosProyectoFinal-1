using System;
using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.StreetTypes.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.EmailDomains.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.Moduls.PeoplePhones.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.Moduls.Airlines.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.AirportAirline.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Infrastructure.Entities;

namespace GestorDeVuelosProyectoFinal.src.Shared.Context;

public class AppDbContext : DbContext
{
    public DbSet<ContinentEntity> Continents { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<RegionEntity> Regions { get; set; }
    public DbSet<CityEntity> Cities { get; set; } = null!;
    public DbSet<AircraftModelsEntity> AircraftModels { get; set; } 
    public DbSet<AircraftEntity> Aircrafts { get; set; } = null!;
    public DbSet<StreetTypeEntity> StreetTypes { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<CabinTypeEntity> CabinTypes { get; set; } = null!;
    public DbSet<CabinConfiurationEntity> CabinConfigurations { get; set; } = null!;
    public DbSet<AircraftModelsEntity> AircraftModel { get; set; } = null!;
    public DbSet<EmailDomainsEntity> EmailDomains { get; set; } = null!;
    public DbSet<PersonEmailEntity> PersonEmails { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<DocumentTypeEntity> DocumentTypes { get; set; } = null!;
    public DbSet<PersonEntity> Persons { get; set; } = null!;
    public DbSet<PeoplePhoneEntity> PeoplePhones { get; set; } = null!;
    public DbSet<PhoneCodeEntity> PhoneCodes { get; set; } = null!;
    public DbSet<AirlineEntity> Airlines { get; set; } = null!;

    public DbSet<AirportEntity> Airports { get; set; } = null!;
    public DbSet<AirportRouteReferenceEntity> AirportRouteReferences { get; set; } = null!;
    public DbSet<AirportAirlineEntity> AirportAirlines { get; set; } = null!;
    public DbSet<PersonalPositionEntity> PersonalPositions { get; set; } = null!;
    public DbSet<StaffPositionReferenceEntity> StaffPositionReferences { get; set; } = null!;
    public DbSet<StaffEntity> Staffs { get; set; } = null!;
    public DbSet<FlightAssignmentStaffReferenceEntity> FlightAssignmentStaffReferences { get; set; } = null!;
    public DbSet<FutureFlightReferenceEntity> FutureFlightReferences { get; set; } = null!;
    public DbSet<AvailabilityStateEntity> AvailabilityStates { get; set; } = null!;

    public DbSet<StaffAvailabilityEntity> StaffAvailabilities { get; set; } = null!;


    public DbSet<StaffAvailabilityStateReferenceEntity> StaffAvailabilityStateReferences { get; set; } = null!;
     public DbSet<StaffAvailabilityEntity> StaffAvailabilities { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplica todas las configuraciones de la carpeta infrastructure/Entity
        modelBuilder.ApplyConfiguration(new ContinentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RegionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CountryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AircraftModelsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AircraftEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StreetTypeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CabinTypeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CabinConfigurationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTypeEntityConfiguration());

        modelBuilder.ApplyConfiguration(new EmailDomainsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonEmailEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PeoplePhoneEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneCodeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AirlineEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AirportEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AirportRouteReferenceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AirportAirlineEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalPositionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StaffPositionReferenceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StaffEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FlightAssignmentStaffReferenceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FutureFlightReferenceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AvailabilityStateEntityConfiguration());

        modelBuilder.ApplyConfiguration(new StaffAvailabilityStateReferenceEntityConfiguration());

        modelBuilder.ApplyConfiguration(new StaffAvailabilityEntityConfiguration());
    }

}
