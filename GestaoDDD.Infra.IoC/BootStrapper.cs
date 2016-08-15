using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Application.Interface;
using GestaoDDD.Infra.Data.Repositories;
using GestaoDDD.Infra.Identity.Configuration;
using GestaoDDD.Infra.Identity.Context;
using GestaoDDD.Infra.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using GestaoDDD.Application.Services;
using GestaoDDD.Domain.Interfaces.Services;
using GestaoDDD.Domain.Services;
using GestaoDDD.Infra.Data.Contexto;


namespace GestaoDDD.Infra.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            

        } 
    }
}
