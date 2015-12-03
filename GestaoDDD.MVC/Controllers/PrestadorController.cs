using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class PrestadorController : Controller
    {
        //
        // GET: /Prestador/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Prestador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Prestador/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Prestador/Create
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
        // GET: /Prestador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Prestador/Edit/5
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
        // GET: /Prestador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Prestador/Delete/5
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
