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
    public class DepartamentoController : ControllerBase
    {

        private readonly DepartamentoService _DepartamentoService;

        public DepartamentoController(DepartamentoService DepartamentoService)
        {
            _DepartamentoService = DepartamentoService;
        }

        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _DepartamentoService.List();
            return Ok(result);
        }

        [Route("Add")] 
        [HttpPost]
        public IActionResult Add([FromBody] Departamento DepartamentotoAdd) {
            if (_DepartamentoService.Add(DepartamentotoAdd))
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
        public IActionResult Update([FromBody] Departamento DepartamentotoUpdate)
        {
            if (_DepartamentoService.Update(DepartamentotoUpdate))
            {
                return Ok("Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("Delete/{DepartamentoID}")]
        [HttpDelete]
        public IActionResult Delete(int DepartamentoID)
        {
            var result = _DepartamentoService.Delete(DepartamentoID);
            if (result == "yes")
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest(new { error= "No es posible eliminar el departamento, existen demas entidades vinculadas al mismo" });
            }
        }
    }
}