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
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _UsuarioService;

        public UsuarioController(UsuarioService UsuarioService)
        {
            _UsuarioService = UsuarioService;
        }

        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _UsuarioService.List();
            return Ok(result);
        }

        [Route("Get/{UsuarioId}")]
        [HttpGet]
        public IActionResult Get(int UsuarioId)
        {
            var result = _UsuarioService.Get(UsuarioId);
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] Usuario UsuariotoAdd)
        {
            if (_UsuarioService.Add(UsuariotoAdd))
            {
                return Ok("Added");
            }
            else
            {
                return BadRequest();
            }

        }

        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] Usuario UsuariotoUpdate)
        {
            if (_UsuarioService.Update(UsuariotoUpdate))
            {
                return Ok("Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Delete/{UsuarioId}")]
        [HttpDelete]
        public IActionResult Delete(int UsuarioId)
        {
            var result = _UsuarioService.Delete(UsuarioId);
            if (result == "yes")
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest(new { error = "No es posible eliminar el usuario, existen demas entidades vinculadas al mismo" });
            }
        }
    }
}