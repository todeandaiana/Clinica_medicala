using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Data;
using Clinica_medicala.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace Clinica_medicala.Controllers
{
    [Authorize(Policy = "DoarReceptie")]
    public class PacientiController : Controller
    {
        private readonly ClinicaContext _context;
        private string _baseUrl = "https://localhost:7011/api/Pacienti";

        public PacientiController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Pacienti
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var pacienti = JsonConvert.DeserializeObject<List<Pacient>>(await response.Content.ReadAsStringAsync());
                return View(pacienti);
            }
            return NotFound();

        }

        // GET: Pacienti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var pacient = new HttpClient();
            var response = await pacient.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var client = JsonConvert.DeserializeObject<Pacient>(
                await response.Content.ReadAsStringAsync());
                return View(client);
            }
            return NotFound();
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
            if (!ModelState.IsValid) return View(pacient);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(pacient);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ex.Message}");
            }
            return View(pacient);
        }

        // GET: Pacienti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var pacient = JsonConvert.DeserializeObject<Pacient>(
                await response.Content.ReadAsStringAsync());
                return View(pacient);
            }
            return new NotFoundResult();
        }

        // POST: Pacienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacientID,Nume,Adresa,DataNasterii")] Pacient pacient)
        {
            if (!ModelState.IsValid) return View(pacient);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(pacient);
            var response = await client.PutAsync($"{_baseUrl}/{pacient.PacientID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(pacient);
        }

        // GET: Pacienti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var pacient = JsonConvert.DeserializeObject<Pacient>(await response.Content.ReadAsStringAsync());
                return View(pacient);
            }
            return new NotFoundResult();
        }

        // POST: Pacienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("PacientID")] Pacient pacient)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{pacient.PacientID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(pacient),
               Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ex.Message}");
            }
            return View(pacient);
        }


      /*  private bool PacientExists(int id)
        {
          return (_context.Pacienti?.Any(e => e.PacientID == id)).GetValueOrDefault();
        }
      */
    }
}
