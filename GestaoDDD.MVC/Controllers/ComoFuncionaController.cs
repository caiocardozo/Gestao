using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class ComoFuncionaController : Controller
    {
        private readonly IComoFuncionaAppService _comoFuncionaAppService;
        public ComoFuncionaController(IComoFuncionaAppService comoFuncionaAppService)
        {
            _comoFuncionaAppService = comoFuncionaAppService;
        }

        //
        // GET: /ComoFunciona/
        public ActionResult Index()
        {
            var comoFuncionaVm = Mapper.Map<IEnumerable<ComoFunciona>, IEnumerable<ComoFuncionaViewModel>>(_comoFuncionaAppService.GetAll());
            return View(comoFuncionaVm);
        }

        //
        // GET: /ComoFunciona/Details/5
        public ActionResult Details(int id)
        {
        
            var comoFunc = _comoFuncionaAppService.GetById(id);
            var comoFuncVm = Mapper.Map<ComoFunciona, ComoFuncionaViewModel>(comoFunc);
            return View(comoFuncVm);
        }

        //
        // GET: /ComoFunciona/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ComoFunciona/Create
        [HttpPost]
        public ActionResult Create(ComoFuncionaViewModel comoFuncVm)
        {
            try
            {
                if (ModelState.IsValid) 
                {

                    var comoFunc = Mapper.Map<ComoFuncionaViewModel, ComoFunciona>(comoFuncVm);
                    _comoFuncionaAppService.Add(comoFunc);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // GET: /ComoFunciona/Edit/5
        public ActionResult Edit(int id)
        {
            var comoFunc = _comoFuncionaAppService.GetById(id);
            var comoFuncVm = Mapper.Map<ComoFunciona, ComoFuncionaViewModel>(comoFunc);
            return View(comoFuncVm);
        }

        //
        // POST: /ComoFunciona/Edit/5
        [HttpPost]
        public ActionResult Edit(ComoFuncionaViewModel comoFuncVm )
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var comoFunc = Mapper.Map<ComoFuncionaViewModel, ComoFunciona>(comoFuncVm);
                    _comoFuncionaAppService.Add(comoFunc);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }

        }

        //
        // GET: /ComoFunciona/Delete/5
        public ActionResult Delete(int id)
        {
        
            var comoFunc = _comoFuncionaAppService.GetById(id);
            var comoFuncVm = Mapper.Map<ComoFunciona, ComoFuncionaViewModel>(comoFunc);
            return View(comoFuncVm);
        }

        //
        // POST: /ComoFunciona/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmaDelete(int id)
        {
            try
            {
                //var adm_grupo = _categoriaApp.GetById(id);
                //_categoriaApp.Remove(adm_grupo);


                var adm_grupo = _comoFuncionaAppService.GetById(id);
                _comoFuncionaAppService.Remove(adm_grupo);
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
