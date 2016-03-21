using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class ServicoService : ServiceBase<Servico>, IServicoService
    {
        private readonly IServicoRepository _servicoRepositorio;

        public ServicoService(IServicoRepository servicoRepositorio)
            :base(servicoRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
        }

         //retorna todos os serviços a partir de uma categoria
        public List<ServicoIdNome> RetornaServicoPelaCategoria(int categoria)
        {
            return _servicoRepositorio.RetornaServicoPelaCategoria(categoria);
        }
    }
}
