using AutoMapper;
using SmartSchool.API.Controllers.Models;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                    .ForMember(
                        dest => dest.Nome,
                        opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")       
                    ).ForMember(dest => dest.Idade,
                                opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                    );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                    .ForMember(
                       dest => dest.Nome,
                       opt => opt.MapFrom(src => $"{src.Nome} {src.SobreNome}"))
                    .ForMember(
                        dest => dest.Idade,
                        opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                    );
            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();

            CreateMap<Disciplina, DisciplinaDto>().ReverseMap();
            CreateMap<Disciplina, DisciplinaRegistrarDto>().ReverseMap();
        }
    }
}
