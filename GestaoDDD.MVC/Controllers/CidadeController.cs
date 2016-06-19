using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities.NoSql;

namespace GestaoDDD.MVC.Controllers
{
    public class CidadeController : Controller
    {
        private readonly ICidadeAppService _cidadeApp;


        public CidadeController(ICidadeAppService cidadeApp)
        {
            _cidadeApp = cidadeApp;
        }

        //retorna os serviços pela categoria
        public JsonResult RCidadePEstado(string id)
        {
            List<CidadeIdNome> retorno = _cidadeApp.RetornaCidadePeloEstado(Convert.ToInt32(id));
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}