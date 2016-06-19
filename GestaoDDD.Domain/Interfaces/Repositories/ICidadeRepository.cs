using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository : IRepositoryBase<Cidade>
    {
        //retorna todos as cidades a partir de um estado
        List<CidadeIdNome> RetornaCidadePeloEstado(int estado);
    }
}
