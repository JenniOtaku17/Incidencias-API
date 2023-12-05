using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentesAPI.Models
{
    public class Incidente
    {
        public int IncidenteId { get; set; }
        public int UsuarioReportaId { get; set; }
        public int? UsuarioAsignadoId { get; set; }
        public int PrioridadId { get; set; }
        public int DepartamentoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTimeOffset? FechaCierre { get; set; }
        public string ComentarioCierre { get; set; }
        public string Estatus { get; set; }
        public bool Borrado { get; set; }
        public DateTimeOffset FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }

        [ForeignKey("UsuarioReportaId")]
        public Usuario UsuarioReporta { get; set; }
        [ForeignKey("UsuarioAsignadoId")]
        public Usuario UsuarioAsignado { get; set; }
        [ForeignKey("PrioridadId")]
        public Prioridad Prioridad { get; set; }
        [ForeignKey("DepartamentoId")]
        public Departamento Departamento { get; set; }
        [ForeignKey("CreadoPor")]
        public Usuario Creador { get; set; }
        [ForeignKey("ModificadoPor")]
        public Usuario Modificador { get; set; }

        public class Map
        {
            public Map(EntityTypeBuilder<Incidente> etb)
            {
                etb.HasKey(x => x.IncidenteId);
                etb.Property(x => x.UsuarioReportaId).HasColumnName("UsuarioReportaId").HasColumnType("int");
                etb.Property(x => x.UsuarioAsignadoId).HasColumnName("UsuarioAsignadoId").HasColumnType("int");
                etb.Property(x => x.PrioridadId).HasColumnName("PrioridadId").HasColumnType("int");
                etb.Property(x => x.DepartamentoId).HasColumnName("DepartamentoId").HasColumnType("int");
                etb.Property(x => x.Titulo).HasColumnName("Titulo").HasMaxLength(200);
                etb.Property(x => x.Descripcion).HasColumnName("Descripcion").HasMaxLength(int.MaxValue);
                etb.Property(x => x.FechaCierre).HasColumnName("FechaCierre").HasColumnType("DateTimeOffset");
                etb.Property(x => x.ComentarioCierre).HasColumnName("ComentarioCierre").HasMaxLength(500);
                etb.Property(x => x.Estatus).HasColumnName("Estatus").HasMaxLength(2);
                etb.Property(x => x.Borrado).HasColumnName("Borrado").HasColumnType("bit");
                etb.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro").HasColumnType("DateTimeOffset").HasDefaultValue(DateTimeOffset.Now);
                etb.Property(x => x.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTimeOffset");
                etb.Property(x => x.CreadoPor).HasColumnName("CreadoPor").HasColumnType("int");
                etb.Property(x => x.ModificadoPor).HasColumnName("ModificadoPor").HasColumnType("int");
                etb.HasOne(x => x.UsuarioReporta);
                etb.HasOne(x => x.UsuarioAsignado);
                etb.HasOne(x => x.Prioridad);
                etb.HasOne(x => x.Departamento);
                etb.HasOne(x => x.Creador);
                etb.HasOne(x => x.Modificador);
            }
        }
    }
}
