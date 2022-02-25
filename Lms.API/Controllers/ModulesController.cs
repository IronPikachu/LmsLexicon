#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.DATA.Data;
using Lms.CORE.Entities;
using AutoMapper;
using Lms.CORE.Dto;

namespace Lms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsContext _context;
        private readonly IMapper _mapper;

        public ModulesController(LmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // CRUD - READ
        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule()
        {
            var moduleDto = await _mapper.ProjectTo<ModuleDto>(_context.Module).ToListAsync();
            return Ok(moduleDto);
        }
        // CRUD - READ
        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var module = await _context.Module.FindAsync(id);

            if (module == null)
            {
                return NotFound(404);
            }

            var moduleDto = _mapper.Map<ModuleDto>(module);

            return Ok(moduleDto);
        }
        // CRUD - UPDATE
        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleDto moduleDto)
        {
            var preModule = _context.Course.Find(id);

            var module = (Module)_mapper.ProjectTo<Module>((IQueryable)moduleDto);

            if (module.Id != preModule.Id)
            {
                return BadRequest(400);
            }

            _context.Entry(module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound(404);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // CRUD - CREATE
        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModuleDto moduleDto)
        {
            var module = (Module)_mapper.ProjectTo<Module>((IQueryable)moduleDto);

            _context.Module.Add(module);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = module.Id }, module);
        }
        // CRUD - DELETE
        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await _context.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound(404);
            }

            _context.Module.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _context.Module.Any(e => e.Id == id);
        }
    }
}
