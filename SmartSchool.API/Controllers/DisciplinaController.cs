using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Controllers.Models;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public DisciplinaController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var disciplinas = await _repo.GetAllDsiciplinasAsync(pageParams);

            var disciplinasResult = _mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas);
            Response.AddPagination(disciplinas.CurrentPage, disciplinas.PageSize, disciplinas.TotalCount, disciplinas.TotalPages);

            return Ok(disciplinasResult);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var disciplina = _repo.GetDisciplinaById(id);
            if (disciplina == null)
            {
                return BadRequest("A disciplina especificada não foi encontrada");
            }

            var disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);
            return Ok(disciplinaDto);
        }

        [HttpPost()]
        public IActionResult Post(DisciplinaRegistrarDto model)
        {
            var disciplina = _mapper.Map<Disciplina>(model);

            _repo.Add(disciplina);
            if (_repo.SaveChanges())
            {
                return Created($"/api/disciplina/{model.Id}", _mapper.Map<DisciplinaDto>(disciplina));
            }

            return BadRequest("Disicplina não cadastrada");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, DisciplinaRegistrarDto model)
        {
            var disciplina = _repo.GetDisciplinaById(id);
            if (disciplina == null) return BadRequest("Disciplina não encontrada");

            _mapper.Map(model, disciplina);

            _repo.Update(disciplina);
            if (_repo.SaveChanges())
            {
                return Created($"/api/disciplina/{model.Id}", _mapper.Map<DisciplinaDto>(disciplina));
            }

            return BadRequest("Disicplina não modificada");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, DisciplinaRegistrarDto model)
        {
            var disciplina = _repo.GetDisciplinaById(id);
            if (disciplina == null) return BadRequest("Disciplina não encontrada");

            _mapper.Map(model, disciplina);

            _repo.Update(disciplina);
            if (_repo.SaveChanges())
            {
                return Created($"/api/disciplina/{model.Id}", _mapper.Map<DisciplinaDto>(disciplina));
            }

            return BadRequest("Disicplina não modificada");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var disciplina = _repo.GetDisciplinaById(id);
            if (disciplina == null) return BadRequest("Disciplina não encontrada");

            _repo.Delete(disciplina);
            if (_repo.SaveChanges())
            {
                return Ok("Disicplina deletada");
            }

            return BadRequest("Disicplina não deletada");
        }

    }
}
