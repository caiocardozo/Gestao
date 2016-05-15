using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
   public  class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
   {
       private readonly GestaoContext _db;
       public ContatoRepository(GestaoContext db)
           :base(db)
       {
           _db = new GestaoContext();
       }
    }
}
