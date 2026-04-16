using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeVuelosProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class Continents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aircraft_manufacturers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraft_manufacturers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "availability_states",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_availability_states", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cabin_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabin_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "document_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "email_domains",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    domains = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_domains", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flight_status",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_status", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "passenger_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    min_age = table.Column<int>(type: "int", nullable: true),
                    max_age = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passenger_types", x => x.id);
                    table.CheckConstraint("CK_PassengerTypes_AgeRange", "max_age IS NULL OR min_age IS NULL OR max_age >= min_age");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "people_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    country_code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_name = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people_code", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "personalpositions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalpositions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price_factor = table.Column<decimal>(type: "decimal(5,4)", nullable: false, defaultValue: 1.0000m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.id);
                    table.CheckConstraint("CK_Seasons_PriceFactor", "price_factor > 0");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "viatypes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viatypes", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aircraft_models",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    manufacturer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    model_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    maximum_capacity = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    maximum_takeoff_weight_kg = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    fuel_consumption_kg_h = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    cruise_speed_kmh = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cruising_altitude_ft = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AircraftManufacturersEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraft_models", x => x.id);
                    table.ForeignKey(
                        name: "FK_aircraft_models_aircraft_manufacturers_AircraftManufacturers~",
                        column: x => x.AircraftManufacturersEntityId,
                        principalTable: "aircraft_manufacturers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_aircraft_models_aircraft_manufacturers_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "aircraft_manufacturers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iso_code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    continente_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                    table.ForeignKey(
                        name: "FK_countries_Continents_continente_id",
                        column: x => x.continente_id,
                        principalTable: "Continents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flight_status_transitions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    origin_state_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    destination_state_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FlightStatusEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_status_transitions", x => x.id);
                    table.CheckConstraint("CK_FlightStatusTransitions_NoSelf", "origin_state_id <> destination_state_id");
                    table.ForeignKey(
                        name: "FK_flight_status_transitions_flight_status_FlightStatusEntityId",
                        column: x => x.FlightStatusEntityId,
                        principalTable: "flight_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flight_status_transitions_flight_status_destination_state_id",
                        column: x => x.destination_state_id,
                        principalTable: "flight_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flight_status_transitions_flight_status_origin_state_id",
                        column: x => x.origin_state_id,
                        principalTable: "flight_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "airlines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iata_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_in = table.Column<DateTime>(type: "date", nullable: true),
                    updated_on = table.Column<DateTime>(type: "date", nullable: true),
                    CountriesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airlines", x => x.id);
                    table.ForeignKey(
                        name: "FK_airlines_countries_CountriesEntityId",
                        column: x => x.CountriesEntityId,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_airlines_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    countries_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.id);
                    table.ForeignKey(
                        name: "FK_regions_countries_countries_id",
                        column: x => x.countries_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    models_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airline_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tuition = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    manufacturing_date = table.Column<DateTime>(type: "date", nullable: true),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    AircraftModelsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AirlinesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.id);
                    table.ForeignKey(
                        name: "FK_Aircraft_aircraft_models_AircraftModelsEntityId",
                        column: x => x.AircraftModelsEntityId,
                        principalTable: "aircraft_models",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Aircraft_aircraft_models_models_Id",
                        column: x => x.models_Id,
                        principalTable: "aircraft_models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aircraft_airlines_AirlinesEntityId",
                        column: x => x.AirlinesEntityId,
                        principalTable: "airlines",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Aircraft_airlines_airline_Id",
                        column: x => x.airline_Id,
                        principalTable: "airlines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    regions_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                    table.ForeignKey(
                        name: "FK_cities_regions_regions_id",
                        column: x => x.regions_id,
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cabin_configuration",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    aircraft_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cabin_Type_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    start_row = table.Column<int>(type: "int", nullable: false),
                    end_row = table.Column<int>(type: "int", nullable: false),
                    seats_per_row = table.Column<int>(type: "int", nullable: false),
                    seat_letters = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AircraftEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabin_configuration", x => x.id);
                    table.ForeignKey(
                        name: "FK_cabin_configuration_Aircraft_AircraftEntityId",
                        column: x => x.AircraftEntityId,
                        principalTable: "Aircraft",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cabin_configuration_Aircraft_aircraft_id",
                        column: x => x.aircraft_id,
                        principalTable: "Aircraft",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cabin_configuration_cabin_types_cabin_Type_Id",
                        column: x => x.cabin_Type_Id,
                        principalTable: "cabin_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    viatype_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    path_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    complement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    postal_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CitiesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ViaTypesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_addresses_cities_CitiesEntityId",
                        column: x => x.CitiesEntityId,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_addresses_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_addresses_viatypes_ViaTypesEntityId",
                        column: x => x.ViaTypesEntityId,
                        principalTable: "viatypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_addresses_viatypes_viatype_id",
                        column: x => x.viatype_id,
                        principalTable: "viatypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "airports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iata_code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icao_code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cities_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CitiesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airports", x => x.id);
                    table.ForeignKey(
                        name: "FK_airports_cities_CitiesEntityId",
                        column: x => x.CitiesEntityId,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_airports_cities_cities_id",
                        column: x => x.cities_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    documenttype_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    document_number = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    surnames = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_birth = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_in = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_in = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.id);
                    table.ForeignKey(
                        name: "FK_people_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_people_document_types_documenttype_id",
                        column: x => x.documenttype_id,
                        principalTable: "document_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AirportAirline",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airport_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airline_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    terminal = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    AirPortsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AirlinesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportAirline", x => x.id);
                    table.ForeignKey(
                        name: "FK_AirportAirline_airlines_AirlinesEntityId",
                        column: x => x.AirlinesEntityId,
                        principalTable: "airlines",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AirportAirline_airlines_airline_Id",
                        column: x => x.airline_Id,
                        principalTable: "airlines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirportAirline_airports_AirPortsEntityId",
                        column: x => x.AirPortsEntityId,
                        principalTable: "airports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AirportAirline_airports_airport_Id",
                        column: x => x.airport_Id,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airport_origin_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airport_destination_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    distance_km = table.Column<int>(type: "int", nullable: true),
                    estimated_duration_Min = table.Column<int>(type: "int", nullable: true),
                    AirPortsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routes", x => x.id);
                    table.ForeignKey(
                        name: "FK_routes_airports_AirPortsEntityId",
                        column: x => x.AirPortsEntityId,
                        principalTable: "airports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_routes_airports_airport_destination_id",
                        column: x => x.airport_destination_id,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_routes_airports_airport_origin_id",
                        column: x => x.airport_origin_id,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    people_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_in = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                    table.ForeignKey(
                        name: "FK_customers_people_people_Id",
                        column: x => x.people_Id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "people_emails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    people_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    email_user = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    domain_email_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    es_principal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    EmailDomainsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PeopleEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people_emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_people_emails_email_domains_EmailDomainsEntityId",
                        column: x => x.EmailDomainsEntityId,
                        principalTable: "email_domains",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_people_emails_email_domains_domain_email_id",
                        column: x => x.domain_email_id,
                        principalTable: "email_domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_people_emails_people_PeopleEntityId",
                        column: x => x.PeopleEntityId,
                        principalTable: "people",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_people_emails_people_people_id",
                        column: x => x.people_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "people_phones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    people_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    phonecode_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    phone_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_main = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    PeopleEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people_phones", x => x.id);
                    table.ForeignKey(
                        name: "FK_people_phones_people_PeopleEntityId",
                        column: x => x.PeopleEntityId,
                        principalTable: "people",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_people_phones_people_code_phonecode_Id",
                        column: x => x.phonecode_Id,
                        principalTable: "people_code",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_people_phones_people_people_Id",
                        column: x => x.people_Id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    people_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    position_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airline_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airport_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    entry_date = table.Column<DateTime>(type: "date", nullable: true),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_in = table.Column<DateTime>(type: "date", nullable: true),
                    updated_in = table.Column<DateTime>(type: "date", nullable: true),
                    AirPortsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AirlinesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PeopleEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PersonalPositionsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.id);
                    table.ForeignKey(
                        name: "FK_Personal_airlines_AirlinesEntityId",
                        column: x => x.AirlinesEntityId,
                        principalTable: "airlines",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Personal_airlines_airline_Id",
                        column: x => x.airline_Id,
                        principalTable: "airlines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personal_airports_AirPortsEntityId",
                        column: x => x.AirPortsEntityId,
                        principalTable: "airports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Personal_airports_airport_Id",
                        column: x => x.airport_Id,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personal_people_PeopleEntityId",
                        column: x => x.PeopleEntityId,
                        principalTable: "people",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Personal_people_people_Id",
                        column: x => x.people_Id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personal_personalpositions_PersonalPositionsEntityId",
                        column: x => x.PersonalPositionsEntityId,
                        principalTable: "personalpositions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Personal_personalpositions_position_Id",
                        column: x => x.position_Id,
                        principalTable: "personalpositions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "staff_availability",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    peoble_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    availabilityState_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    observation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_availability", x => x.id);
                    table.CheckConstraint("CK_StaffAvailability_Fechas", "end_date IS NULL OR end_date > start_date");
                    table.ForeignKey(
                        name: "FK_staff_availability_availability_states_availabilityState_id",
                        column: x => x.availabilityState_id,
                        principalTable: "availability_states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_staff_availability_people_peoble_id",
                        column: x => x.peoble_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "fares",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    route_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    cabin_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    passenger_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    season_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    base_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valid_from = table.Column<DateTime>(type: "date", nullable: true),
                    valid_to = table.Column<DateTime>(type: "date", nullable: true),
                    CabinTypesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PassengerTypesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RoutesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SeasonsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fares", x => x.id);
                    table.CheckConstraint("CK_Fares_BasePrice", "base_price >= 0");
                    table.CheckConstraint("CK_Fares_Dates", "valid_to IS NULL OR valid_from IS NULL OR valid_to >= valid_from");
                    table.ForeignKey(
                        name: "FK_fares_cabin_types_CabinTypesEntityId",
                        column: x => x.CabinTypesEntityId,
                        principalTable: "cabin_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_fares_cabin_types_cabin_type_id",
                        column: x => x.cabin_type_id,
                        principalTable: "cabin_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fares_passenger_types_PassengerTypesEntityId",
                        column: x => x.PassengerTypesEntityId,
                        principalTable: "passenger_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_fares_passenger_types_passenger_type_id",
                        column: x => x.passenger_type_id,
                        principalTable: "passenger_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fares_routes_RoutesEntityId",
                        column: x => x.RoutesEntityId,
                        principalTable: "routes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_fares_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fares_seasons_SeasonsEntityId",
                        column: x => x.SeasonsEntityId,
                        principalTable: "seasons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_fares_seasons_season_id",
                        column: x => x.season_id,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    flight_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    airline_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    route_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    aircraft_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    flight_state_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    departure_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    estimated_arrival_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    total_capacity = table.Column<int>(type: "int", nullable: false),
                    available_seats = table.Column<int>(type: "int", nullable: false),
                    rescheduled_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    AircraftEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AirlinesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    FlightStatusEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.id);
                    table.CheckConstraint("CK_Flights_AvailableSeats", "available_seats >= 0");
                    table.CheckConstraint("CK_Flights_Capacity", "total_capacity > 0");
                    table.CheckConstraint("CK_Flights_Dates", "estimated_arrival_date > departure_date");
                    table.CheckConstraint("CK_Flights_Seats", "available_seats <= total_capacity");
                    table.ForeignKey(
                        name: "FK_flights_Aircraft_AircraftEntityId",
                        column: x => x.AircraftEntityId,
                        principalTable: "Aircraft",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flights_Aircraft_aircraft_id",
                        column: x => x.aircraft_id,
                        principalTable: "Aircraft",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flights_airlines_AirlinesEntityId",
                        column: x => x.AirlinesEntityId,
                        principalTable: "airlines",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flights_airlines_airline_id",
                        column: x => x.airline_id,
                        principalTable: "airlines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flights_flight_status_FlightStatusEntityId",
                        column: x => x.FlightStatusEntityId,
                        principalTable: "flight_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flights_flight_status_flight_state_id",
                        column: x => x.flight_state_id,
                        principalTable: "flight_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flights_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "route_stops",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    route_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    airport_stop_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    order = table.Column<int>(type: "int", nullable: false),
                    stop_duration_min = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AirPortsEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RoutesEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_stops", x => x.id);
                    table.ForeignKey(
                        name: "FK_route_stops_airports_AirPortsEntityId",
                        column: x => x.AirPortsEntityId,
                        principalTable: "airports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_route_stops_airports_airport_stop_id",
                        column: x => x.airport_stop_id,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_route_stops_routes_RoutesEntityId",
                        column: x => x.RoutesEntityId,
                        principalTable: "routes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_route_stops_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_CitiesEntityId",
                table: "addresses",
                column: "CitiesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_city_id",
                table: "addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_viatype_id",
                table: "addresses",
                column: "viatype_id");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_ViaTypesEntityId",
                table: "addresses",
                column: "ViaTypesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_AircraftModelsEntityId",
                table: "Aircraft",
                column: "AircraftModelsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_airline_Id",
                table: "Aircraft",
                column: "airline_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_AirlinesEntityId",
                table: "Aircraft",
                column: "AirlinesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_models_Id",
                table: "Aircraft",
                column: "models_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_tuition",
                table: "Aircraft",
                column: "tuition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_models_AircraftManufacturersEntityId",
                table: "aircraft_models",
                column: "AircraftManufacturersEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_models_manufacturer_id",
                table: "aircraft_models",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_airlines_CountriesEntityId",
                table: "airlines",
                column: "CountriesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_airlines_country_id",
                table: "airlines",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_airlines_iata_code",
                table: "airlines",
                column: "iata_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AirportAirline_airline_Id",
                table: "AirportAirline",
                column: "airline_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AirportAirline_AirlinesEntityId",
                table: "AirportAirline",
                column: "AirlinesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportAirline_airport_Id",
                table: "AirportAirline",
                column: "airport_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AirportAirline_AirPortsEntityId",
                table: "AirportAirline",
                column: "AirPortsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_airports_cities_id",
                table: "airports",
                column: "cities_id");

            migrationBuilder.CreateIndex(
                name: "IX_airports_CitiesEntityId",
                table: "airports",
                column: "CitiesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_airports_iata_code",
                table: "airports",
                column: "iata_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_airports_icao_code",
                table: "airports",
                column: "icao_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_availability_states_name",
                table: "availability_states",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cabin_configuration_aircraft_id",
                table: "cabin_configuration",
                column: "aircraft_id");

            migrationBuilder.CreateIndex(
                name: "IX_cabin_configuration_AircraftEntityId",
                table: "cabin_configuration",
                column: "AircraftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_cabin_configuration_cabin_Type_Id",
                table: "cabin_configuration",
                column: "cabin_Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_cabin_types_name",
                table: "cabin_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cities_name",
                table: "cities",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cities_regions_id",
                table: "cities",
                column: "regions_id");

            migrationBuilder.CreateIndex(
                name: "IX_Continents_name",
                table: "Continents",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_continente_id",
                table: "countries",
                column: "continente_id");

            migrationBuilder.CreateIndex(
                name: "IX_countries_iso_code",
                table: "countries",
                column: "iso_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_people_Id",
                table: "customers",
                column: "people_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_document_types_code",
                table: "document_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_email_domains_domains",
                table: "email_domains",
                column: "domains",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fares_cabin_type_id",
                table: "fares",
                column: "cabin_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_CabinTypesEntityId",
                table: "fares",
                column: "CabinTypesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_fares_passenger_type_id",
                table: "fares",
                column: "passenger_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_PassengerTypesEntityId",
                table: "fares",
                column: "PassengerTypesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_fares_route_id_cabin_type_id_passenger_type_id_season_id",
                table: "fares",
                columns: new[] { "route_id", "cabin_type_id", "passenger_type_id", "season_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fares_RoutesEntityId",
                table: "fares",
                column: "RoutesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_fares_season_id",
                table: "fares",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_SeasonsEntityId",
                table: "fares",
                column: "SeasonsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_name",
                table: "flight_status",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_transitions_destination_state_id",
                table: "flight_status_transitions",
                column: "destination_state_id");

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_transitions_FlightStatusEntityId",
                table: "flight_status_transitions",
                column: "FlightStatusEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_transitions_origin_state_id_destination_state_~",
                table: "flight_status_transitions",
                columns: new[] { "origin_state_id", "destination_state_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flights_aircraft_id",
                table: "flights",
                column: "aircraft_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_AircraftEntityId",
                table: "flights",
                column: "AircraftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_airline_id",
                table: "flights",
                column: "airline_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_AirlinesEntityId",
                table: "flights",
                column: "AirlinesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_flight_code",
                table: "flights",
                column: "flight_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flights_flight_state_id",
                table: "flights",
                column: "flight_state_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_FlightStatusEntityId",
                table: "flights",
                column: "FlightStatusEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_route_id",
                table: "flights",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_passenger_types_name",
                table: "passenger_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_people_address_id",
                table: "people",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_people_document_number",
                table: "people",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_people_documenttype_id",
                table: "people",
                column: "documenttype_id");

            migrationBuilder.CreateIndex(
                name: "IX_people_code_country_code",
                table: "people_code",
                column: "country_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_people_emails_domain_email_id",
                table: "people_emails",
                column: "domain_email_id");

            migrationBuilder.CreateIndex(
                name: "IX_people_emails_EmailDomainsEntityId",
                table: "people_emails",
                column: "EmailDomainsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_people_emails_people_id",
                table: "people_emails",
                column: "people_id");

            migrationBuilder.CreateIndex(
                name: "IX_people_emails_PeopleEntityId",
                table: "people_emails",
                column: "PeopleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_people_phones_people_Id",
                table: "people_phones",
                column: "people_Id");

            migrationBuilder.CreateIndex(
                name: "IX_people_phones_PeopleEntityId",
                table: "people_phones",
                column: "PeopleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_people_phones_phonecode_Id",
                table: "people_phones",
                column: "phonecode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_airline_Id",
                table: "Personal",
                column: "airline_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_AirlinesEntityId",
                table: "Personal",
                column: "AirlinesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_airport_Id",
                table: "Personal",
                column: "airport_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_AirPortsEntityId",
                table: "Personal",
                column: "AirPortsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_people_Id",
                table: "Personal",
                column: "people_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personal_PeopleEntityId",
                table: "Personal",
                column: "PeopleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_PersonalPositionsEntityId",
                table: "Personal",
                column: "PersonalPositionsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_position_Id",
                table: "Personal",
                column: "position_Id");

            migrationBuilder.CreateIndex(
                name: "IX_personalpositions_name",
                table: "personalpositions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_regions_countries_id",
                table: "regions",
                column: "countries_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_stops_airport_stop_id",
                table: "route_stops",
                column: "airport_stop_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_stops_AirPortsEntityId",
                table: "route_stops",
                column: "AirPortsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_route_stops_route_id_order",
                table: "route_stops",
                columns: new[] { "route_id", "order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_stops_RoutesEntityId",
                table: "route_stops",
                column: "RoutesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_routes_airport_destination_id",
                table: "routes",
                column: "airport_destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_routes_airport_origin_id_airport_destination_id",
                table: "routes",
                columns: new[] { "airport_origin_id", "airport_destination_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_routes_AirPortsEntityId",
                table: "routes",
                column: "AirPortsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_seasons_name",
                table: "seasons",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_staff_availability_availabilityState_id",
                table: "staff_availability",
                column: "availabilityState_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_availability_peoble_id",
                table: "staff_availability",
                column: "peoble_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportAirline");

            migrationBuilder.DropTable(
                name: "cabin_configuration");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "fares");

            migrationBuilder.DropTable(
                name: "flight_status_transitions");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "people_emails");

            migrationBuilder.DropTable(
                name: "people_phones");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "route_stops");

            migrationBuilder.DropTable(
                name: "staff_availability");

            migrationBuilder.DropTable(
                name: "cabin_types");

            migrationBuilder.DropTable(
                name: "passenger_types");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "Aircraft");

            migrationBuilder.DropTable(
                name: "flight_status");

            migrationBuilder.DropTable(
                name: "email_domains");

            migrationBuilder.DropTable(
                name: "people_code");

            migrationBuilder.DropTable(
                name: "personalpositions");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "availability_states");

            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "aircraft_models");

            migrationBuilder.DropTable(
                name: "airlines");

            migrationBuilder.DropTable(
                name: "airports");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "document_types");

            migrationBuilder.DropTable(
                name: "aircraft_manufacturers");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "viatypes");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "Continents");
        }
    }
}
