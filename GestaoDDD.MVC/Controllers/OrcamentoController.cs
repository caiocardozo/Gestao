using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class OrcamentoController : Controller
    {
        //
        // GET: /Orcamento/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Orcamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Orcamento/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Orcamento/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Orcamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Orcamento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Orcamento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Orcamento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
