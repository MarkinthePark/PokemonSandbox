using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pokemon.DAL;
using Pokemon.Models;

namespace Pokemon.Controllers
{
    public class PokedataController : Controller
    {
        private PokemonContext db = new PokemonContext();

        // GET: Pokedata
        public ActionResult Index()
        {
            return View(db.Pokedatas.ToList());
        }

        // GET: Pokedata/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Moves pokedata = db.Moves.Find(id);
            if (pokedata == null)
            {
                return HttpNotFound();
            }
            Console.WriteLine(pokedata);
            return View(pokedata);
        }

        // GET: Pokedata/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pokedata/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,base_experience,height,is_default,location_area_encounters,name,order,weight")] Pokedata pokedata)
        {
            if (ModelState.IsValid)
            {
                db.Pokedatas.Add(pokedata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pokedata);
        }

        // GET: Pokedata/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pokedata pokedata = db.Pokedatas.Find(id);
            if (pokedata == null)
            {
                return HttpNotFound();
            }
            return View(pokedata);
        }

        // POST: Pokedata/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,base_experience,height,is_default,location_area_encounters,name,order,weight")] Pokedata pokedata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pokedata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pokedata);
        }

        // GET: Pokedata/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pokedata pokedata = db.Pokedatas.Find(id);
            if (pokedata == null)
            {
                return HttpNotFound();
            }
            return View(pokedata);
        }

        // POST: Pokedata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pokedata pokedata = db.Pokedatas.Find(id);
            db.Pokedatas.Remove(pokedata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
