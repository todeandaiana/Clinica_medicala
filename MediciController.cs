using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Data;
using Clinica_medicala.Models;

namespace Clinica_medicala
{
    public class MediciController : Controller
    {
        private readonly ClinicaContext _context;

        public MediciController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Medici
        public async Task<IActionResult> Index()
        {
              return _context.Medici != null ? 
                          View(await _context.Medici.ToListAsync()) :
                          Problem("Entity set 'ClinicaContext.Medici'  is null.");
        }

        // GET: Medici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medici == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .FirstOrDefaultAsync(m => m.MedicID == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // GET: Medici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicID,Nume")] Medic medic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medic);
        }

        // GET: Medici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medici == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici.FindAsync(id);
            if (medic == null)
            {
                return NotFound();
            }
            return View(medic);
        }

        // POST: Medici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicID,Nume")] Medic medic)
        {
            if (id != medic.MedicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicExists(medic.MedicID))
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
            return View(medic);
        }

        // GET: Medici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medici == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .FirstOrDefaultAsync(m => m.MedicID == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // POST: Medici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medici == null)
            {
                return Problem("Entity set 'ClinicaContext.Medici'  is null.");
            }
            var medic = await _context.Medici.FindAsync(id);
            if (medic != null)
            {
                _context.Medici.Remove(medic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicExists(int id)
        {
          return (_context.Medici?.Any(e => e.MedicID == id)).GetValueOrDefault();
        }
    }
}
