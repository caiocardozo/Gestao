using AutoMapper;
using GestaoDDD.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestaoDDD.Application.Services;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.MVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoAppService _iContatoAppService;

        public ContatoController(IContatoAppService iContatoAppService)
        {
            _iContatoAppService = iContatoAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Contato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contato/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ContatoSucesso()
        {
            return View();
        }

        //
        // POST: /Contato/Create
        [HttpPost]
        public ActionResult Create(ContatoViewModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contatoVm = Mapper.Map<ContatoViewModel, Contato>(contato);
                    _iContatoAppService.SaveOrUpdate((contatoVm));
                    return RedirectToAction("ContatoSucesso");
                }
                else
                    return View(contato);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contato/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contato/Edit/5
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
        // GET: /Contato/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contato/Delete/5
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
