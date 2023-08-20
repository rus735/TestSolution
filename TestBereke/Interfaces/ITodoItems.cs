using DataManager.Models.EF;
using DataManager.Models.FormData;

namespace TestBereke.Interfaces
{
    public interface ITodoItems
    {
        Task<TodoItem> GetTodoItem(int id);
        Task<List<TodoItem>> GetTodoItems();
        Task<bool> UpdateTodoItem(TodoItemUpdateFormData todoItemDTO);
        Task<bool> CreateTodoItem(TodoItemFormData todoItemDTO);
        Task<bool> DeleteTodoItem(TodoItemUpdateFormData todoItemDTO);
    }
}