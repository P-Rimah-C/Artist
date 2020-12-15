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
    public class CategoryController : Controller
    {
        private readonly DataContext context;

        public CategoryController(DataContext context)
        {
            this.context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            //string[] lol = { "#DC143C", "#00FF00", "#FF1493", "#FFFF00", "#0000FF", "#8A2BE2" };
            return View(await context.Categories.ToListAsync());
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin, Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string Name = collection["Name"];
                Category newCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = Name,
                };
                context.Categories.Add(newCategory);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch /*(Exception Error)*/
            {
                return View(/*Error*/);
            }


        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(Guid id)
        {
            return View(context.Categories.Where(item => item.Id == id).First());
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string name = collection["Name"];
                var itemToEdit = context.Categories.Where(item => item.Id == id).First();
                itemToEdit.Name = name;
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(Guid id)
        {
            return View(context.Categories.Where(item => item.Id == id).First());
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Category catItemToDelete = context.Categories.Where(item => item.Id == id).First();
                IEnumerable<Art> artsItemToDelete = context.Arts.Where(item => item.Id == catItemToDelete.Id).AsEnumerable();
                if (artsItemToDelete is IEnumerable<Art>)
                {
                    foreach (Art i in artsItemToDelete)
                    {
                        context.Arts.Remove(i);
                        context.SaveChanges();
                    }
                }
                context.Categories.Remove(catItemToDelete);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
