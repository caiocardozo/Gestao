
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
namespace GestaoDDD.Application.Services
{
    public class PrestadorAppService : AppServiceBase<Prestador>, IPrestadorAppService
    {
        private readonly IPrestadorService _prestadorService;
        public PrestadorAppService(IPrestadorService prestadorService)
            :base(prestadorService)
        {
            _prestadorService = prestadorService;
        }

        public Prestador GetPorCpf(string cpf) 
        {
          //  return GetPorCpf(cpf); lembra sempre de chamar dentro das interfaces e classes _prestador...
            return _prestadorService.GetPorCpf(cpf);
        }


        public Prestador GetPorGuid(string guid)
        {
            return _prestadorService.GetPorGuid(guid);
        }

        //retorna o prestador atraves do email
        public Prestador GetPorEmail(string email)
        {
            return _prestadorService.GetPorEmail(email);
        }
    }
}
