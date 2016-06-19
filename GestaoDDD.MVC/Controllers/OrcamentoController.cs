using System;
using System.Collections;
using System.Web.Mvc;
using AutoMapper;
using GestaoDDD.Application.Interface;
using GestaoDDD.Application.ViewModels;
using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using EnumClass = GestaoDDD.Domain.Entities.NoSql.EnumClasses;

namespace GestaoDDD.MVC.Controllers
{
    public class OrcamentoController : Controller
    {

        private readonly IOrcamentoAppService _orcamentoApp;
        private readonly ICategoriaAppService _categoriaApp;
        private readonly IServicoAppService _servicoApp;
        private readonly IPrestadorAppService _prestadorApp;
        private readonly ICidadeAppService _cidadeApp;
        
        public OrcamentoController(IOrcamentoAppService orcamentoApp, ICategoriaAppService categoriaApp,
            IServicoAppService servicoApp, IPrestadorAppService prestadorApp, ICidadeAppService cidadeApp)
        {
            _orcamentoApp = orcamentoApp;
            _categoriaApp = categoriaApp;
            _servicoApp = servicoApp;
            _prestadorApp = prestadorApp;
            _cidadeApp = cidadeApp;
        }

        //
        // GET: /Orcamento/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrcamentoEnviadoSucesso() 
        {
            return View();
        }

        //
        // GET: /Orcamento/Details/5
        public ActionResult Detalhes(int id)
        {
            var orcamentoEntity = Mapper.Map<Orcamento, OrcamentoViewModel>(_orcamentoApp.GetById(2));
            ViewBag.Servico = _servicoApp.GetById(orcamentoEntity.serv_Id);
            return View(orcamentoEntity);
        }

        //
        // GET: /Orcamento/Cadastrar
        public ActionResult Cadastrar()
        {
            ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
            return View();
        }

        // POST: /Orcamento/Cadastrar
        [HttpPost]
        public ActionResult Cadastrar(OrcamentoViewModel orcamento, int servico_id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orcamentoEntity = Mapper.Map<OrcamentoViewModel, Orcamento>(orcamento);


                    var endereco = orcamento.orc_Endereco;
                    var x = endereco.Split(',');
                    var y = x[1].Split('-');
                    orcamentoEntity.orc_cidade = y[0];
                    orcamentoEntity.orc_estado = (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), y[1]);
                    

                    orcamentoEntity.serv_Id = servico_id;
                    _orcamentoApp.Add(orcamentoEntity);

                    return RedirectToAction("OrcamentoEnviadoSucesso");
                }
                else
                {
                    ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
                    return View(orcamento);
                }

            }
            catch (Exception)
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
                    var endereco = orcamentodomain.orc_endereco;
                    var x = endereco.Split(',');
                    var y = x[1].Split('-');
                    orcamento.orc_cidade = y[0].Trim();
                    orcamento.orc_estado = (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), y[1]);
            
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

        public ActionResult BuscaTrabalhosIndex()
        {
            return View();
        }

        public ActionResult BuscaTrabalhos(string usuarioId)
        {
            var prestador = _prestadorApp.GetPorGuid(usuarioId);
            ViewBag.Nome = prestador.pres_Nome;
            ViewBag.CaminhoFoto = prestador.caminho_foto;


            ViewBag.Cidades = new SelectList(_cidadeApp.GetAll(), "Id", "NomeCidade");
            ViewBag.ListaCat = new SelectList(_categoriaApp.GetAll(), "cat_Id", "cat_Nome");
            var orcamentoVm = Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(_orcamentoApp.GetAll());
            return View(orcamentoVm);
        }

       public PartialViewResult BuscaTrabalhosPartial( string servico, string cidade)
        {
           // var orcamentoVm = Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(_orcamentoApp.GetAll());
           var retorno = _orcamentoApp.RetornaOrcamentos(Convert.ToInt32(servico), cidade);
           return PartialView(Mapper.Map<IEnumerable<Orcamento>, IEnumerable<OrcamentoViewModel>>(retorno));
        }
    }

}
