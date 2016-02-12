using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            var prestadorViewModel = Mapper.Map<IEnumerable<Prestador>, IEnumerable<PrestadorViewModel>>(_prestadorApp.GetAll());
            return View(prestadorViewModel);
        }
        
        //
        // GET: /Prestador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Prestador/Create
        public ActionResult Create()
        {
            return View();
        }

        
        //
        // POST: /Prestador/Create
        [HttpPost]
        public ActionResult Create(Prestador prestador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  _prestadorApp.SaveOrUpdate(prestador);
                  return RedirectToAction("IndexServicosCategorias", "Servico", new { cpf = prestador.pres_Cpf_Cnpj });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Prestador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Prestador/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
        // GET: /Prestador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Prestador/Delete/5
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
    }
}
