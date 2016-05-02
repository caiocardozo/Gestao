using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;
namespace GestaoDDD.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        public UsuarioService(IUsuarioRepository usuarioRepositorio)
            :base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        //obtem o usuario atraves do email
        public Usuario ObterPorEmail(string email)
        {
            return _usuarioRepositorio.ObterPorEmail(email);
        }
    }
}
