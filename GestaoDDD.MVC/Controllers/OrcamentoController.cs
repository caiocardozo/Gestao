using System;
using System.Web.Mvc;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.MVC.Controllers
{
    public class OrcamentoController : Controller
    {

        private readonly IOrcamentoAppService _orcamentoApp;

        public OrcamentoController(IOrcamentoAppService orcamentoApp)
        {
            _orcamentoApp = orcamentoApp;
        }

        //
        // GET: /Orcamento/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Orcamento/Details/5
        public ActionResult Detalhes(int id)
        {
            return View();
        }

        //
        // GET: /Orcamento/Cadastrar
        public ActionResult Cadastrar(FormCollection collection)
        {
            return View();
        }

        //
        // POST: /Orcamento/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(OrcamentoViewModel orcamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orcamentoDomain = Mapper.Map<OrcamentoViewModel, Orcamento>(orcamento);
                    _orcamentoApp.Add(orcamentoDomain);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(orcamento);
                }

            }
            catch(Exception)
            {
               return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult Editar(int id)
        {
            var orcamento = _orcamentoApp.GetById(id);
            var orcamentoViewModel = Mapper.Map<Orcamento, OrcamentoViewModel>(orcamento);
            return View(orcamentoViewModel);
        }

        //
        // POST: /\Orcamento/Edit/5

        [HttpPost]
        public ActionResult Editar(OrcamentoViewModel orcamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orcamentodomain = Mapper.Map<OrcamentoViewModel, Orcamento>(orcamento);
                    _orcamentoApp.Update(orcamentodomain);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    //inserir pagina de erro
                    return RedirectToAction("ErroAoCadastrar");
                }
            }
            else
            {
                return View(orcamento);
            }
        }


        public ActionResult Deletar(int id)
        {
            var orcamento = _orcamentoApp.GetById(id);
            var orcamentoViewModel = Mapper.Map<Orcamento, OrcamentoViewModel>(orcamento);
            return View(orcamentoViewModel);
            
        }

        //
        // POST: /Orcamento/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var adm_grupo = _orcamentoApp.GetById(id);
            _orcamentoApp.Remove(adm_grupo);

            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
