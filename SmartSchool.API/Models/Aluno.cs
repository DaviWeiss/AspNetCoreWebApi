using SmartSchool.API.Models;
using System;
using System.Collections.Generic;

namespace SmartSchool.API.Controllers.Models
{
    public class Aluno
    {

        public Aluno() {}

        public Aluno(int id, int matricula, string nome, string sobrenome, string telefone, DateTime dataNasc)
        {
            this.Id = id;
            this.Matricula = matricula;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.DataNasc = dataNasc;
        }
        public int Id { get; set; }

        public int Matricula { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNasc { get; set; }

        public DateTime DataIni { get; set; } = DateTime.Now;

        public DateTime? DataFim { get; set; } = null;

        public bool Ativo { get; set; } = true;


        //Criada essa variável como Ienumerable, para fazer a ligação com disciplinas, ou seja, essa variável basicamente informa para o banco de dados que
        //o aluno tal foi criado e que precisa ter uma disciplina associada a ele. Necessário, pois um aluno pode fazer várias disciplinas e uma disciplina pode ter
        //vários alunos
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}