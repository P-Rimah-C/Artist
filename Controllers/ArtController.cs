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
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var art = await context.Arts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (art == null)
            {
                return NotFound();
            }

            return View(art);
        }

        // GET: Art/Create
        public ActionResult Create(Guid? id = null)
        {
            return View();
        }

        // POST: Art/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Guid? cid = null)
        {
            try
            {
                // TODO: Add insert logic here
                string img = collection["Img"];
                string name = collection["Name"];
                string description = collection["Description"];
                var catForProd = context.Categories.Where(item => item.Id == cid).First();
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
                return RedirectToAction(nameof(Index), new {CatId = cid });

            }
            catch /*(Exception Error)*/
            {

                return View(/*Error*/);
            }
        }

        // GET: Art/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var art = await context.Arts.FindAsync(id);
            if (art == null)
            {
                return NotFound();
            }
            return View(art);
        }

        // POST: Art/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Img")] Art art)
        {
            if (id != art.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(art);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtExists(art.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(art);
        }

        // GET: Art/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var art = await context.Arts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (art == null)
            {
                return NotFound();
            }

            return View(art);
        }

        // POST: Art/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var art = await context.Arts.FindAsync(id);
            context.Arts.Remove(art);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtExists(Guid id)
        {
            return context.Arts.Any(e => e.Id == id);
        }
    }
}
