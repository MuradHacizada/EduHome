using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EduHome.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                About = _db.Abouts.First(),
                AboutTeachers=_db.AboutTeachers.ToList(),
                Infos = _db.Infos.ToList(),
                NoticeBoards = _db.NoticeBoards.ToList(),
            };
            
            return View(aboutVM);
        }
    }
}
