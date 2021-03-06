﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prevent.Web.Data;
using Prevent.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PreventTypesController : Controller
    {
        private readonly DataContext _context;

        public PreventTypesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PreventTypes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventTypeEntity preventTypeEntity = await _context.PreventTypes.FindAsync(id);
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (preventTypeEntity == null)
            {
                return NotFound();
            }

            return View(preventTypeEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreventTypeEntity preventTypeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preventTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preventTypeEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreventTypeEntity preventTypeEntity = await _context.PreventTypes.FindAsync(id);
            if (preventTypeEntity == null)
            {
                return NotFound();
            }
            return View(preventTypeEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreventTypeEntity preventTypeEntity)
        {
            if (id != preventTypeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(preventTypeEntity);
                await _context.SaveChangesAsync();   
                return RedirectToAction(nameof(Index));
            }
            return View(preventTypeEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventTypeEntity = await _context.PreventTypes.FindAsync(id);
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (preventTypeEntity == null)
            {
                return NotFound();
            }

            _context.PreventTypes.Remove(preventTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}