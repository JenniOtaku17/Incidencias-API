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
    public class PuestoController : ControllerBase
    {
        private readonly PuestoService _PuestoService;

        public PuestoController(PuestoService PuestoService)
        {
            _PuestoService = PuestoService;
        }

        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _PuestoService.List();
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] Puesto PuestotoAdd)
        {
            if (_PuestoService.Add(PuestotoAdd))
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
        public IActionResult Update([FromBody] Puesto PuestotoUpdate)
        {
            if (_PuestoService.Update(PuestotoUpdate))
            {
                return Ok("Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Delete/{PuestoID}")]
        [HttpDelete]
        public IActionResult Delete(int PuestoID)
        {
            var result = _PuestoService.Delete(PuestoID);
            if (result == "yes")
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest(new { error = "No es posible eliminar el departamento, existen demas entidades vinculadas al mismo" });
            }
        }
    }
}