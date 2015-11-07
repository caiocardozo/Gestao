using AutoMapper;
using GestaoDDD.Domain.Entities;
using GestaoDDD.MVC.ViewModels;

namespace GestaoDDD.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override  string ProfileName 
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure() 
        {
            Mapper.CreateMap<Categoria, CategoriaViewModel>();
        }

    }
}