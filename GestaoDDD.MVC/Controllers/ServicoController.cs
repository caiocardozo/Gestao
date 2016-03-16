using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

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

        public ActionResult ServicosCategorias(string cpf, string nome, string celular, string email)
        {
            ViewBag.CategoriaModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_iCategoriaApp.GetAll());
            ViewBag.Cpf = cpf;
            ViewBag.Nome = nome;
            ViewBag.Celular = celular;
            ViewBag.Email = email;

            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());
            return View(servicoViewModel);
        }

        [HttpPost]
        public ActionResult ServicosCategorias(FormCollection collection, string cpfPrestador, string nome, string celular, string email)
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
                //var prestador = _iPrestadorApp.GetPorCpf(cpfPrestador);
                var pts = new Prestador();
                pts.pres_Cpf_Cnpj = cpfPrestador;
                pts.pres_Nome = nome;
                pts.pres_Telefone_Celular = celular;
                pts.pres_Email = email;

                _iServicoPrestadorApp.SalvarServicosPrestador(checkboxes, pts);
                return RedirectToAction("Cadastrar", "Prestador");
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }


        public ActionResult Index(string pesquisa, string Filtro)
        {
            if (pesquisa == null)
            {
                pesquisa = Filtro;
            }

            ViewBag.Filtro = pesquisa;

            
            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());

            if (!string.IsNullOrEmpty(pesquisa))
            {
                servicoViewModel = servicoViewModel.Where(s => s.serv_Nome.ToUpper().Contains(pesquisa.ToUpper()));
            }

            ViewBag.CategoriaModel = Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_iCategoriaApp.GetAll());

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
