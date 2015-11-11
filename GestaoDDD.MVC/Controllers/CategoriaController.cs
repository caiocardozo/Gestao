using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.MVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaAppService _categoriaApp;

        public CategoriaController(ICategoriaAppService categoriaApp)
        {
            _categoriaApp = categoriaApp;
        }


        public ActionResult Index()
        {
            //retorna todas as caategorias
            var categoriaViewModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_categoriaApp.GetAll());
            return View(categoriaViewModel);
        }

        //
        // GET: /Categoria/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Categoria/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categoria/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Categoria/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Categoria/Edit/5

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
        // GET: /Categoria/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Categoria/Delete/5

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
