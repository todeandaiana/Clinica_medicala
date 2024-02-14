using Clinica_medicala.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Data;
using Clinica_medicala.Models.ClinicaViewModels;



namespace Clinica_medicala.Controllers
{
    public class HomeController : Controller

    {
        //  private readonly ILogger<HomeController> _logger;

        private readonly ClinicaContext _context;
        public HomeController(ClinicaContext context)
        {
            _context = context;
        }
        /* public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         } */

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<GrupProgramare> data =
            from order in _context.Programari
            group order by order.DataProgramare into dateGroup
            select new GrupProgramare()
            {
                DataProgramare = dateGroup.Key,
                NrServicii = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}