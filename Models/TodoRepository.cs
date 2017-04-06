using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext context;

        public TodoRepository(TodoContext context)
        {
            this.context = context;

            if (this.context.TodoItems.Count() == 0) {
                Add(new TodoItem { Name = "learn asp.net core" });
            }
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return this.context.TodoItems.ToList();
        }

        public void Add(TodoItem item)
        {
            this.context.TodoItems.Add(item);
            this.context.SaveChanges();
        }

        public TodoItem Find(long key)
        {
            return this.context.TodoItems.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = this.context.TodoItems.First(t => t.Key == key);
            this.context.TodoItems.Remove(entity);
            this.context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            this.context.TodoItems.Update(item);
            this.context.SaveChanges();
        }
    }
}