using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;
using System;

namespace GestaoDDD.Domain.Services
{
    public class PrestadorService : ServiceBase<Prestador>, IPrestadorService
    {
        private readonly IPrestadorRepository _prestadorRepository;

        public PrestadorService(IPrestadorRepository prestadorRepositorio)
            : base(prestadorRepositorio)
        {
            _prestadorRepository = prestadorRepositorio;
        }

        public Prestador GetPorCpf(string cpf) 
        {
            return _prestadorRepository.GetPorCpf(cpf);
        }

        public Prestador GetPorGuid(Guid guid)
        {
            return _prestadorRepository.GetPorGuid(guid);
        }

        //retorna o prestador atraves do email
        public Prestador GetPorEmail(string email)
        {
            return _prestadorRepository.GetPorEmail(email);
        }

        //retorna os pretadores que nao estao ligados ao orçamento selecionado
        public IEnumerable<Prestador> GetPrestadores(int orcamentoId)
        {
            return _prestadorRepository.GetPrestadores(orcamentoId);
        }

        public IEnumerable<Prestador> GetPrestadoresComServicos()
        {
            return _prestadorRepository.GetPrestadoresComServicos();
        }
    }
}
