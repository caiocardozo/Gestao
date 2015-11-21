using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using System;
using System.Web.Mvc;
using System.Linq;
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
            var categoriaId = Mapper.Map<Categoria, CategoriaViewModel>(_categoriaApp.GetById(id)); 
            if(categoriaId == null)
                return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            return View(categoriaId);
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
        public ActionResult Create(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoriaApp.SaveOrUpdate(categoria);
                }
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
            var categoriaEdit = Mapper.Map<Categoria, CategoriaViewModel>(_categoriaApp.GetById(id));
            if (categoriaEdit == null)
                return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            return View(categoriaEdit);
        }

        //
        // POST: /Categoria/Edit/5

        [HttpPost]
        public ActionResult Edit(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    _categoriaApp.SaveOrUpdate(categoria);
                }
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
            var categoriaId = Mapper.Map<Categoria, CategoriaViewModel>(_categoriaApp.GetById(id));
            if(categoriaId == null)
                return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            return View(categoriaId);
        }

        //
        // POST: /Categoria/Delete/5

        [HttpPost]
        [HttpPost, ActionName("Excluir")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoriaDel = _categoriaApp.GetById(id);
                _categoriaApp.Remove(categoriaDel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

