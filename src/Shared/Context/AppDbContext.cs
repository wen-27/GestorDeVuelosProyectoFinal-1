using System;
using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Entity;



namespace GestorDeVuelosProyectoFinal.src.Shared.Context;

public class AppDbContext : DbContext
{
    public DbSet<ContinentEntity> Continents { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<RegionEntity> Regions { get; set; }
    public DbSet<AircraftModelsEntity> AircraftModels { get; set; } 


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplica todas las configuraciones de la carpeta infrastructure/Entity
        modelBuilder.ApplyConfiguration(new ContinentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RegionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CountryEntityConfiguration());
    }

}

