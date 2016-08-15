
using System;
using System.Collections.Generic;
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


        public Prestador GetPorGuid(Guid guid)
        {
            return _prestadorService.GetPorGuid(guid);
        }

        //retorna o prestador atraves do email
        public Prestador GetPorEmail(string email)
        {
            return _prestadorService.GetPorEmail(email);
        }

        //retorna os pretadores que nao estao ligados ao orçamento selecionado
        public IEnumerable<Prestador> GetPrestadores(int orcamentoId)
        {
            return _prestadorService.GetPrestadores(orcamentoId);
        }

        public IEnumerable<Prestador> GetPrestadoresComServicos()
        {
            return _prestadorService.GetPrestadoresComServicos();
        }

        //retorna todos os prestadores ativos
        public IEnumerable<Prestador> RetornaPrestadoresAtivos()
        {
            return _prestadorService.RetornaPrestadoresAtivos();
        }
    }
}
