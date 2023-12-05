using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;

namespace IncidentesAPI.Services
{
    public class HistorialIncidenteService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public HistorialIncidenteService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public List<HistorialIncidente> List( int IncidenteId )
        {
            try
            {
                var historialIncidentes = _DbContextIncidentes.HistorialIncidente.Where(x => x.IncidenteId == IncidenteId).Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.Incidente).OrderByDescending(x => x.HistorialIncidenteId).ToList();
                return historialIncidentes;

            }
            catch (Exception error)
            {
                return new List<HistorialIncidente>();
            }
        }

        public string Add(HistorialIncidente HistorialIncidente)
        {
            try
            {
                _DbContextIncidentes.HistorialIncidente.Add(HistorialIncidente);
                _DbContextIncidentes.SaveChanges();
                return "yes";
            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }

    }
}
