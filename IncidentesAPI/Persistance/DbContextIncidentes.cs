using System;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncidentesAPI.Persistance
{
    public class DbContextIncidentes: DbContext
    {
        public DbContextIncidentes(DbContextOptions options) : base(options) { }

        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<HistorialIncidente> HistorialIncidente { get; set; }
        public virtual DbSet<Incidente> Incidente { get; set; }
        public virtual DbSet<Prioridad> Prioridad { get; set; }
        public virtual DbSet<Puesto> Puesto { get; set; }
        public virtual DbSet<Sla> Sla { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder MB)
        {
            new Departamento.Map(MB.Entity<Departamento>());
            new HistorialIncidente.Map(MB.Entity<HistorialIncidente>());
            new Incidente.Map(MB.Entity<Incidente>());
            new Sla.Map(MB.Entity<Sla>());
            new Prioridad.Map(MB.Entity<Prioridad>());
            new Puesto.Map(MB.Entity<Puesto>());
            new Usuario.Map(MB.Entity<Usuario>());
            base.OnModelCreating(MB);
        }
    }
}
