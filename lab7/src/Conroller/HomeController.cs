using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
    
    public ActionResult Create()
    {
        return View();
    }
}