using SmartSchool.API.Models;
using System.Collections.Generic;
using System;

namespace SmartSchool.API.Dtos
{
    public class ProfessorRegistrarDto
    {
        public int Id { get; set; }

        public int Registro { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNasc { get; set; }

        public DateTime DataIni { get; set; } = DateTime.Now;

        public DateTime? DataFim { get; set; } = null;

        public bool Ativo { get; set; } = true;
    }
}
