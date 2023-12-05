using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;

namespace IncidentesAPI.Services
{
    public class PrioridadService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public PrioridadService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public List<Prioridad> List()
        {
            try
            {
                var prioridades = _DbContextIncidentes.Prioridad.Where(x => x.Borrado == false).Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.Sla).OrderByDescending(x => x.PrioridadId).ToList();
                return prioridades;

            }
            catch (Exception error)
            {
                return new List<Prioridad>()
                {
                    new Prioridad(){ Nombre = error.ToString() }
                };
            }
        }

        public bool Add(Prioridad Prioridad)
        {
            try
            {
                _DbContextIncidentes.Prioridad.Add(Prioridad);
                _DbContextIncidentes.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }

        public bool Update(Prioridad Prioridad)
        {
            try
            {
                var prioridad = _DbContextIncidentes.Prioridad.FirstOrDefault(x => x.PrioridadId == Prioridad.PrioridadId);
                prioridad.Nombre = Prioridad.Nombre;
                prioridad.SlaId = Prioridad.SlaId;
                prioridad.Estatus = Prioridad.Estatus;
                prioridad.FechaModificacion = DateTimeOffset.Now;
                prioridad.ModificadoPor = Prioridad.ModificadoPor;
                _DbContextIncidentes.SaveChanges();
                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public string Delete(int PrioridadId)
        {
            try
            {
                var incidentes = _DbContextIncidentes.Incidente.FirstOrDefault(x => x.PrioridadId == PrioridadId);

                if (incidentes == null)
                {
                    var prioridad = _DbContextIncidentes.Prioridad.FirstOrDefault(x => x.PrioridadId == PrioridadId);
                    prioridad.Borrado = true;
                    _DbContextIncidentes.SaveChanges();
                    return "yes";
                }
                else
                {
                    return incidentes.ToString();
                }

            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
