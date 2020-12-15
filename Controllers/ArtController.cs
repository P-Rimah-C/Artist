using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asernatat_3.Data;
using Asernatat_3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Asernatat_3.Controllers
{
    public class ArtController : Controller
    {
        private readonly DataContext context;

        public ArtController(DataContext context)
        {
            this.context = context;
        }

        // GET: Art
        public ActionResult Index(Guid? id)
        {
            ViewData["CatId"] = id;
            return View(context.Arts.Where(item => item.Category.Id == id).ToList());
        }

        // GET: Art/Details/5
        public ActionResult Details(Guid id, Guid cid)
        {
            ViewData["CatId"] = cid;
            return View(context.Arts.Where(item => item.Id == id).First());
        }

        // GET: Art/Create
        public ActionResult Create(Guid? id)
        {
            ViewData["CatId"] = id;
            return View();
        }

        // POST: Art/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Guid? id = null)
        {
            try
            {
                // TODO: Add insert logic here
                string img = collection["Img"];
                string name = collection["Name"];
                string description = collection["Description"];
                var catForProd = context.Categories.Where(item => item.Id == id).First();
                Art newItem = new Art
                {
                    Img = img,
                    Name = name,
                    Description = description,
                    Id = Guid.NewGuid(),
                    Category = catForProd
                };
                context.Arts.Add(newItem);
                context.SaveChanges();
                return RedirectToAction(nameof(Index), new {id = id });

            }
            catch (Exception Error)
            {

                return View(Error);
            }
        }

        // GET: Art/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(Guid id, Guid cid)
        {
            ViewData["CatId"] = cid;
            return View(context.Arts.Where(item => item.Id == id).First());
        }

        // POST: Art/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(Guid id, Guid cid, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string img = collection["Img"];
                string name = collection["Name"];
                string description = collection["Description"];
                var itemToEdit = context.Arts.Where(item => item.Id == id).First();
                itemToEdit.Name = name;
                itemToEdit.Description = description;
                itemToEdit.Img = img;
                context.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = cid });
            }
            catch
            {
                return View();
            }
        }

        // GET: Art/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(Guid id, Guid cid)
        {
            ViewData["CatId"] = cid;
            return View(context.Arts.Where(item => item.Id == id).First());
        }

        // POST: Art/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(Guid id, Guid cid, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Art itemToDelete = context.Arts.Where(item => item.Id == id).First();
                context.Arts.Remove(itemToDelete);
                context.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = cid });
            }
            catch
            {
                return View();
            }
        }
    }
}
