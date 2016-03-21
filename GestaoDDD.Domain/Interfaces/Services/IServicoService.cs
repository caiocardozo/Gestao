using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IServicoService : IServiceBase<Servico>
    {
         //retorna todos os serviços a partir de uma categoria
        List<ServicoIdNome> RetornaServicoPelaCategoria(int categoria);
    }
}
