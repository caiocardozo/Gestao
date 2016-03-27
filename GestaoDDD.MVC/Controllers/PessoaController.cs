using System;
using System.Web.Mvc;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace GestaoDDD.MVC.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaAppService _pessoaApp;

        public PessoaController(IPessoaAppService pessoaApp)
        {
            _pessoaApp = pessoaApp;
        }


        // GET: Pessoa
        public ActionResult DadosCadastrais()
        {
            var pessoa = _pessoaApp.RPessoaPorId(User.Identity.GetUserId());
            var pessoaViewModel = Mapper.Map<Pessoa, PessoaViewModel>(pessoa);
            return View(pessoaViewModel);
        }
    }
}