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
    public class PacientiController : Controller
    {
        private readonly ClinicaContext _context;

        public PacientiController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Pacienti
        public async Task<IActionResult> Index()
        {
              return _context.Pacienti != null ? 
                          View(await _context.Pacienti.ToListAsync()) :
                          Problem("Entity set 'ClinicaContext.Pacienti'  is null.");
        }

        // GET: Pacienti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacienti == null)
            {
                return NotFound();
            }

            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.PacientID == id);
            if (pacient == null)
            {
                return NotFound();
            }

            return View(pacient);
        }

        // GET: Pacienti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacienti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacientID,Nume,Adresa,DataNasterii")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacient);
        }

        // GET: Pacienti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacienti == null)
            {
                return NotFound();
            }

            var pacient = await _context.Pacienti.FindAsync(id);
            if (pacient == null)
            {
                return NotFound();
            }
            return View(pacient);
        }

        // POST: Pacienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacientID,Nume,Adresa,DataNasterii")] Pacient pacient)
        {
            if (id != pacient.PacientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientExists(pacient.PacientID))
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
            return View(pacient);
        }

        // GET: Pacienti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacienti == null)
            {
                return NotFound();
            }

            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.PacientID == id);
            if (pacient == null)
            {
                return NotFound();
            }

            return View(pacient);
        }

        // POST: Pacienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacienti == null)
            {
                return Problem("Entity set 'ClinicaContext.Pacienti'  is null.");
            }
            var pacient = await _context.Pacienti.FindAsync(id);
            if (pacient != null)
            {
                _context.Pacienti.Remove(pacient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientExists(int id)
        {
          return (_context.Pacienti?.Any(e => e.PacientID == id)).GetValueOrDefault();
        }
    }
}
