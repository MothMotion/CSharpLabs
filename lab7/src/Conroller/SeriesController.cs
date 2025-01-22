using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Пример метода получения всех игроков
        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            var series = await _context.GameSeries.ToListAsync();
            return View(series);
        }

        [HttpPost]
        public IActionResult SaveChanges(long[] selectedSeries, string action)
        {
            switch (action)
            {
                case "delete":
                    foreach (var seriesId in selectedSeries)
                    {
                        var serie = _context.GameSeries.Find(seriesId);
                        if (serie != null)
                        {
                            _context.GameSeries.Remove(serie);
                        }
                    }
                    _context.SaveChanges();
                    break;

                case "add":
                    // Логика добавления нового элемента (например, перенаправление на форму добавления)
                    return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            //Console.WriteLine($"WHYYYYYYYYYYYY {id}");
            var serie = _context.GameSeries.Find(id);
            if (serie == null)
            {
                return NotFound();
            }
            return View(serie);
        }

        [HttpPost]
        public IActionResult Edit(Series serie, long id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {   
                    if (serie.Name != null)
                    {
                        var existingSerie = _context.GameSeries.Where(p => p.Id == id).First();
                        existingSerie.Name = serie.Name;

                        _context.Update(existingSerie);
                        _context.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    return Edit(id);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "ошъибкочка)))");
                    return Edit(id);
                }
            }
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Series serie)
        {
            if (serie.Name != null)
            {
                _context.GameSeries.Add(serie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
        }

        public IActionResult Delete(long id)
        {
            var serie = _context.GameSeries.Find(id);
            if (serie != null)
            {
                _context.GameSeries.Remove(serie);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}