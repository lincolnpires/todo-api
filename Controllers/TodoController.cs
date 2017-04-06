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

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
