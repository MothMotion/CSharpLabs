using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players.ToListAsync();
            return View(players);
        }

        [HttpPost]
        public IActionResult SaveChanges(long[] selectedPlayers, string action)
        {
            switch (action)
            {
                case "delete":
                    foreach (var playerId in selectedPlayers)
                    {
                        var player = _context.Players.Find(playerId);
                        if (player != null)
                        {
                            _context.Players.Remove(player);
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
            var player = _context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        [HttpPost]
        public IActionResult Edit(Player player, long id, string Nickname, double MoneySum)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {   
                    if (player.Nickname != null && player.MoneySum >= 0)
                    {
                        var existingPlayer = _context.Players.Where(p => p.Id == id).First();
                        existingPlayer.Nickname = player.Nickname;
                        existingPlayer.MoneySum = player.MoneySum;

                        _context.Update(existingPlayer);
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
        public IActionResult Create(Player player)
        {
            if (player.Nickname != null && player.MoneySum >= 0)
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
        }

        public IActionResult Delete(long id)
        {
            var player = _context.Players.Find(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}