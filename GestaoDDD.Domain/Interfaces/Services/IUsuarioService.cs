using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {

       //obtem o usuario atraves do email
        Usuario ObterPorEmail(string email);
    }
}
