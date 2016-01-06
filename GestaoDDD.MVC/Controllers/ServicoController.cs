using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoAppService _servicoApp;
        private readonly ICategoriaAppService _categoriaApp;

        public ServicoController(IServicoAppService servicoApp, ICategoriaAppService categoriaApp)
        {
            _servicoApp = servicoApp;
            _categoriaApp = categoriaApp;
        }

        //
        // GET: /Servico/
        public ActionResult Index(FormCollection collection)
        {
            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_servicoApp.GetAll());
            return View(servicoViewModel);
        }

        //
        // GET: /Servico/Details/5
        public ActionResult Detailhes(int id)
        {
            var servicoId = Mapper.Map<Servico, ServicoViewModel>(_servicoApp.GetById(id));
            if (servicoId == null)
                return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            return View(servicoId);
        }

        //
        // GET: /Servico/Create
        public ActionResult Cadastrar(FormCollection collection)
        {
            ViewBag.cat_Id = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
            return View();
        }

        //
        // POST: /Servico/Create
        [HttpPost]
        public ActionResult Cadastrar(ServicoViewModel servico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var servicoDomain = Mapper.Map<ServicoViewModel, Servico>(servico);
                    _servicoApp.Add(servicoDomain);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.cat_Id = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
                    return View(servico);
                }
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // GET: /Servico/Edit/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        //
        // POST: /Servico/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Servico/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Servico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
