using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.MVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaAppService _categoriaApp;
        private readonly IServicoAppService _servicoApp;
        private static string msgRetorno = "";

        public CategoriaController(ICategoriaAppService categoriaApp, IServicoAppService servicoApp)
        {
            _categoriaApp = categoriaApp;
            _servicoApp = servicoApp;
        }


        public ActionResult IndexServicos()
        {
            var categoriaViewModel =
                Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_categoriaApp.GetAll());
            return View(categoriaViewModel);
        }

        public ActionResult Index()
        {
            ViewBag.Retorno = msgRetorno;
            var categoriaViewModel =
                Mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaViewModel>>(_categoriaApp.GetAll());
            return View(categoriaViewModel);
        }

        //
        // GET: /Categoria/Details/5

        public ActionResult Detalhes(int id)
        {
            var categoria = _categoriaApp.GetById(id);
            var categoriaViewModel = Mapper.Map<Categoria, CategoriaViewModel>(categoria);
            return View(categoriaViewModel);
        }

        // GET: /Servico/Create
        public ActionResult Cadastrar(FormCollection collection)
        {
            return View();
        }

        //
        // POST: /Servico/Create
        [HttpPost]
        public ActionResult Cadastrar(CategoriaViewModel categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoriaDomain = Mapper.Map<CategoriaViewModel, Categoria>(categoria);
                    _categoriaApp.Add(categoriaDomain);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(categoria);
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
            var categoria = _categoriaApp.GetById(id);
            var categoriaViewModel = Mapper.Map<Categoria, CategoriaViewModel>(categoria);
            return View(categoriaViewModel);
        }

        //
        // POST: /Categoria/Edit/5

        [HttpPost]
        public ActionResult Editar(CategoriaViewModel categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoriadomain = Mapper.Map<CategoriaViewModel, Categoria>(categoria);
                    _categoriaApp.Update(categoriadomain);
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
                return View(categoria);
            }
        }

        //
        // GET: /Categoria/Delete/5

        public ActionResult Deletar(int id)
        {
            try
            {

                var categoria = _categoriaApp.GetById(id);
                var servicoCategoria = _servicoApp.RetornaServicoPelaCategoria(id);
                if (servicoCategoria != null)
                {
                    msgRetorno = "Esta categoria não pode ser excluída, pois existem serviços vinculados a ela.";
                }
                else
                {
                    _categoriaApp.Remove(categoria);
                    msgRetorno = "Categoria excluída com sucesso";
                }
                return RedirectToAction("ListarTodos");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //
        // POST: /Categoria/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var catDelete = _categoriaApp.GetById(id);
            _categoriaApp.Remove(catDelete);

            return RedirectToAction("Index");
        }

        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}

    ;