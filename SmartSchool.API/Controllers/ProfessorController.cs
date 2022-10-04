using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Controllers.Models;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        public readonly SmartContext context;
        public ProfessorController(SmartContext context) {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.context.Professores);
        }

        //api/professor/byId?id=
        [HttpGet("byId")]

        public IActionResult GetById(int id)
        {
            Professor professor = this.context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("O Professor especificado não foi encontrado");
            return Ok(professor);
        }

        [HttpGet("byName")]

        public IActionResult GetByName(string nome)
        {
            Professor professor = this.context.Professores.FirstOrDefault(p => p.Nome == nome);
            if (professor == null) return BadRequest("O Professor especificado não foi encontrado");

            return Ok(professor);
        }

        [HttpPost()]

        public IActionResult Post(Professor professor)
        {
            this.context.Add(professor);
            this.context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]

        public IActionResult Put(int id, Professor professor)
        {
            Professor prof = this.context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("O professor especificado não foi encontrado");

            this.context.Update(professor);
            this.context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]

        public IActionResult Patch(int id, Professor professor)
        {
            Professor prof = this.context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("O professor especificado não foi encontrado");

            this.context.Update(professor);
            this.context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            Professor professor = this.context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("O professor especificado não foi encontrado");

            this.context.Remove(professor);
            this.context.SaveChanges();

            return Ok("O professor foi excluído com sucesso");

        }


    }
}
