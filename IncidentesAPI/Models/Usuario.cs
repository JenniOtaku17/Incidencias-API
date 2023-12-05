using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentesAPI.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int? PuestoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Estatus { get; set; }
        public bool Borrado { get; set; }
        public DateTimeOffset FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }

        [ForeignKey("PuestoId")]
        public Puesto Puesto { get; set; }
        [ForeignKey("CreadoPor")]
        public Usuario Creador { get; set; }
        [ForeignKey("ModificadoPor")]
        public Usuario Modificador { get; set; }

        public class Map
        {
            public Map(EntityTypeBuilder<Usuario> etb)
            {
                etb.HasKey(x => x.UsuarioId);
                etb.Property(x => x.PuestoId).HasColumnName("PuestoId").HasColumnType("int");
                etb.Property(x => x.Nombre).HasColumnName("Nombre").HasMaxLength(100);
                etb.Property(x => x.Apellido).HasColumnName("Apellido").HasMaxLength(100);
                etb.Property(x => x.Cedula).HasColumnName("Cedula").HasMaxLength(11);
                etb.Property(x => x.Correo).HasColumnName("Correo").HasMaxLength(50);
                etb.Property(x => x.Telefono).HasColumnName("Telefono").HasMaxLength(15);
                etb.Property(x => x.FechaNacimiento).HasColumnName("FechaNacimiento").HasColumnType("Datetime");
                etb.Property(x => x.NombreUsuario).HasColumnName("NombreUsuario").HasMaxLength(100);
                etb.Property(x => x.Contrasena).HasColumnName("Contrasena").HasMaxLength(500);
                etb.Property(x => x.Estatus).HasColumnName("Estatus").HasMaxLength(2);
                etb.Property(x => x.Borrado).HasColumnName("Borrado").HasColumnType("bit");
                etb.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro").HasColumnType("DateTimeOffset").HasDefaultValue(DateTimeOffset.Now);
                etb.Property(x => x.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTimeOffset");
                etb.Property(x => x.CreadoPor).HasColumnName("CreadoPor").HasColumnType("int");
                etb.Property(x => x.ModificadoPor).HasColumnName("ModificadoPor").HasColumnType("int");
                etb.HasOne(x => x.Puesto);
                etb.HasOne(x => x.Creador);
                etb.HasOne(x => x.Modificador);
            }
        }
    }
}
