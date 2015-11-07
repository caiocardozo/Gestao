using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoDDD.MVC.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(m =>
            {
                m.AddProfile<DomainToViewModelMappingProfile>();
                m.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}