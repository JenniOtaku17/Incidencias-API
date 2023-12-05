using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;

namespace IncidentesAPI.Services
{
    public class DepartamentoService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public DepartamentoService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public List<Departamento> List()
        {
            try
            {
                var departamentos = _DbContextIncidentes.Departamento.Where(x => x.Borrado == false).Include(x => x.Creador).Include(x => x.Modificador).OrderByDescending(x => x.DepartamentoId).ToList();
                return departamentos;

            }
            catch (Exception error)
            {
                return new List<Departamento>()
                {
                    new Departamento()
                    {
                        DepartamentoId = 0,
                        Nombre = error.ToString(),
                        Estatus = "f",
                        Borrado = false,
                        FechaRegistro = DateTimeOffset.Now,
                        FechaModificacion = DateTimeOffset.Now,
                        CreadoPor = 1,
                        ModificadoPor = 1
                    }
                };
            }
        }

        public bool Add(Departamento Departamento)
        {
            try
            {
                _DbContextIncidentes.Departamento.Add(Departamento);
                _DbContextIncidentes.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                var message = error;
                return false;
            }
        }

        public bool Update(Departamento Departamento)
        {
            try
            {
                var departamento = _DbContextIncidentes.Departamento.FirstOrDefault(x => x.DepartamentoId == Departamento.DepartamentoId);
                departamento.Nombre = Departamento.Nombre;
                departamento.Estatus = Departamento.Estatus;
                departamento.FechaModificacion = DateTimeOffset.Now;
                departamento.ModificadoPor = Departamento.ModificadoPor;
                _DbContextIncidentes.SaveChanges();
                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public string Delete(int DepartamentoId)
        {
            var puestos = _DbContextIncidentes.Puesto.FirstOrDefault(x => x.DepartamentoId == DepartamentoId);

            try
            {

                if (puestos == null)
                {
                    var departamento = _DbContextIncidentes.Departamento.FirstOrDefault(x => x.DepartamentoId == DepartamentoId);
                    departamento.Borrado = true;
                    _DbContextIncidentes.SaveChanges();
                    return "yes";
                }
                else
                {
                    return puestos.ToString();
                }

            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
