using AtencionesApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Models.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rol> Roles { get; set; }
    public DbSet<Institucion> Instituciones { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Diagnostico> Diagnosticos { get; set; }
    public DbSet<TipoPrestacionEnfermeria> TiposPrestacionEnfermeria { get; set; }
    public DbSet<TipoPrestacionOdontologia> TiposPrestacionOdontologia { get; set; }
    public DbSet<AtencionEnfermeria> AtencionesEnfermeria { get; set; }
    public DbSet<PrestacionEnfermeria> PrestacionesEnfermeria { get; set; }
    public DbSet<AtencionOdontologia> AtencionesOdontologia { get; set; }
    public DbSet<PrestacionOdontologia> PrestacionesOdontologia { get; set; }
    public DbSet<ValoracionDental> ValoracionesDentales { get; set; }
    public DbSet<OdontogramaEstado> OdontogramaEstados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}