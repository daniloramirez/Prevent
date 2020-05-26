using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prevent.Web.Data;
using Prevent.Web.Data.Entities;
using Prevent.Web.Helpers;

namespace Prevent.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreventsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public PreventsController(
            DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/Prevents
        [HttpGet]
        public IEnumerable<PreventEntity> GetPrevents()
        {
            return _context.Prevents;
        }

        // GET: api/Prevents/5
        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetPreventEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preventEntity = await _context.Prevents.FindAsync(id);

            if (preventEntity == null)
            {
                return NotFound();
            }

            return Ok(preventEntity);
        }*/
        [HttpGet("{PreventType}")]
        public async Task<IActionResult> GetPreventEntity(int PreventType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var preventEntity = await _context.PreventTypes.FirstOrDefaultAsync(t => t.Id == PreventType);

            var preventEntity = await _context.PreventTypes
                .Include(t => t.Prevents)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == PreventType);

            if (preventEntity == null)
            {
                return NotFound();
            }

            return Ok(_converterHelper.ToPreventResponse(preventEntity));
        }
    }
}