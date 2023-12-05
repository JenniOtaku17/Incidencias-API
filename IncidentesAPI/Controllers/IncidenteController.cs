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
    public class IncidenteController : ControllerBase
    {
        private readonly IncidenteService _IncidenteService;

        public IncidenteController(IncidenteService IncidenteService)
        {
            _IncidenteService = IncidenteService;
        }

        [Route("Get/{IncidenteId}")]
        [HttpGet]
        public IActionResult Get(int IncidenteId)
        {
            var result = _IncidenteService.Get(IncidenteId);
            return Ok(result);
        }

        [Route("ListAll")]
        [HttpGet]
        public IActionResult ListAll()
        {
            var result = _IncidenteService.ListAll();
            return Ok(result);
        }

        [Route("ListForMe/{UsuarioAsignadoId}")]
        [HttpGet]
        public IActionResult ListForMe( int UsuarioAsignadoId )
        {
            var result = _IncidenteService.ListForMe(UsuarioAsignadoId);
            return Ok(result);
        }

        [Route("ListFromMe/{UsuarioReportaId}")]
        [HttpGet]
        public IActionResult ListFromMe( int UsuarioReportaId )
        {
            var result = _IncidenteService.ListFromMe(UsuarioReportaId);
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] Incidente IncidentetoAdd)
        {
            var result = _IncidenteService.Add(IncidentetoAdd);
            if (result == "yes")
            {
                return Ok("Added");
            }
            else
            {
                return BadRequest(result);
            }

        }

        [Route("Asign")]
        [HttpPut]
        public IActionResult Asign([FromBody] Incidente IncidentetoUpdate)
        {
            if (_IncidenteService.Asign(IncidentetoUpdate))
            {
                return Ok("Asigned");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Close")]
        [HttpPut]
        public IActionResult Close([FromBody] Incidente IncidentetoUpdate)
        {
            if (_IncidenteService.Close(IncidentetoUpdate))
            {
                return Ok("Closed");
            }
            else
            {
                return BadRequest();
            }
        }

    }
}