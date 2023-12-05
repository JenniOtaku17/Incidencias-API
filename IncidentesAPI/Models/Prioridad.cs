﻿using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentesAPI.Models
{
    public class Prioridad
    {
        public int PrioridadId { get; set; }
        public int SlaId { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public bool Borrado { get; set; }
        public DateTimeOffset FechaRegistro { get; set; }
        public DateTimeOffset? FechaModificacion { get; set; }
        public int CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }

        [ForeignKey("SlaId")]
        public Sla Sla { get; set; }
        [ForeignKey("CreadoPor")]
        public Usuario Creador { get; set; }
        [ForeignKey("ModificadoPor")]
        public Usuario Modificador { get; set; }

        public class Map
        {

            public Map(EntityTypeBuilder<Prioridad> etb)
            {
                etb.HasKey(x => x.PrioridadId);
                etb.Property(x => x.SlaId).HasColumnName("SlaId").HasColumnType("int");
                etb.Property(x => x.Nombre).HasColumnName("Nombre").HasMaxLength(50);
                etb.Property(x => x.Estatus).HasColumnName("Estatus").HasMaxLength(2);
                etb.Property(x => x.Borrado).HasColumnName("Borrado").HasColumnType("bit");
                etb.Property(x => x.FechaRegistro).HasColumnName("FechaRegistro").HasColumnType("DateTimeOffset").HasDefaultValue(DateTimeOffset.Now);
                etb.Property(x => x.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTimeOffset");
                etb.Property(x => x.CreadoPor).HasColumnName("CreadoPor").HasColumnType("int");
                etb.Property(x => x.ModificadoPor).HasColumnName("ModificadoPor").HasColumnType("int");
                etb.HasOne(x => x.Sla);
                etb.HasOne(x => x.Creador);
                etb.HasOne(x => x.Modificador);
            }
        }
    }
}
