using MoviesDBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesDBManager.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Movies(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Movies.HasChanged)
            {
                return PartialView(DB.Movies.ToList().OrderBy(c => c.Name));
            }
            return null;
        }
        public ActionResult MovieForm(Movie movie)
        {
            return PartialView(movie);
        }

        public ActionResult Create()
        {
            ViewBag.Castings = null;
            ViewBag.Actors = SelectListUtilities<Actor>.Convert(DB.Actors.ToList());
            return View(new Movie());
        }

        [HttpPost]
        public ActionResult Create(Movie movie, List<int> SelectedItems)
        {
            if (ModelState.IsValid)
            {
                DB.Movies.Add(movie, SelectedItems);
                return RedirectToAction("Index");
            }
            ViewBag.Castings = SelectListUtilities<Actor>.Convert(movie.Actors);
            ViewBag.Actors = SelectListUtilities<Actor>.Convert(DB.Actors.ToList());
            return View();
        }
        public ActionResult Details(int id)
        {
            Movie movie = DB.Movies.Get(id);
            if (movie != null)
            {
                return View(movie);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Movie movie = DB.Movies.Get(id);
            if (movie != null)
            {
                ViewBag.Castings = SelectListUtilities<Actor>.Convert(movie.Actors);
                ViewBag.Actors = SelectListUtilities<Actor>.Convert(DB.Actors.ToList());
                return View(movie);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Movie movie, List<int> SelectedItems)
        {
            if (ModelState.IsValid)
            {
                DB.Movies.Update(movie, SelectedItems);
                return RedirectToAction("Index");
            }
            ViewBag.Castings = SelectListUtilities<Actor>.Convert(movie.Actors);
            ViewBag.Actors = SelectListUtilities<Actor>.Convert(DB.Actors.ToList());
            return View();
        }
        public ActionResult Delete(int id)
        {
            DB.Movies.Delete(id);
            return RedirectToAction("Index");
        }
    }
}