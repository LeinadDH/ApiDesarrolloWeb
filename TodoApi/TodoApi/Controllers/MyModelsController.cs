using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyModelsController : ControllerBase
    {
        private readonly MyModelContext _context;

        public MyModelsController(MyModelContext context)
        {
            _context = context;
        }

        // GET: api/MyModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyModel>>> GetMyModels()
        {
            return await _context.MyModels.ToListAsync();
        }

        // GET: api/MyModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyModel>> GetMyModel(long id)
        {
            var myModel = await _context.MyModels.FindAsync(id);

            if (myModel == null)
            {
                return NotFound();
            }

            return myModel;
        }

        // PUT: api/MyModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyModel(long id, MyModel myModel)
        {
            if (id != myModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(myModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyModelExists(id))
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

        // POST: api/MyModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MyModel>> PostTodoItem(MyModel myModel)
        {
            _context.MyModels.Add(myModel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetMyModel), new { id = myModel.Id }, myModel);
        }

        // DELETE: api/MyModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyModel(long id)
        {
            var myModel = await _context.MyModels.FindAsync(id);
            if (myModel == null)
            {
                return NotFound();
            }

            _context.MyModels.Remove(myModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MyModelExists(long id)
        {
            return _context.MyModels.Any(e => e.Id == id);
        }
    }
}
