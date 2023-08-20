using DataManager.Models.EF;

namespace DataManager.Models.Base
{
    public static class Function
    {
        public static TodoItem ItemToDTO(TodoItem todoItem) =>
            new TodoItem
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
