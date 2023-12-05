using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;

namespace IncidentesAPI.Services
{
    public class UsuarioService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;

        public UsuarioService(DbContextIncidentes DbContextIncidentes)
        {
            _DbContextIncidentes = DbContextIncidentes;
        }

        public List<Usuario> List()
        {
            try
            {
                var usuarios = _DbContextIncidentes.Usuario.Where(x => x.Borrado == false).Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.Puesto).OrderByDescending(x => x.UsuarioId).ToList();
                return usuarios;

            }
            catch (Exception error)
            {
                return new List<Usuario>();
            }
        }

        public Usuario Get( int UsuarioId )
        {
            try
            {
                var usuario = _DbContextIncidentes.Usuario.Include(x => x.Creador).Include(x => x.Modificador).Include(x => x.Puesto).FirstOrDefault(x => x.Borrado == false && x.UsuarioId == UsuarioId);
                return usuario;

            }
            catch (Exception error)
            {
                return new Usuario();
            }
        }

        public bool Add(Usuario Usuario)
        {
            try
            {
                _DbContextIncidentes.Usuario.Add(Usuario);
                _DbContextIncidentes.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                return false;
            }
        }

        public bool Update(Usuario Usuario)
        {
            try
            {
                var usuario = _DbContextIncidentes.Usuario.FirstOrDefault(x => x.UsuarioId == Usuario.UsuarioId);
                usuario.PuestoId = Usuario.PuestoId;
                usuario.Nombre = Usuario.Nombre;
                usuario.Apellido = Usuario.Apellido;
                usuario.Cedula = Usuario.Cedula;
                usuario.Correo = Usuario.Correo;
                usuario.Telefono = Usuario.Telefono;
                usuario.FechaNacimiento = Usuario.FechaNacimiento;
                usuario.NombreUsuario = Usuario.NombreUsuario;
                usuario.Contrasena = Usuario.Contrasena;
                usuario.Estatus = Usuario.Estatus;
                usuario.FechaModificacion = DateTimeOffset.Now;
                usuario.ModificadoPor = Usuario.ModificadoPor;
                _DbContextIncidentes.SaveChanges();
                return true;

            }
            catch (Exception error)
            {
                return false;
            }
        }

        public string Delete(int UsuarioId)
        {
            try
            {
                var departamentos = _DbContextIncidentes.Departamento.FirstOrDefault(x => x.CreadoPor == UsuarioId || x.ModificadoPor == UsuarioId);
                var historiales = _DbContextIncidentes.HistorialIncidente.FirstOrDefault(x => x.CreadoPor == UsuarioId );
                var incidentes = _DbContextIncidentes.Incidente.FirstOrDefault(x => x.CreadoPor == UsuarioId || x.ModificadoPor == UsuarioId || x.UsuarioReportaId == UsuarioId || x.UsuarioAsignadoId == UsuarioId);
                var prioridades = _DbContextIncidentes.Prioridad.FirstOrDefault(x => x.CreadoPor == UsuarioId || x.ModificadoPor == UsuarioId);
                var puestos = _DbContextIncidentes.Puesto.FirstOrDefault(x => x.CreadoPor == UsuarioId || x.ModificadoPor == UsuarioId);
                var slas = _DbContextIncidentes.Sla.FirstOrDefault(x => x.CreadoPor == UsuarioId || x.ModificadoPor == UsuarioId);
                var usuarios = _DbContextIncidentes.Usuario.FirstOrDefault(x => x.CreadoPor == UsuarioId || x.ModificadoPor == UsuarioId);

                if (prioridades == null && historiales == null && incidentes == null && prioridades == null && puestos == null && slas == null && usuarios == null)
                {
                    var usuario = _DbContextIncidentes.Usuario.FirstOrDefault(x => x.UsuarioId == UsuarioId);
                    usuario.Borrado = true;
                    _DbContextIncidentes.SaveChanges();
                    return "yes";
                }
                else
                {
                    return "error";
                }

            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
