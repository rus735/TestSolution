using DataManager.Models.Base;
using DataManager.Models.EF;
using Microsoft.EntityFrameworkCore;
using TestBereke.Services;
using Xunit;

namespace BerekeXUnit
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
               .UseInMemoryDatabase(databaseName: "ToDoListDB")
               .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new TodoContext(options))
            {
                context.TodoItems.Add(new TodoItem { Id = 1, IsComplete = false, Name = "JJJ" });
                context.TodoItems.Add(new TodoItem { Id = 2, IsComplete = false, Name = "GGG" });
                context.TodoItems.Add(new TodoItem { Id = 3, IsComplete = false, Name = "MMM" });
                context.SaveChanges();
            }

            var listLong = new List<long>
            {
                1,
                2
            };

            // Use a clean instance of the context to run the test
            using (var context = new TodoContext(options))
            {
                var movieRepository = new TodoItemsServices(context);
                var getToDoItems = await movieRepository.GetTodoItems();
                Assert.Equal(3, getToDoItems.Count);
                var getToDo = await movieRepository.GetTodoItem(1);
                Assert.Equal("JJJ", getToDo.Name);
                var updateToDo = await movieRepository.UpdateTodoItem(new DataManager.Models.FormData.TodoItemUpdateFormData
                {
                    Id = listLong,
                });
                Assert.True(updateToDo);
                var createToDo = await movieRepository.CreateTodoItem(new DataManager.Models.FormData.TodoItemFormData
                {
                    Name = "HHH"
                });
                Assert.True(createToDo);
                var deleteToDoItem = await movieRepository.DeleteTodoItem(new DataManager.Models.FormData.TodoItemUpdateFormData
                {
                    Id = listLong,
                });
                Assert.True(deleteToDoItem);
            }
        }
    }
}