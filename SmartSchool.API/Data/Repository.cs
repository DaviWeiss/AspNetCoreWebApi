using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Controllers.Models;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id);


            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) ||
                aluno.Sobrenome.ToUpper().Contains(pageParams.Nome.ToUpper()));

            if (pageParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

            if (pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));
            /// return await query.ToListAsync();

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }


        public async Task<PageList<Professor>> GetAllProfessoresAsync(PageParams pageParams)
        {
            IQueryable<Professor> query = _context.Professores;

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id);


            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(professor => professor.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) ||
                professor.SobreNome.ToUpper().Contains(pageParams.Nome.ToUpper()));

            if (pageParams.Registro > 0)
                query = query.Where(professor => professor.Registro == pageParams.Registro);

            if (pageParams.Ativo != null)
                query = query.Where(professor => professor.Ativo == (pageParams.Ativo != 0));
            /// return await query.ToListAsync();

            return await PageList<Professor>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
        public Professor[] GetAllProfessores(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                          .OrderBy(aluno => aluno.Id)
                          .Where(aluno => aluno.Disciplinas.Any(
                              d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)
                          ));

            return query.ToArray();
        }
        public Professor GetProfessorById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                          .OrderBy(aluno => aluno.Id)
                          .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }
        public async Task<PageList<Disciplina>> GetAllDsiciplinasAsync(PageParams pageParams)
        {
            IQueryable<Disciplina> query = _context.Disciplinas;

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.NomeDisciplina))
                query = query.Where(disciplina => disciplina.Nome.ToUpper().Contains(pageParams.NomeDisciplina.ToUpper()));

            if (pageParams.idDisciplina > 0)
                query = query.Where(professor => professor.Id == pageParams.idDisciplina);

            return await PageList<Disciplina>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Disciplina GetDisciplinaById(int disciplinaId)
        {
            IQueryable<Disciplina> query = _context.Disciplinas;

            query = query.AsNoTracking()
                          .OrderBy(disciplina => disciplina.Id)
                          .Where(disciplina => disciplina.Id == disciplinaId);

            return query.FirstOrDefault();
        }
    }
}
