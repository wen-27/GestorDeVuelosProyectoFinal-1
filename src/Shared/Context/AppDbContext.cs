using System;
using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Infrastructure.Persistence.Entities;
/*using GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Persistence.Entities;

using GestorDeVuelosProyectoFinal.src.Moduls.Countries.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Regions.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.DocumentTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Cities.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.ViaTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Addresses.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.People.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.EmailDomains.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PhoneCodes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PeoplePhones.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.AirPorts.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PersonalPositions.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Personal.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.AvailabilityStates.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.StaffAvailability.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Routes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Seasons.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Rates.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatus.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightRoles.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightAssignments.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.SeatLocationTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightSeats.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.ReservationStateTransitions.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CheckinStates.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CheckIns.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.InvoiceItemTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.InvoiceItems.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.CardTypes.Infrastructure.Entity;
using  GestorDeVuelosProyectoFinal.src.Moduls.CardIssuers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Payments.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.SystemRoles.Infrastructure.Entity;*/


namespace GestorDeVuelosProyectoFinal.src.Shared.Context;

public class AppDbContext : DbContext
{
    public DbSet<ContinentEntity> Continents { get; set; }
     /*public Dbset<AddressEntity> Addresses { get; set; }
    public DbSet<CountriesEntity> Countries { get; set; }
    public DbSet<RegionsEntity> Regions { get; set; }
    public DbSet<DocumentTypesEntity> DocumentTypes { get; set; }
    public DbSet<CitiesEntity> Cities { get; set; }
    public DbSet<ViaTypesEntity> ViaTypes { get; set; }
    public DbSet<AddressesEntity> Addresses { get; set; }
    public DbSet<PeopleEntity> People { get; set; }
    public DbSet<EmailDomainsEntity> EmailDomains { get; set; }
    public DbSet<PhoneCodesEntity> PhoneCodes { get; set; }
    public DbSet<PeopleEmailsEntity> PeopleEmails { get; set; }
    public DbSet<PeoplePhonesEntity> PeoplePhones { get; set; }
    public DbSet<CustomersEntity> Customers {get; set;}
    public DbSet<AirlinesEntity> Airlines {get; set;}
    public DbSet<AirPortsEntity> AirPorts {get; set;}
    public DbSet<AirportAirlineEntity> AirportAirline {get; set;}
    public DbSet<PersonalPositionsEntity> PersonalPositions {get; set;}
    public DbSet<PersonalEntity> Personal {get; set;}
    public DbSet<AvailabilityStatesEntity> AvailabilityStates {get; set;}
    public DbSet<StaffAvailabilityEntity> StaffAvailability {get; set;}
    public DbSet<AircraftManufacturersEntity> AircraftManufacturers {get; set;}
    public DbSet<AircraftModelsEntity> AircraftModels {get; set;}
    public DbSet<AircraftEntity> Aircraft {get; set;}
    public DbSet<CabinTypesEntity> CabinTypes {get; set;}
    public DbSet<CabinConfigurationEntity> CabinConfiguration {get; set;}
    public DbSet<RoutesEntity> Routes {get; set;}
    public DbSet<RouteStopoversEntity> RouteStopovers {get; set;}
    public DbSet<SeasonsEntity> Seasons {get; set;}
    public DbSet<PassengerTypesEntity> PassengerTypes {get; set;}
    public DbSet<RatesEntity> Rates {get; set;}
    public DbSet<FlightStatusEntity> FlightStatus {get; set;}
    public DbSet<FlightStatusTransitionsEntity> FlightStatusTransitions {get; set;}
    public DbSet<FlightsEntity> Flights {get; set;}
    public DbSet<FlightRolesEntity> FlightRoles {get; set;}
    public DbSet <FlightAssignmentsEntity> FlightAssignments {get; set;}
    public DbSet<SeatLocationTypesEntity> SeatLocationTypes {get; set;}
    public DbSet<FlightSeatsEntity> FlightSeats {get; set;}
    public DbSet<PassengersEntity> Passengers {get; set;}
    public DbSet<ReserveStatesEntity> ReserveStates {get; set;}
    public DbSet<ReservationStateTransitionsEntity> ReservationStateTransitions {get; set;}
    public DbSet<ReservationsEntity> Reservations {get; set;}
    public DbSet<FlightReservationsEntity> FlightReservations {get; set;}
    public DbSet<PassengerReservationsEntity> PassengerReservations {get; set;}
    public DbSet<TicketStatesEntity> TicketStates {get; set;}
    public DbSet<TicketEntity> Ticket {get; set;}
    public DbSet<CheckinStatesEntity> CheckinStates {get; set;}
    public DbSet<CheckinEntity> Checkin {get; set;}
    public DbSet<InvoiceItemTypesEntity> InvoiceItemTypes {get; set;}
    public DbSet<InvoicesEntity> Invoices {get; set;}
    public DbSet<InvoiceItemsEntity> InvoiceItems {get; set;}
    public DbSet<PaymentStatusesEntity> PaymentStatuses {get; set;}
    public DbSet <PaymentMediumTypesEntity> PaymentMediumTypes {get; set;}
    public DbSet<CardTypesEntity> CardTypes {get; set;}
    public DbSet<CardIssuersEntity> CardIssuers {get; set;}
    public DbSet<PaymentMethodsEntity> PaymentMethods {get; set;}
    public DbSet<PaymentsEntity> Payments {get; set;}
    public DbSet<SystemRolesEntity> SystemRoles {get; set;}*/
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplica todas las configuraciones de la carpeta infrastructure/Entity
        modelBuilder.ApplyConfiguration(new ContinentEntityConfiguration());
    }

}

