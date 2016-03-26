using AutoMapper;
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
    public class IndiqueProfissionalController : Controller
    {
        private readonly IIndiqueProfissionalAppService _iIndiqueAppService;
        private readonly IServicoAppService _iServicoApp;
        public IndiqueProfissionalController(IIndiqueProfissionalAppService iIdProfAppService, IServicoAppService iServicoApp)
        {
            _iIndiqueAppService = iIdProfAppService;
            _iServicoApp = iServicoApp;
                
        }
        public ActionResult Index()
        {
            var indiqueVm = Mapper.Map<IEnumerable<IndiqueProfissional>, IEnumerable<IndiqueProfissionalViewModel>>(_iIndiqueAppService.GetAll());
            return View(indiqueVm);
        }

        //
        // GET: /IndiqueProfissional/Details/5
        public ActionResult Details(int id)
        {
            var indique = _iIndiqueAppService.GetById(id);
            var indiqueVm = Mapper.Map<IndiqueProfissional, IndiqueProfissionalViewModel>(indique);
            return View(indiqueVm);
        }

        //
        // GET: /IndiqueProfissional/Create
        public ActionResult Create()
        {
            ViewBag.ListaServico = _iServicoApp.GetAll();
            return View();
        }

        //
        // POST: /IndiqueProfissional/Create
        [HttpPost]
        public ActionResult Create(IndiqueProfissionalViewModel indiqueProf, string servico_id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var servico = _iServicoApp.GetById(int.Parse(servico_id));
                    indiqueProf.Servico = servico;

                    var indiqueDomain = Mapper.Map<IndiqueProfissionalViewModel, IndiqueProfissional>(indiqueProf);
                    _iIndiqueAppService.Add(indiqueDomain);
                    return RedirectToAction("IndicacaoSucesso");
                }
                else 
                {
                    ViewBag.ListaServico = _iServicoApp.GetAll();
                    return View(indiqueProf);
                }
                
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }
        public ActionResult IndicacaoSucesso() 
        {
            return View();
        }

        //
        // GET: /IndiqueProfissional/Edit/5
        public ActionResult Edit(int id)
        {
            var indique = _iIndiqueAppService.GetById(id);
            var indiqueVm = Mapper.Map<IndiqueProfissional, IndiqueProfissionalViewModel>(indique);
            return View(indiqueVm);
        }

        //
        // POST: /IndiqueProfissional/Edit/5
        [HttpPost]
        public ActionResult Edit(IndiqueProfissionalViewModel indiqueProf)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var indiqueDomain = Mapper.Map<IndiqueProfissionalViewModel, IndiqueProfissional>(indiqueProf);
                    _iIndiqueAppService.Add(indiqueDomain);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(indiqueProf);
                }

            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // GET: /IndiqueProfissional/Delete/5
        public ActionResult Delete(int id)
        {
            var indique = _iIndiqueAppService.GetById(id);
            var indiqueVm = Mapper.Map<IndiqueProfissional, IndiqueProfissionalViewModel>(indique);
            return View(indiqueVm);
        }

        //
        // POST: /IndiqueProfissional/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDelete(int id)
        {
            try
            {
                var indiqueDel = _iIndiqueAppService.GetById(id);
                _iIndiqueAppService.Remove(indiqueDel);

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
