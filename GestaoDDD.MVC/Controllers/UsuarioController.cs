using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        //private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioAppService _usuarioAppService;
        public UsuarioController(IUsuarioAppService usuarioRepository)
        {
            _usuarioAppService = usuarioRepository;
        }

        //
        // GET: /Usuario/
        public ActionResult Index()
        {
            var usuarioViewModel = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioAppService.GetAll());
            return View(usuarioViewModel);
        }

        //
        // GET: /Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuarioId = Mapper.Map<Usuario, UsuarioViewModel>(_usuarioAppService.GetById(id));
            if(usuarioId == null)
                return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            return View(usuarioId);
        }

        public ActionResult DesativarLock(string id)
        {
            _usuarioAppService.DesativarLock(id);
            return RedirectToAction("Index");
            //_usuarioRepository.DesativarLock(id);
            //return RedirectToAction("Index");
        }

        //
        // GET: /Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Usuario/Create
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
        // GET: /Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Edit/5
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
        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Usuario/Delete/5
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
