using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IAppCategoriaServicos _categoriaApp;
        public CategoriaController(IAppCategoriaServicos categoriaApp)
        {
            _categoriaApp = categoriaApp;
        }

        public ActionResult Index()
        {
            var CategoriaViewModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_categoriaApp.FindAll());
            return View(CategoriaViewModel);
        }

        public ActionResult Especiais()
        {
            var CategoriaViewModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_categoriaApp.ObterCategoriasEspeciais());
            return View(CategoriaViewModel);
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int id)
        {
            var Categoria = _categoriaApp.FindById(id);
            var CategoriaView = Mapper.Map<Categoria, CategoriaViewModel>(Categoria);
            return View(CategoriaView);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaViewModel _Categoria)
        {
            if (ModelState.IsValid)
            {
                var Categoria = Mapper.Map<CategoriaViewModel, Categoria>(_Categoria);
                _categoriaApp.SaveOrUpdate(Categoria);
                return RedirectToAction("Index");
            }
            return View(_Categoria);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int id)
        {
            var Categoria = _categoriaApp.FindById(id);
            var CategoriaView = Mapper.Map<Categoria, CategoriaViewModel>(Categoria);
            return View(CategoriaView);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoriaViewModel _Categoria)
        {
            if (ModelState.IsValid)
            {
                var Categoria = Mapper.Map<CategoriaViewModel, Categoria>(_Categoria);
                _categoriaApp.SaveOrUpdate(Categoria);
                return RedirectToAction("Index");
            }
            return View(_Categoria);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int id)
        {
            var Categoria = _categoriaApp.FindById(id);
            var CategoriaView = Mapper.Map<Categoria, CategoriaViewModel>(Categoria);

            return View(CategoriaView);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmado(int id)
        {
            var Categoria = _categoriaApp.FindById(id);
            _categoriaApp.Delete(Categoria);

            return RedirectToAction("Index");

        }



    }
}
