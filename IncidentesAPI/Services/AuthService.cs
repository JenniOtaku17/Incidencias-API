using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Persistance;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace IncidentesAPI.Services
{
    public class AuthService
    {
        private readonly DbContextIncidentes _DbContextIncidentes;
        private readonly IConfiguration _configuration;

        public AuthService(DbContextIncidentes DbContextIncidentes, IConfiguration configuration)
        {
            _DbContextIncidentes = DbContextIncidentes;
            _configuration = configuration;
        }

        public Log Login(string user, string password)
        {
            try
            {
                var usuario = _DbContextIncidentes.Usuario.FirstOrDefault(x => x.Contrasena == password && (x.NombreUsuario == user ||  x.Correo == user));
                var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                        new Claim(ClaimTypes.Email, usuario.Correo)
                    }),

                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddDays(1),
                    // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(createdToken);

                var result = new Log()
                {
                    UsuarioId = usuario.UsuarioId,
                    NombreUsuario = usuario.NombreUsuario,
                    Token = token
                };

                return result;

            }
            catch (Exception error)
            {
                return new Log()
                {
                    UsuarioId = 0,
                    NombreUsuario = error.ToString(),
                    Token = null
                };
            }
        }

    }
}
