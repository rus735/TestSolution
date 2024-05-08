using DataManager.Models.Base;
using DataManager.Models.EF;
using DataManager.Models.FormData;
using Microsoft.EntityFrameworkCore;
using TestSolution.Interfaces;

namespace TestSolution.Services
{
    public class TodoItemsServices : ITodoItems
    {
        public readonly TodoContext _context;
        public TodoItemsServices(TodoContext context) => _context = context;
        public async Task<TodoItem> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(c => c.Id == id);
            return todoItem;
        }
        public async Task<List<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        public async Task<bool> UpdateTodoItem(TodoItemUpdateFormData todoItemDTO)
        {
            var listTask = new List<TodoItem>();
            foreach (var item in todoItemDTO.Id)
            {
                var todoItem = await _context.TodoItems.FirstOrDefaultAsync(c => c.Id == item);
                if(todoItem == null) { return false; }
                listTask.Add(todoItem);
            }
            

            if (listTask.Count > 0)
            {
                foreach (var item in listTask)
                {
                    item.IsComplete = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> CreateTodoItem(TodoItemFormData todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = false,
                Name = todoItemDTO.Name
            };

            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteTodoItem(TodoItemUpdateFormData todoItemDTO)
        {
            var listTask = new List<TodoItem>();
            foreach (var item in todoItemDTO.Id)
            {
                var todoItem = await _context.TodoItems.FirstOrDefaultAsync(c => c.Id == item);
                if (todoItem != null)
                {
                    listTask.Add(todoItem);
                }          
            }

            if (listTask.Count > 0)
            {
                foreach (var item in listTask)
                {
                    _context.TodoItems.Remove(item);
                }
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}