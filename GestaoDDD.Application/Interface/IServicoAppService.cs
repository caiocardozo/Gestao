using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Application.Interface
{
    public interface IServicoAppService : IAppServiceBase<Servico>
    {
         //retorna todos os serviços a partir de uma categoria
        List<ServicoIdNome> RetornaServicoPelaCategoria(int categoria);
    }
}
