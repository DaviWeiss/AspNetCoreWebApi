using SmartSchool.API.Models;
using System.Collections.Generic;
using System;

namespace SmartSchool.API.Dtos
{
    public class DisciplinaDto
    {
        public string Nome { get; set; }

        public int CargaHoraria { get; set; }

        public Disciplina Prerequisito { get; set; }
        public int ProfessorId { get; set; }

        public Professor Professor { get; set; }

        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}
