using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asernatat_3.Data;
using Asernatat_3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Asernatat_3.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext context;

        public OrderController(DataContext context)
        {
            this.context = context;
        }

        // GET: Order
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await context.Orders.ToListAsync());
        }

        // GET: Order/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                decimal prise = Decimal.Parse(collection["Prise"]);
                string name = collection["Name"];
                string time = collection["Time"];
                Order newItem = new Order
                {
                    Prise = prise,
                    Name = name,
                    Time = time,
                    Id = Guid.NewGuid(),
                    Email = User.Identity.Name
                };
                context.Orders.Add(newItem);
                context.SaveChanges();
                return RedirectToAction(nameof(Index), nameof(HomeController));

            }
            catch /*(Exception Error)*/
            {

                return View(/*Error*/);
            }
        }

    }
}
