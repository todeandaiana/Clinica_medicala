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
    public class ProgramariController : Controller
    {
        private readonly ClinicaContext _context;

        public ProgramariController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Programari
        public async Task<IActionResult> Index()
        {
            var clinicaContext = _context.Programari.Include(p => p.Pacient).Include(p => p.Serviciu);
            return View(await clinicaContext.ToListAsync());
        }

        // GET: Programari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Programari == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari
                .Include(p => p.Pacient)
                .Include(p => p.Serviciu)
                .FirstOrDefaultAsync(m => m.ProgramareID == id);
            if (programare == null)
            {
                return NotFound();
            }

            return View(programare);
        }

        // GET: Programari/Create
        public IActionResult Create()
        {
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID");
            ViewData["ServiciuID"] = new SelectList(_context.Servicii, "ServiciuID", "ServiciuID");
            return View();
        }

        // POST: Programari/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramareID,PacientID,ServiciuID,DataProgramare")] Programare programare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID", programare.PacientID);
            ViewData["ServiciuID"] = new SelectList(_context.Servicii, "ServiciuID", "ServiciuID", programare.ServiciuID);
            return View(programare);
        }

        // GET: Programari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Programari == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari.FindAsync(id);
            if (programare == null)
            {
                return NotFound();
            }
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID", programare.PacientID);
            ViewData["ServiciuID"] = new SelectList(_context.Servicii, "ServiciuID", "ServiciuID", programare.ServiciuID);
            return View(programare);
        }

        // POST: Programari/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramareID,PacientID,ServiciuID,DataProgramare")] Programare programare)
        {
            if (id != programare.ProgramareID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramareExists(programare.ProgramareID))
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
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID", programare.PacientID);
            ViewData["ServiciuID"] = new SelectList(_context.Servicii, "ServiciuID", "ServiciuID", programare.ServiciuID);
            return View(programare);
        }

        // GET: Programari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Programari == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari
                .Include(p => p.Pacient)
                .Include(p => p.Serviciu)
                .FirstOrDefaultAsync(m => m.ProgramareID == id);
            if (programare == null)
            {
                return NotFound();
            }

            return View(programare);
        }

        // POST: Programari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Programari == null)
            {
                return Problem("Entity set 'ClinicaContext.Programari'  is null.");
            }
            var programare = await _context.Programari.FindAsync(id);
            if (programare != null)
            {
                _context.Programari.Remove(programare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramareExists(int id)
        {
          return (_context.Programari?.Any(e => e.ProgramareID == id)).GetValueOrDefault();
        }
    }
}
