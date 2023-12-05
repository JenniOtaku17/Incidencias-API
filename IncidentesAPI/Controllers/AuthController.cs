using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncidentesAPI.Models;
using IncidentesAPI.Services;
using Microsoft.AspNetCore.Authorization;


namespace IncidentesAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _AuthService;

        public AuthController(AuthService AuthService)
        {
            _AuthService = AuthService;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] Usuario Usuario)
        {
            var result = _AuthService.Login(Usuario.NombreUsuario, Usuario.Contrasena);
            var empty = new Log();

            if(result == empty)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
            
        }
    }
}