using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Controllers.Models;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>()
        {
            new Aluno()
            {
                Id = 1,
                Nome = "Marcos",
                Sobrenome = "Almeida",
                Telefone = "923908987"
            },
            new Aluno()
            {
                Id = 2,
                Nome = "Maria",
                Sobrenome = "Kent",
                Telefone = "923908985"
            },
            new Aluno()
            {
                Id = 3,
                Nome = "Laura",
                Sobrenome = "Maria",
                Telefone = "923908984"
            },
        };

        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            Aluno aluno = Alunos.FirstOrDefault(res => res.Id == id);
            if(aluno == null)
            {
                return BadRequest("O aluno especificado não foi encontrado");
            }

            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            Aluno aluno = Alunos.FirstOrDefault(res => res.Nome.Contains(nome) && res.Sobrenome.Contains(sobrenome));
            if (aluno == null)
            {
                return BadRequest("O aluno especificado não foi encontrado");
            }

            return Ok(aluno);
        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("O Aluno foi excluído com sucesso");
        }

    }
}
