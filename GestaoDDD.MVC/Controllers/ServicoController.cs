﻿using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoAppService _servicoApp;

        public ServicoController(IServicoAppService servicoApp)
        {
            _servicoApp = servicoApp;
        }

        //
        // GET: /Servico/
        public ActionResult Index()
        {
            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_servicoApp.GetAll());
            return View(servicoViewModel);
        }

        //
        // GET: /Servico/Details/5
        public ActionResult Details(int id)
        {
            var servicoId = Mapper.Map<Servico, ServicoViewModel>(_servicoApp.GetById(id));
            if(servicoId == null)
                return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            return View(servicoId);
        }

        //
        // GET: /Servico/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Servico/Create
        [HttpPost]
        public ActionResult Create(Servico servico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _servicoApp.SaveOrUpdate(servico);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Servico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Servico/Edit/5
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
    }
}