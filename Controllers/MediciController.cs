using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Data;
using Clinica_medicala.Models;
using System.Security.Policy;
using Clinica_medicala.Models;
using Clinica_medicala.Models.ClinicaViewModels;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Clinica_medicala.Controllers
{
    public class MediciController : Controller
    {
        private readonly ClinicaContext _context;

        public MediciController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Medici
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id, int? serviciuID)
        {
            var viewModel = new MedicIndexData();
            viewModel.Medici = await _context.Medici
            .Include(p => p.ServiciiPrestate)
            .ThenInclude(pb => pb.Serviciu).ThenInclude(b => b.Specializare)
            .Include(p => p.ServiciiPrestate)
                    .ThenInclude(pb => pb.Serviciu)
                    .ThenInclude(i => i.Programari)
                    .ThenInclude(i => i.Pacient)
            .AsNoTracking()
            .OrderBy(p => p.Nume)
            .ToListAsync();
            if (id != null)
            {
                ViewData["MedicID"] = id.Value;
                Medic medic = viewModel.Medici.Where(i => i.MedicID == id.Value).Single();
                viewModel.Servicii = medic.ServiciiPrestate.Select(s => s.Serviciu);
            }
            if (serviciuID != null)
            {
                ViewData["ServiciuID"] = serviciuID.Value;
                viewModel.Programari = viewModel.Servicii.Where(x => x.ServiciuID == serviciuID).Single().Programari;

            }

            /* if (CustomerID != null) {
                 ViewData["CustomerID"] = CustomerID.Value;
                  viewModel.Orders = viewModel.Customers.Where(x => x.CustomerID == CustomerID).Single().Orders;

             } */
            return View(viewModel);

        }


        // GET: Medici/Details/5
        [AllowAnonymous]
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
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Angajat, Manager")]
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
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medici == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
            .Include(i => i.ServiciiPrestate).ThenInclude(i => i.Serviciu)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MedicID == id);


            if (medic == null)
            {
                return NotFound();
            }

            PopulateServiciuPrestatData(medic);
            return View(medic);
        }
        private void PopulateServiciuPrestatData(Medic medic)
        {
            var allServicii = _context.Servicii;
            var medicServicii = new HashSet<int>(medic.ServiciiPrestate.Select(c => c.ServiciuID));
            var viewModel = new List<ServiciuPrestatData>();
            foreach (var serviciu in allServicii)
            {
                viewModel.Add(new ServiciuPrestatData
                {
                    ServiciuID = serviciu.ServiciuID,
                    Titlu = serviciu.Titlu,
                    IsPrestat = medicServicii.Contains(serviciu.ServiciuID)
                });
            }
            ViewData["Servicii"] = viewModel;
        }

        // POST: Medici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id, string[] selectedServicii)
        {
            if (id == null)
            {
                return NotFound();
            }
            var medicToUpdate = await _context.Medici
            .Include(i => i.ServiciiPrestate)
            .ThenInclude(i => i.Serviciu)
            .FirstOrDefaultAsync(m => m.MedicID == id);
            if (await TryUpdateModelAsync<Medic>(medicToUpdate, "", i => i.Nume))
            {
                UpdateServiciiPrestate(selectedServicii, medicToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateServiciiPrestate(selectedServicii, medicToUpdate);
            PopulateServiciuPrestatData(medicToUpdate);
            return View(medicToUpdate);
        }

        private void UpdateServiciiPrestate(string[] selectedServicii, Medic medicToUpdate)
        {
            if (selectedServicii == null)
            {
                medicToUpdate.ServiciiPrestate = new List<ServiciuPrestat>();
                return;
            }
            var selectedServiciiHS = new HashSet<string>(selectedServicii);
            var serviciiPrestate = new HashSet<int>
            (medicToUpdate.ServiciiPrestate.Select(c => c.Serviciu.ServiciuID));
            foreach (var serviciu in _context.Servicii)
            {
                if (selectedServiciiHS.Contains(serviciu.ServiciuID.ToString()))
                {
                    if (!serviciiPrestate.Contains(serviciu.ServiciuID))
                    {
                        medicToUpdate.ServiciiPrestate.Add(new ServiciuPrestat
                        {
                            MedicID = medicToUpdate.MedicID,
                            ServiciuID = serviciu.ServiciuID
                        });
                    }
                }
                else
                {
                    if (serviciiPrestate.Contains(serviciu.ServiciuID))
                    {
                        ServiciuPrestat servicuToRemove = medicToUpdate.ServiciiPrestate.FirstOrDefault(i => i.ServiciuID == serviciu.ServiciuID);
                        _context.Remove(servicuToRemove);
                    }
                }
            }
        }

        // GET: Medici/Delete/5
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
