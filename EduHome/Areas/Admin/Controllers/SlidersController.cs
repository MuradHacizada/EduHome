using EduHome.DAL;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SlidersController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.ToListAsync();
            return View(sliders);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            #region Save Image
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "ENTER IMAGE !");
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please Select Image");
                return View();
            }
            if (slider.Photo.IsOlder1Mb())
            {
                ModelState.AddModelError("Photo", "Max 1Mb");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img", "slider");
            slider.Image = await slider.Photo.SaveFileAsync(folder);
            #endregion

            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service dbService = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbService == null)
            {
                return BadRequest();
            }
            return View(dbService);

        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            if (dbSlider.IsDeactive)
            {
                dbSlider.IsDeactive = false;
            }
            else
            {
                dbSlider.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            return View(dbSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Slider slider)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            #region Save Image
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please Select Image");
                    return View();
                }
                if (slider.Photo.IsOlder1Mb())
                {
                    ModelState.AddModelError("Photo", "Max 1Mb");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "img", "slider");
                slider.Image = await slider.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath,folder, dbSlider.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbSlider.Image = slider.Image;
            }
            #endregion
            dbSlider.Title = slider.Title;
            dbSlider.Description = slider.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
