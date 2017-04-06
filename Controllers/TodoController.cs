using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return this.todoRepository.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = this.todoRepository.Find(id);
            if (item == null) {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null) {
                return BadRequest();
            }

            this.todoRepository.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id) {
                return BadRequest();
            }

            var todoToBeUpdated = this.todoRepository.Find(id);
            if (todoToBeUpdated == null) {
                return NotFound();
            }

            todoToBeUpdated.IsComplete = item.IsComplete;
            todoToBeUpdated.Name = item.Name;

            this.todoRepository.Update(todoToBeUpdated);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todoToBeDeleted = this.todoRepository.Find(id);
            if (todoToBeDeleted == null) {
                return NotFound();
            }

            this.todoRepository.Remove(id);
            return new NoContentResult();
        }
    }
}
