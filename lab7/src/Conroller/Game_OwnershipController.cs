using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Controllers
{
    public class GameOwnershipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameOwnershipController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Пример метода получения всех владений играми
        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            var ownerships = await _context.GameOwnerships.Include(go => go.Player).Include(go => go.Game).ToListAsync();
            return View(ownerships);
        }

        [HttpPost]
        public IActionResult SaveChanges(int[] selectedGOwn, string action)
        {
            switch (action)
            {
                case "delete":
                    foreach (var gOwnId in selectedGOwn)
                    {
                        var gOwn = _context.GameOwnerships.Find(gOwnId);
                        if (gOwn != null)
                        {
                            _context.GameOwnerships.Remove(gOwn);
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
        public IActionResult Edit(int id)
        {
            //Console.WriteLine($"WHYYYYYYYYYYYY {id}");
            var gOwn = _context.GameOwnerships.Find(id);
            if (gOwn == null)
            {
                return NotFound();
            }
            ViewBag.Players = _context.Players.ToList();
            ViewBag.Games = _context.Games.ToList();
            return View(gOwn);
        }

        [HttpPost]
        public IActionResult Edit(GameOwnership gOwn, int id, int selected_player, int selected_game, bool isgift)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                gOwn.Id = id;
                try
                {
                    if (_context.Players.Where(p => p.Id == selected_player).Any() && _context.Games.Where(p => p.Id == selected_game).Any())
                    {
                        var existingGOwn = _context.GameOwnerships.Where(p => p.Id == id).First();
                        existingGOwn.Player = _context.Players.Where(p => p.Id == selected_player).First();
                        existingGOwn.Game = _context.Games.Where(p => p.Id == selected_game).First();
                        existingGOwn.IsGift = isgift;

                        _context.Update(existingGOwn);
                        _context.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    return Edit(gOwn.Id);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "ошъибкочка)))");
                    return Edit(gOwn.Id);
                }
            }
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Players = _context.Players.ToList();
            ViewBag.Games = _context.Games.ToList();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(GameOwnership gOwn, long selected_player, long selected_game, bool isgift)
        {
            Console.WriteLine($"bndithudibhtbidrtubidhrt oh: {isgift}");
            if (_context.Players.Where(p => p.Id == selected_player).Any() && _context.Games.Where(p => p.Id == selected_game).Any())
            {
                gOwn.Player = _context.Players.Where(p => p.Id == selected_player).First();
                gOwn.Game = _context.Games.Where(p => p.Id == selected_game).First();
                gOwn.IsGift = isgift;

                _context.GameOwnerships.Add(gOwn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
        }

        public IActionResult Delete(int id)
        {
            var gOwn = _context.GameOwnerships.Find(id);
            if (gOwn != null)
            {
                _context.GameOwnerships.Remove(gOwn);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}