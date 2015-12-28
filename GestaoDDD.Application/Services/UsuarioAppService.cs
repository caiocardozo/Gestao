using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
using System;

namespace GestaoDDD.Application.Services
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
            :base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public void DesativarLock(string id)
        {
            int idInt;
            Int32.TryParse(id, out idInt);

            var usuario = _usuarioService.GetById(idInt);
            usuario.LockoutEnabled= false;
            _usuarioService.SaveOrUpdate(usuario);
        }
    }
}
