using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Data;
using Clinica_medicala.Models;

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
        public async Task<IActionResult> Index()
        {
              return _context.Servicii != null ? 
                          View(await _context.Servicii.ToListAsync()) :
                          Problem("Entity set 'ClinicaContext.Servicii'  is null.");
        }

        // GET: Servicii/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicii == null)
            {
                return NotFound();
            }

            var serviciu = await _context.Servicii
                .FirstOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Create([Bind("ID,Titlu,Medic,Pret")] Serviciu serviciu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviciu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titlu,Medic,Pret")] Serviciu serviciu)
        {
            if (id != serviciu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviciu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiciuExists(serviciu.ID))
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
            return View(serviciu);
        }

        // GET: Servicii/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servicii == null)
            {
                return NotFound();
            }

            var serviciu = await _context.Servicii
                .FirstOrDefaultAsync(m => m.ID == id);
            if (serviciu == null)
            {
                return NotFound();
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
            var serviciu = await _context.Servicii.FindAsync(id);
            if (serviciu != null)
            {
                _context.Servicii.Remove(serviciu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiciuExists(int id)
        {
          return (_context.Servicii?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
