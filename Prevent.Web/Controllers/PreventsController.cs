﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prevent.Web.Data;
using Prevent.Web.Data.Entities;
using Prevent.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PreventsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;

        public PreventsController(
            DataContext context,
            IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Prevents.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventEntity preventEntity = await _context.Prevents.FindAsync(id);
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (preventEntity == null)
            {
                return NotFound();
            }

            return View(preventEntity);
        }

        public IActionResult Create()
        {
            ViewBag.PreventTypes = _context.PreventTypes.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Name.ToString(),
                    Value = d.Id.ToString(),
                };
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreventEntity preventEntity)
        {
            if (ModelState.IsValid)
            {
                preventEntity.Title = preventEntity.Title.ToUpper();
                preventEntity.PreventType = await _context.PreventTypes.FirstOrDefaultAsync(u => u.Id == preventEntity.PreventTypeId);
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    preventEntity.File = await _imageHelper.UploadImageAsync(HttpContext.Request.Form.Files[0], "prevents");
                }
                preventEntity.Date = DateTime.UtcNow;
                _context.Add(preventEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preventEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventEntity preventEntity = await _context.Prevents.FindAsync(id);
            if (preventEntity == null)
            {
                return NotFound();
            }
            ViewBag.PreventTypes = _context.PreventTypes.ToList().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Name.ToString(),
                    Value = d.Id.ToString(),
                };
            });
            return View(preventEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreventEntity preventEntity)
        {
            if (id != preventEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                preventEntity.Title = preventEntity.Title.ToUpper();
                preventEntity.PreventType = await _context.PreventTypes.FirstOrDefaultAsync(u => u.Id == preventEntity.PreventTypeId);
                _context.Update(preventEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preventEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventEntity = await _context.Prevents.FindAsync(id);
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (preventEntity == null)
            {
                return NotFound();
            }
            _context.Prevents.Remove(preventEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}