using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;

namespace IncidentesAPI.Services
{
    public class SlaService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public SlaService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public List<Sla> List()
        {
            try
            {
                var slas = _DbContextIncidentes.Sla.Where(x => x.Borrado == false).Include(x => x.Creador).Include(x => x.Modificador).OrderByDescending(x => x.SlaId).ToList();
                return slas;

            }
            catch (Exception error)
            {
                return new List<Sla>();
            }
        }

        public bool Add(Sla Sla)
        {
            try
            {
                _DbContextIncidentes.Sla.Add(Sla);
                _DbContextIncidentes.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }

        public bool Update(Sla Sla)
        {
            try
            {
                var sla = _DbContextIncidentes.Sla.FirstOrDefault(x => x.SlaId == Sla.SlaId);
                sla.Descripcion = Sla.Descripcion;
                sla.CantidadHoras = Sla.CantidadHoras;
                sla.Estatus = Sla.Estatus;
                sla.FechaModificacion = DateTimeOffset.Now;
                sla.ModificadoPor = Sla.ModificadoPor;
                _DbContextIncidentes.SaveChanges();
                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public string Delete(int SlaId)
        {
            try
            {
                var prioridades = _DbContextIncidentes.Prioridad.FirstOrDefault(x => x.SlaId == SlaId);

                if (prioridades == null)
                {
                    var sla = _DbContextIncidentes.Sla.FirstOrDefault(x => x.SlaId == SlaId);
                    sla.Borrado = true;
                    _DbContextIncidentes.SaveChanges();
                    return "yes";
                }
                else
                {
                    return prioridades.ToString();
                }

            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
