using System;
using System.Collections.Generic;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class PrestadorController : Controller
    {
        private readonly IPrestadorAppService _prestadorApp;
        public PrestadorController(IPrestadorAppService prestadorApp)
        {
            _prestadorApp = prestadorApp;
        }
        //
        // GET: /Prestador/
        [Authorize(Roles = "Admin")]
        // GET: /Prestador/
        public ActionResult Index()
        {
            var prestadorViewModel = Mapper.Map<IEnumerable<Prestador>, IEnumerable<PrestadorViewModel>>(_prestadorApp.GetAll());
            return View(prestadorViewModel);
        }

        //
        // GET: /Prestador/Details/5
        public ActionResult Detalhes(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);
        }

        //
        // GET: /Prestador/Cadastrar
        public ActionResult Cadastrar(FormCollection collection)
        {
            return View();
        }

        //
        // POST: /Prestador/Create
        
        public ActionResult Create()
        {

            return View();

        }
        //
        // POST: /Prestador/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(Prestador prestador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var prestadorDomain = Mapper.Map<PrestadorViewModel, Prestador>(prestador);
                   // _prestadorApp.SaveOrUpdate(prestador);
                    return RedirectToAction("IndexServicosCategorias", "Servico",
                        new { cpf = prestador.pres_Cpf_Cnpj,
                              nome = prestador.pres_Nome,
                              email = prestador.pres_Email,
                              celular = prestador.pres_Telefone_Celular
                        }); 
                }
                else
                {
                    return View(prestador);
                }

            }
            catch (Exception)
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult Editar(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);
        }

        //
        // POST: /\Prestador/Edit/5

        [HttpPost]
        public ActionResult Editar(PrestadorViewModel prestador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var prestadordomain = Mapper.Map<PrestadorViewModel, Prestador>(prestador);
                    _prestadorApp.Update(prestadordomain);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    //inserir pagina de erro
                    return RedirectToAction("ErroAoCadastrar");
                }
            }
            else
            {
                return View(prestador);
            }
        }


        public ActionResult Deletar(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);

        }

        //
        // POST: /Prestador/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var adm_grupo = _prestadorApp.GetById(id);
            _prestadorApp.Remove(adm_grupo);

            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
