using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncidentesAPI.Models
{
    public class Log
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Token { get; set; }
    }
}
