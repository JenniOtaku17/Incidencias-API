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
    public class HistorialIncidenteController : ControllerBase
    {
        private readonly HistorialIncidenteService _HistorialIncidenteService;

        public HistorialIncidenteController(HistorialIncidenteService HistorialIncidenteService)
        {
            _HistorialIncidenteService = HistorialIncidenteService;
        }

        [Route("List/{IncidenteID}")]
        [HttpGet]
        public IActionResult List(int IncidenteId)
        {
            var result = _HistorialIncidenteService.List(IncidenteId);
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] HistorialIncidente HistorialIncidentetoAdd)
        {
            var result = _HistorialIncidenteService.Add(HistorialIncidentetoAdd);
            if (result == "yes")
            {
                return Ok("Added");
            }
            else
            {
                return BadRequest(result);
            }

        }
    }
}