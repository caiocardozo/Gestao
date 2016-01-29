using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoAppService _iServicoApp;
        private readonly ICategoriaAppService _iCategoriaApp;
        private readonly IPrestadorAppService _iPrestadorApp;
        private readonly IServicoPrestadorAppService _iServicoPrestadorApp;
        public ServicoController(IServicoAppService iServicoApp, ICategoriaAppService iCategoriaApp,
IPrestadorAppService iPrestadorApp, IServicoPrestadorAppService iServicoPrestadorApp)
        {
            _iServicoApp = iServicoApp;
            _iCategoriaApp = iCategoriaApp;
            _iPrestadorApp = iPrestadorApp;
            _iServicoPrestadorApp = iServicoPrestadorApp;
        }

        //
        // GET: /Servico/

        public ActionResult IndexServicosCategorias(string cpf)
        {
            ViewBag.CategoriaModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_iCategoriaApp.GetAll());
            ViewBag.Cpf = cpf;

            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());
            return View(servicoViewModel);
        }

        [HttpPost]
        public ActionResult IndexServicosCategorias(FormCollection collection, string cpfPrestador)
        {
            try
            {
                List<Servico> checkboxes = new List<Servico>();
                foreach (var col in collection)
                {

                    if (col.ToString() != "cpfPrestador")
                    {
                        int servId;
                        Int32.TryParse(col.ToString(), out servId);
                        var servico = _iServicoApp.GetById(servId);
                        checkboxes.Add(servico);
                    }
                }
                var prestador = _iPrestadorApp.GetPorCpf(cpfPrestador);

                _iServicoPrestadorApp.SalvarServicosPrestador(checkboxes, prestador);
                return RedirectToAction("Create", "Prestador");
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }


        public ActionResult Index(FormCollection collection)
        {
            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());
            return View(servicoViewModel);
        }

        //
        public ActionResult Detalhes(int id)
        {
            var servico = _iServicoApp.GetById(id);
            var servicoViewModel = Mapper.Map<Servico, ServicoViewModel>(servico);
            return View(servicoViewModel);
        }

        //
        // GET: /Servico/Create
        public ActionResult Cadastrar(FormCollection collection)
        {
            ViewBag.cat_Id = new SelectList(_iCategoriaApp.GetAll(), "cat_Id", "cat_Nome");
            return View();
        }

        //
        // POST: /Servico/Create
        [HttpPost]
        public ActionResult Cadastrar(ServicoViewModel servico)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var servicoDomain = Mapper.Map<ServicoViewModel, Servico>(servico);
                    _iServicoApp.Add(servicoDomain);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.cat_Id = new SelectList(_iCategoriaApp.GetAll(), "cat_Id", "cat_Nome");
                    return View(servico);
                }
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        //
        // GET: /Categoria/Edit/5

        public ActionResult Editar(int id)
        {
            var servico = _iServicoApp.GetById(id);
            var servicoViewModel = Mapper.Map<Servico, ServicoViewModel>(servico);
            ViewBag.cat_Id = new SelectList(_iCategoriaApp.GetAll(), "cat_Id", "cat_Nome", servico.cat_Id);
            return View(servicoViewModel);
        }

        //
        // POST: /Categoria/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ServicoViewModel servico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var servicoViewModel = Mapper.Map<ServicoViewModel, Servico>(servico);
                    _iServicoApp.Update(servicoViewModel);
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
                return View(servico);
            }
        }


        public ActionResult Deletar(int id)
        {
            var servico = _iServicoApp.GetById(id);
            var servicoViewModel = Mapper.Map<Servico, ServicoViewModel>(servico);
            return View(servicoViewModel);
            //if (categoriaId == null)
            //    return HttpNotFound("Não Foi Encontrado Nenhum Registro. Favor verifique, ou entre em contato com o Administrador.");
            //return View(categoriaId);
        }

        //
        // POST: /Categoria/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var servico = _iServicoApp.GetById(id);
            _iServicoApp.Remove(servico);

            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
