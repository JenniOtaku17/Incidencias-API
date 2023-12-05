using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentesAPI.Models
{
    public class HistorialIncidente
    {
        public int HistorialIncidenteId { get; set; }
        public int IncidenteId { get; set; }
        public string Comentario { get; set; }
        public string Color { get; set; }
        public string Estatus { get; set; }
        public bool Borrado { get; set; }
        public DateTimeOffset FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }

        [ForeignKey("IncidenteId")]
        public Incidente Incidente { get; set; }
        [ForeignKey("CreadoPor")]
        public Usuario Creador { get; set; }
        [ForeignKey("ModificadoPor")]
        public Usuario Modificador { get; set; }

        public class Map
        {

            public Map(EntityTypeBuilder<HistorialIncidente> etb)
            {
                etb.HasKey(x => x.HistorialIncidenteId);
                etb.Property(x => x.IncidenteId).HasColumnName("IncidenteId").HasColumnType("int");
                etb.Property(x => x.Comentario).HasColumnName("Comentario").HasMaxLength(500);
                etb.Property(x => x.Color).HasColumnName("Color").HasMaxLength(10);
                etb.Property(x => x.Estatus).HasColumnName("Estatus").HasMaxLength(2);
                etb.Property(x => x.Borrado).HasColumnName("Borrado").HasColumnType("bit");
                etb.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro").HasColumnType("DateTimeOffset").HasDefaultValue(DateTimeOffset.Now);
                etb.Property(x => x.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTimeOffset");
                etb.Property(x => x.CreadoPor).HasColumnName("CreadoPor").HasColumnType("int");
                etb.Property(x => x.ModificadoPor).HasColumnName("ModificadoPor").HasColumnType("int");
                etb.HasOne(x => x.Incidente);
                etb.HasOne(x => x.Creador);
                etb.HasOne(x => x.Modificador);
            }
        }
    }
}
