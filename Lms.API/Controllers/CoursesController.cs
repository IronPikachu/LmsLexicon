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
    public class CoursesController : ControllerBase
    {
        private readonly LmsContext _context;
        private readonly IMapper _mapper;

        public CoursesController(LmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // CRUD - READ
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse()
        {
            var courseDto = await _mapper.ProjectTo<CourseDto>(_context.Course).ToListAsync();
            return Ok(courseDto);
        }
        // CRUD - READ
        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Course.Include(c => c.Modules).FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound(404);
            }

            var courseDto = _mapper.Map<CourseDto>(course);

            return Ok(courseDto);
        }
        // CRUD - UPDATE
        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto courseDto)
        {
            var preCourse = _context.Course.Find(id);
            
            var course = (Course)_mapper.ProjectTo<Course>((IQueryable)courseDto);

            if (course.Id != preCourse.Id)
            {
                return BadRequest(400);
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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
        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto courseDto)
        {
            var course = (Course)_mapper.ProjectTo<Course>((IQueryable)courseDto);

            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }
        // CRUD - DELETE
        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound(404);
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
