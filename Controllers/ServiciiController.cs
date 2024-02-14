using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Data;
using Clinica_medicala.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Clinica_medicala.Controllers
{
    public class ServiciiController : Controller
    {
        private readonly ClinicaContext _context;

        public ServiciiController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Servicii
            public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
            {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
                ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;

                var servicii = from b in _context.Servicii select b;

                if (!String.IsNullOrEmpty(searchString))
                {
                servicii = servicii.Where(s => s.Titlu.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "title_desc":
                    servicii = servicii.OrderByDescending(b => b.Titlu);
                        break;
                    case "Price":
                    servicii = servicii.OrderBy(b => b.Pret);
                        break;
                    case "price_desc":
                    servicii = servicii.OrderByDescending(b => b.Pret);
                        break;
                    default:
                    servicii = servicii.OrderBy(b => b.Titlu);
                        break;
            }

                int pageSize = 2;
            //return View(await servicii.AsNoTracking().ToListAsync());

            return View(await PaginatedList<Serviciu>.CreateAsync(servicii.AsNoTracking(), pageNumber ?? 1, pageSize));

                //  return View(await books.AsNoTracking().Include(a => a.Author).ToListAsync());
            }

        // GET: Servicii/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicii == null)
            {
                return NotFound();
            }

            var serviciu = await _context.Servicii
                 .Include(s => s.Programari)
                 .ThenInclude(e => e.Pacient)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.ServiciuID== id);


            if (serviciu == null)
            {
                return NotFound();
            }

            return View(serviciu);
        }

        // GET: Servicii/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicii/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titlu,Medic,Pret")] Serviciu serviciu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(serviciu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            return View(serviciu);
        }


    // GET: Servicii/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servicii == null)
            {
                return NotFound();
            }

            var serviciu = await _context.Servicii.FindAsync(id);
            if (serviciu == null)
            {
                return NotFound();
            }
            return View(serviciu);
        }

        // POST: Servicii/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviciuToUpdate = await _context.Servicii.FirstOrDefaultAsync(s => s.ServiciuID == id);

            if (await TryUpdateModelAsync<Serviciu>(serviciuToUpdate, "", s => s.Titlu, s => s.Medic, s => s.Pret))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
          //  ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "FullName", serviciuToUpdate.AuthorID);

            return View(serviciuToUpdate);
        }

        // GET: Servicii/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Servicii == null)
            {
                return NotFound();
            }

            var serviciu = await _context.Servicii
                //.Include(b => b.Medic)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ServiciuID == id);
            if (serviciu == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(serviciu);
        }

        // POST: Servicii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicii == null)
            {
                return Problem("Entity set 'ClinicaContext.Servicii'  is null.");
            }
            var book = await _context.Servicii.FindAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Servicii.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }


        }

        private bool ServiciuExists(int id)
        {
          return (_context.Servicii?.Any(e => e.ServiciuID == id)).GetValueOrDefault();
        }
    }
}
