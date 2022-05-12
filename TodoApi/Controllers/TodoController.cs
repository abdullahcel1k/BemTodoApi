using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        public async Task<List<Todo>> Get()
        {
           return await _todoContext.Todos.ToListAsync();
        }

        [HttpPost]
        public async Task<Todo> Post(Todo todo)
        {
            var addedTodo = await _todoContext.Todos.AddAsync(todo);
            _todoContext.SaveChanges();
            return addedTodo.Entity;
        }

        [HttpDelete]
        public async Task<Todo> Delete(int id)
        {
            var deletedTodo = await _todoContext.Todos.FindAsync(id);
            _todoContext.Todos.Remove(deletedTodo);
            _todoContext.SaveChanges();
            return deletedTodo;
        }

        [HttpPatch]
        public async Task<Todo> Patch(int id)
        {
            var findedTodo = await _todoContext.Todos.FindAsync(id);
            Todo updatedTodo = findedTodo;
            updatedTodo.IsComplete = !findedTodo.IsComplete;
            _todoContext.Todos.Update(updatedTodo);
            _todoContext.SaveChanges();
            return updatedTodo;
        }
    }
}
