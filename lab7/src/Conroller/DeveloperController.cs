using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Controllers
{
    public class DeveloperController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeveloperController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Пример метода получения всех игроков
        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            var developers = await _context.Developers.ToListAsync();
            return View(developers);
        }

        [HttpPost]
        public IActionResult SaveChanges(long[] selectedDevelopers, string action)
        {
            switch (action)
            {
                case "delete":
                    foreach (var developerId in selectedDevelopers)
                    {
                        var developer = _context.Developers.Find(developerId);
                        if (developer != null)
                        {
                            _context.Developers.Remove(developer);
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
            var developer = _context.Developers.Find(id);
            if (developer == null)
            {
                return NotFound();
            }
            return View(developer);
        }

        [HttpPost]
        public IActionResult Edit(Developer developer, long id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {   
                    if (developer.Name != null && developer.WAmount >= 0)
                    {
                        var existingDeveloper = _context.Developers.Where(p => p.Id == id).First();
                        existingDeveloper.Name = developer.Name;
                        existingDeveloper.Rating = developer.Rating;
                        existingDeveloper.WAmount = developer.WAmount;

                        _context.Update(existingDeveloper);
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
        public IActionResult Create(Developer developer)
        {
            if (developer.Name != null && developer.WAmount >= 0)
            {
                _context.Developers.Add(developer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
        }

        public IActionResult Delete(long id)
        {
            var developer = _context.Developers.Find(id);
            if (developer != null)
            {
                _context.Developers.Remove(developer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}