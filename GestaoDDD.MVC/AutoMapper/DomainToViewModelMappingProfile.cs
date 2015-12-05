

using AutoMapper;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        //public override string ProfileName
        //{
        //    get { return "ViewModelToDomainMappings"; }
        //}

        protected override void Configure()
        {
            //mapeia de viewModel para classe
            Mapper.CreateMap<CategoriaViewModel, Categoria>();
            Mapper.CreateMap<ServicoViewModel, Servico>();
        }
    }
}