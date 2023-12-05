using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;
using System.Threading.Tasks;

namespace IncidentesAPI.Services
{
    public class PuestoService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public PuestoService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public List<Puesto> List()
        {
            try
            {
                var puestos = _DbContextIncidentes.Puesto.Where(x => x.Borrado == false).Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.Departamento).OrderByDescending(x => x.PuestoId).ToList();
                return puestos;

            }
            catch (Exception error)
            {
                return new List<Puesto>();
            }
        }

        public bool Add(Puesto Puesto)
        {
            try
            {
                _DbContextIncidentes.Puesto.Add(Puesto);
                _DbContextIncidentes.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }

        public bool Update(Puesto Puesto)
        {
            try
            {
                var puesto = _DbContextIncidentes.Puesto.FirstOrDefault(x => x.PuestoId == Puesto.PuestoId);
                puesto.Nombre = Puesto.Nombre;
                puesto.DepartamentoId = Puesto.DepartamentoId;
                Puesto.Estatus = Puesto.Estatus;
                puesto.FechaModificacion = DateTimeOffset.Now;
                puesto.ModificadoPor = Puesto.ModificadoPor;
                _DbContextIncidentes.SaveChanges();
                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public string Delete(int PuestoId)
        {
            try
            {
                var usuarios = _DbContextIncidentes.Usuario.FirstOrDefault(x => x.PuestoId == PuestoId);

                if (usuarios == null)
                {
                    var puesto = _DbContextIncidentes.Puesto.FirstOrDefault(x => x.PuestoId == PuestoId);
                    puesto.Borrado = true;
                    _DbContextIncidentes.SaveChanges();
                    return "yes";
                }
                else
                {
                    return usuarios.ToString();
                }

            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
