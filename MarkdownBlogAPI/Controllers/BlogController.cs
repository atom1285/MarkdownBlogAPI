using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarkdownBlogAPI.DataTransferObjects;
using MarkdownBlogAPI.DataTransferObjects.DataTransferObjects;

namespace MarkdownBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(DatabaseContext context) : ControllerBase
    {
        // GET: api/Blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> Index()
        {
            return await context.Blogs
                .Select(x => BlogToDto(x))
                .ToListAsync();
        }

        // GET: api/Blog/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Blog>> Show(long id)
        {
            var blog = await context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // POST: api/Blog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> Store(Blog blog)
        {
            context.Blogs.Add(blog);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Show), new { id = blog.Id }, blog);
        }

        // PUT: api/Blog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            context.Entry(blog).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Destroy(long id)
        {
            var blog = await context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(long id)
        {
            return context.Blogs.Any(e => e.Id == id);
        }

        private static BlogDTO BlogToDto(Blog blog)
        {
            return new BlogDTO
            {
                Id = blog.Id,
                Title = blog.Title
            };
        }
    }
}