using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoDDD.MVC.Controllers
{
    public class IndiqueProfissionalController : Controller
    {
        //
        // GET: /IndiqueProfissional/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /IndiqueProfissional/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /IndiqueProfissional/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /IndiqueProfissional/Create
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
        // GET: /IndiqueProfissional/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /IndiqueProfissional/Edit/5
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
        // GET: /IndiqueProfissional/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /IndiqueProfissional/Delete/5
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
