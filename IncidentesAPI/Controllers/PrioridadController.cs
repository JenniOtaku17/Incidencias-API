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
    public class PrioridadController : ControllerBase
    {
        private readonly PrioridadService _PrioridadService;

        public PrioridadController(PrioridadService PrioridadService)
        {
            _PrioridadService = PrioridadService;
        }

        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _PrioridadService.List();
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] Prioridad PrioridadtoAdd)
        {
            if (_PrioridadService.Add(PrioridadtoAdd))
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
        public IActionResult Update([FromBody] Prioridad PrioridadtoUpdate)
        {
            if (_PrioridadService.Update(PrioridadtoUpdate))
            {
                return Ok("Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Delete/{PrioridadID}")]
        [HttpDelete]
        public IActionResult Delete(int PrioridadID)
        {
            var result = _PrioridadService.Delete(PrioridadID);
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