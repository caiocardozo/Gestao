using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using AutoMapper;
using GeoCoordinatePortable;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System.Web.Mvc;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.MVC.Controllers
{
    public class PrestadorController : Controller
    {
        private readonly IPrestadorAppService _prestadorApp;
        private readonly IOrcamentoService _orcamentoApp;
        public PrestadorController(IPrestadorAppService prestadorApp, IOrcamentoService orcamentoApp)
        {
            _prestadorApp = prestadorApp;
            _orcamentoApp = orcamentoApp;
        }
        //
        // GET: /Prestador/
        [Authorize(Roles = "Admin")]
        // GET: /Prestador/
        public ActionResult Index()
        {
            var prestadorViewModel = Mapper.Map<IEnumerable<Prestador>, IEnumerable<PrestadorViewModel>>(_prestadorApp.GetAll());
            return View(prestadorViewModel);
        }

        //
        // GET: /Prestador/Details/5
        public ActionResult Detalhes(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);
        }

        //
        // GET: /Prestador/Cadastrar
        public ActionResult Cadastrar(FormCollection collection)
        {
            return View();
        }

        //
        // POST: /Prestador/Create

        public ActionResult Create()
        {

            return View();

        }

        public ActionResult ExibirOrcamentos()
        {
            //Essa view ta aqui so por questao de teste... Ela nao existe no sistema, era so pra chamar ela e debugar esse processo aqui.
            var list = Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(_orcamentoApp.GetAll());
            var lstCoord = new List<double>();

            foreach (var l in list)
            {
              
                //Coordenada de cada orçamento
                var coordenada_orcamento = new GeoCoordinate();
                coordenada_orcamento.Latitude = double.Parse(l.orc_longitude);
                coordenada_orcamento.Longitude = double.Parse(l.orc_longitude);

                //Coordenada fixa de cada prestador... Deve-se passar o id do prestador logado.
                var coordenada_prestador = new GeoCoordinate();
                coordenada_prestador.Latitude = double.Parse("-16.2744242");
                coordenada_prestador.Longitude = double.Parse("-48.95429619999999");
                // Coordenada fixa do parque dos pirineus.

                var distanceKm = GetDistanceTo(coordenada_orcamento, coordenada_prestador);
            }

            ViewBag.Coordenadas = lstCoord;

            return View();
        }

        public double GetDistanceTo(GeoCoordinate orcamento, GeoCoordinate prestador)
        {
            double distanceKm = 0.0;
            if (double.IsNaN(orcamento.Latitude) || double.IsNaN(orcamento.Longitude) || double.IsNaN(prestador.Latitude) || double.IsNaN(prestador.Longitude))
            {
                double latitude = orcamento.Latitude * 0.0174532925199433;
                double longitude = orcamento.Longitude * 0.0174532925199433;
                double num = prestador.Latitude * 0.0174532925199433;
                double longitude1 = prestador.Longitude * 0.0174532925199433;
                double num1 = longitude1 - longitude;
                double num2 = num - latitude;
                double num3 = Math.Pow(Math.Sin(num2 / 2), 2) + Math.Cos(latitude) * Math.Cos(num) * Math.Pow(Math.Sin(num1 / 2), 2);
                double num4 = 2 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1 - num3));
                double num5 = 6376500 * num4;
                distanceKm = num5 * 0.001;
                return distanceKm;
            }
            return distanceKm = 0.0;
        }

        //
        // POST: /Prestador/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(Prestador prestador)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("IndexServicosCategorias", "Servico",
                        new
                        {
                            cpf = prestador.pres_Cpf_Cnpj,
                            nome = prestador.pres_Nome,
                            email = prestador.pres_Email,
                            celular = prestador.pres_Telefone_Celular
                        });
                }
                else
                {
                    return View(prestador);
                }

            }
            catch (Exception)
            {
                return RedirectToAction("ErroAoCadastrar");
            }
        }

        public ActionResult Editar(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);
        }

        //
        // POST: /\Prestador/Edit/5

        [HttpPost]
        public ActionResult Editar(PrestadorViewModel prestador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var prestadordomain = Mapper.Map<PrestadorViewModel, Prestador>(prestador);
                    _prestadorApp.Update(prestadordomain);
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
                return View(prestador);
            }
        }


        public ActionResult Deletar(int id)
        {
            var prestador = _prestadorApp.GetById(id);
            var prestadorViewModel = Mapper.Map<Prestador, PrestadorViewModel>(prestador);
            return View(prestadorViewModel);

        }

        //
        // POST: /Prestador/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var adm_grupo = _prestadorApp.GetById(id);
            _prestadorApp.Remove(adm_grupo);

            return RedirectToAction("Index");
        }


        public ActionResult ErroAoCadastrar()
        {
            return View();
        }
    }
}
