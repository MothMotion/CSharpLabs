using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Connections;
using System.ComponentModel.DataAnnotations;


namespace Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Пример метода получения всех игр
       [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await _context.Games.Include(go => go.Series).Include(go => go.Developer).ToListAsync();
            return View(games);
        }
        
        [HttpPost]
        public IActionResult SaveChanges(int[] selectedGames, string action)
        {
            switch (action)
            {
                case "delete":
                    foreach (var gameId in selectedGames)
                    {
                        var game = _context.Games.Find(gameId);
                        if (game != null)
                        {
                            _context.Games.Remove(game);
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
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewBag.GameSeries = _context.GameSeries.ToList();
            ViewBag.Developers = _context.Developers.ToList();
            return View(game);
        }

        [HttpPost]
        public IActionResult Edit(Game game, int id, int selected_series, int selected_dev, string Description)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                game.Id = id;
                try
                {   
                    if (_context.Games.Where(p => p.Id == id).Any() && _context.Developers.Where(p => p.Id == selected_dev).Any() && game.Cost >= 0)
                    {
                        var existingGame = _context.Games.Where(p => p.Id == id).First();
                        existingGame.Name = game.Name;
                        existingGame.Cost = game.Cost;
                        if(_context.GameSeries.Where(p => p.Id == selected_series).Any())
                            Console.WriteLine(_context.GameSeries.Where(p => p.Id == selected_series).First());
                        if(_context.GameSeries.Where(p => p.Id == selected_series).Any())
                            existingGame.Series = _context.GameSeries.Where(p => p.Id == selected_series).First();
                        else {
                            existingGame.Series = null;
                            Console.WriteLine($"HELLO???? {existingGame.Series}");
                        }
                        existingGame.Developer = _context.Developers.Where(p => p.Id == selected_dev).First();
                        existingGame.Description = Description;
                        //existingGame.ReleaseDate.ToUniversalTime();

                        _context.Update(existingGame);
                        _context.SaveChanges();
                        if(!_context.GameSeries.Where(p => p.Id == selected_series).Any())
                            _context.Database.ExecuteSqlRaw($"UPDATE game SET series = NULL WHERE id = {existingGame.Id}");

                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    return Edit(game.Id);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "ошъибкочка)))");
                    return Edit(game.Id);
                }
            }
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GameSeries = _context.GameSeries.ToList();
            ViewBag.Developers = _context.Developers.ToList();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Game game, long selected_series, long selected_dev)
        {
            if (game.Name != null && _context.Developers.Where(p => p.Id == selected_dev).Any())
            {
                if(_context.GameSeries.Where(p => p.Id == selected_series).Any())
                    game.Series = _context.GameSeries.Where(p => p.Id == selected_series).First();
                game.Developer = _context.Developers.Where(p => p.Id == selected_dev).First();

                _context.Games.Add(game);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
        }

        public IActionResult Delete(int id)
        {
            var game = _context.Games.Find(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}