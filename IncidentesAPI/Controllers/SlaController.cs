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
    public class SlaController : ControllerBase
    {
        private readonly SlaService _SlaService;

        public SlaController(SlaService SlaService)
        {
            _SlaService = SlaService;
        }

        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _SlaService.List();
            return Ok(result);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] Sla SlatoAdd)
        {
            if (_SlaService.Add(SlatoAdd))
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
        public IActionResult Update([FromBody] Sla SlatoUpdate)
        {
            if (_SlaService.Update(SlatoUpdate))
            {
                return Ok("Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Delete/{SlaID}")]
        [HttpDelete]
        public IActionResult Delete(int SlaID)
        {
            var result = _SlaService.Delete(SlaID);
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