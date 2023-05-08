﻿using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders =await _db.Sliders.ToListAsync(),
                Services = await _db.Services.OrderByDescending(x=>x.Id).Skip(2).Take(3).ToListAsync(),
                About = await _db.Abouts.FirstOrDefaultAsync(),
                Blogs =await _db.Blogs.ToListAsync(),
            };
            return View(homeVM);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
