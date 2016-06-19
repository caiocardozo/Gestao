using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GestaoDDD.Domain.Entities.NoSql;

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
        public ActionResult ServicosCategorias(FormCollection collection, string cpf,
            string nome, string celular, string email)
        {
            try
            {
                var checkboxes = new List<Servico>();
                foreach (var col in collection)
                {
                    int servId;
                    Int32.TryParse(col.ToString(), out servId);
                    var servico = _iServicoApp.GetById(servId);
                    checkboxes.Add(servico);
                }
                //inserir por email, assim nao tem como duplicar
                var prestador = _iPrestadorApp.GetPorEmail(email);
               _iServicoPrestadorApp.SalvarServicosPrestador(checkboxes, prestador);
                return RedirectToAction("PrestadorCadastroSucesso", "Prestador");
            }
            catch
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }


        public ActionResult Index(string pesquisa_servico, string pesquisa_categoria)
        {
            var servicoViewModel = Mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoViewModel>>(_iServicoApp.GetAll());

            if ((!string.IsNullOrEmpty(pesquisa_servico) && (!string.IsNullOrEmpty(pesquisa_categoria))))
            {
                servicoViewModel = servicoViewModel.Where(s => s.serv_Nome.ToUpper().Contains(pesquisa_servico.ToUpper()) && s.Categoria.cat_Nome.ToUpper().Contains(pesquisa_categoria.ToUpper()));
            }
            else if ((string.IsNullOrEmpty(pesquisa_servico) && (!string.IsNullOrEmpty(pesquisa_categoria))))
            {
                servicoViewModel = servicoViewModel.Where(s => s.Categoria.cat_Nome.ToUpper().Contains(pesquisa_categoria.ToUpper()));
            }
            else if ((!string.IsNullOrEmpty(pesquisa_servico) && (string.IsNullOrEmpty(pesquisa_categoria))))
            {
                servicoViewModel = servicoViewModel.Where(s => s.serv_Nome.ToUpper().Contains(pesquisa_servico.ToUpper()));
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

        //retorna os serviços pela categoria
        public JsonResult RServicosPCategoria(string id)
        {
            List<ServicoIdNome> retorno = _iServicoApp.RetornaServicoPelaCategoria(Convert.ToInt32(id));
            return Json(retorno, JsonRequestBehavior.AllowGet);
            //List<Object> resultado = new List<object>();
            //resultado.Add(new
            //                {
            //                    Nome = "Linha de Código",
            //                    URL = "www.linhadecodigo.com.br"
            //                });
            //resultado.Add(new
            //                {
            //                    Nome = "DevMedia",
            //                    URL = "www.devmedia.com.br"
            //                });
            //resultado.Add(new
            //                {
            //                    Nome = "Mr. Bool",
            //                    URL = "www.mrbool.com.br"
            //                });
            //return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ServicosCadastrados()
        {
            return View();
        }
    }
}
