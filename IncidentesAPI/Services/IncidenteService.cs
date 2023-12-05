using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;

namespace IncidentesAPI.Services
{
    public class IncidenteService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public IncidenteService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public Incidente Get(int IncidenteId)
        {
            try
            {
                var incidente = _DbContextIncidentes.Incidente.Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.Departamento).Include(x => x.Prioridad).Include(x => x.Prioridad.Sla).Include(x => x.UsuarioAsignado).Include(x => x.UsuarioReporta).FirstOrDefault(x => x.Borrado == false && x.IncidenteId == IncidenteId);
                return incidente;

            }
            catch (Exception error)
            {
                return new Incidente();
            }
        }

        public List<Incidente> ListAll()
        {
            try
            {
                var incidentes = _DbContextIncidentes.Incidente.Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.UsuarioReporta).Include(x => x.UsuarioAsignado).Include(x => x.Prioridad).Include(x => x.Prioridad.Sla).Include(x => x.Departamento).OrderBy(x => x.FechaCierre).ToList();
                return incidentes;

            }
            catch (Exception error)
            {
                return new List<Incidente>()
                {
                    new Incidente(){ Titulo= error.ToString() }
                };
            }
        }

        public List<Incidente> ListForMe( int UsuarioAsignadoId )
        {
            try
            {
                var incidentes = _DbContextIncidentes.Incidente.Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.UsuarioReporta).Include(x => x.UsuarioAsignado).Include(x => x.Prioridad).Include(x => x.Departamento).OrderBy(x => x.FechaCierre).Where( x => x.UsuarioAsignadoId == UsuarioAsignadoId).ToList();
                return incidentes;

            }
            catch (Exception error)
            {
                return new List<Incidente>();
            }
        }

        public List<Incidente> ListFromMe( int UsuarioReportaId)
        {
            try
            {
                var incidentes = _DbContextIncidentes.Incidente.Where(x => x.UsuarioReportaId == UsuarioReportaId).Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.UsuarioReporta).Include(x => x.UsuarioAsignado).Include(x => x.Prioridad).Include(x => x.Departamento).OrderBy(x => x.FechaCierre).ToList();
                return incidentes;

            }
            catch (Exception error)
            {
                return new List<Incidente>();
            }
        }

        public string Add(Incidente Incidente)
        {
            try
            {
                _DbContextIncidentes.Incidente.Add(Incidente);
                _DbContextIncidentes.SaveChanges();
                return "yes";
            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }

        public bool Update(Incidente Incidente)
        {
            try
            {
                var incidente = _DbContextIncidentes.Incidente.FirstOrDefault(x => x.IncidenteId == Incidente.IncidenteId);
                incidente.Titulo = Incidente.Titulo;
                incidente.PrioridadId = Incidente.PrioridadId;
                incidente.DepartamentoId = Incidente.DepartamentoId;
                incidente.Descripcion = Incidente.Descripcion;
                incidente.FechaModificacion = DateTimeOffset.Now;
                incidente.ModificadoPor = Incidente.ModificadoPor;
                _DbContextIncidentes.SaveChanges();

                var historial = new HistorialIncidente()
                {
                    IncidenteId = Incidente.IncidenteId,
                    Comentario = "Actualizó los datos del incidente.",
                    FechaRegistro = DateTimeOffset.Now,
                    CreadoPor = Convert.ToInt32(Incidente.ModificadoPor)
                };
                _DbContextIncidentes.HistorialIncidente.Add(historial);
                _DbContextIncidentes.SaveChanges();

                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public bool Asign(Incidente Incidente)
        {
            try
            {
                var incidente = _DbContextIncidentes.Incidente.FirstOrDefault(x => x.IncidenteId == Incidente.IncidenteId);
                incidente.UsuarioAsignadoId = Incidente.UsuarioAsignadoId;
                incidente.FechaModificacion = DateTimeOffset.Now;
                incidente.ModificadoPor = Incidente.ModificadoPor;
                _DbContextIncidentes.SaveChanges();

                var historial = new HistorialIncidente()
                {
                    IncidenteId = Incidente.IncidenteId,
                    Comentario = "Asignó el incidente al usuario con Código:" + Incidente.UsuarioAsignadoId.ToString(),
                    CreadoPor = Convert.ToInt32(Incidente.ModificadoPor),
                    Color = "cyan"
                };
                _DbContextIncidentes.HistorialIncidente.Add(historial);
                _DbContextIncidentes.SaveChanges();

                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public bool Close(Incidente Incidente)
        {
            try
            {
                var incidente = _DbContextIncidentes.Incidente.FirstOrDefault(x => x.IncidenteId == Incidente.IncidenteId);
                incidente.FechaCierre = DateTime.Now;
                incidente.ComentarioCierre = Incidente.ComentarioCierre;
                incidente.Estatus = "I";
                incidente.FechaModificacion = DateTimeOffset.Now;
                incidente.ModificadoPor = Incidente.ModificadoPor;
                _DbContextIncidentes.SaveChanges();

                var historial = new HistorialIncidente()
                {
                    IncidenteId = Incidente.IncidenteId,
                    Comentario = "Cerró el incidente.",
                    FechaRegistro = DateTimeOffset.Now,
                    CreadoPor = Convert.ToInt32(Incidente.ModificadoPor),
                    Color = "cyan"
                };
                _DbContextIncidentes.HistorialIncidente.Add(historial);
                _DbContextIncidentes.SaveChanges();

                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

    }
}
