using DataManager.Models.Base;
using DataManager.Models.EF;
using DataManager.Models.FormData;
using Microsoft.AspNetCore.Mvc;
using TestBereke.Interfaces;

namespace TestBereke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : Controller
    {
        private readonly ITodoItems _todoService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoItems todoItems)
        {
            _todoService = todoItems;
            _logger = logger;
        }
        [HttpGet("GetTodoItems")]
        public async Task<IActionResult> GetTodoItems()
        {
            var result = new Response<List<TodoItem>>();
            try
            {
                var res = await _todoService.GetTodoItems();

                if (res.Count > 0)
                {
                    result.StatusCode = 0;
                    result.Result = res;
                }
                else
                {
                    result.StatusCode = -2;
                    result.Result = res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка метод GetTodoItems()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }
            return Ok(result);
        }
        [HttpGet("GetTodoItem")]
        public async Task<IActionResult> GetTodoItem(int id)
        {
            var result = new Response<TodoItem>();
            try
            {
                var res = await _todoService.GetTodoItem(id);

                if (res != null)
                {
                    result.StatusCode = 0;
                    result.Result = res;
                }
                else
                {
                    result.StatusCode = -2;
                    result.Result = res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка метод GetTodoItem()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }
            return Ok(result);
        }
        [HttpPut("UpdateTodoItem")]
        public async Task<IActionResult> UpdateTodoItem([FromForm] TodoItemUpdateFormData todoItemDTO)
        {
            var result = new Response<bool>();
            try
            {
                var res = await _todoService.UpdateTodoItem(todoItemDTO);

                if (res)
                {
                    result.StatusCode = 0;
                    result.Result = res;
                }
                else
                {
                    result.StatusCode = -2;
                    result.Result = res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка метод GetTodoItem()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }
            return Ok(result);
        }
        [HttpPost("CreateTodoItem")]
        public async Task<IActionResult> CreateTodoItem([FromForm] TodoItemFormData todoItemDTO)
        {
            var result = new Response<bool>();
            try
            {
                var res = await _todoService.CreateTodoItem(todoItemDTO);
                result.StatusCode = 0;
                result.Result = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка метод GetTodoItem()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }
            return Ok(result);
        }
        [HttpDelete("DeleteTodoItem")]
        public async Task<IActionResult> DeleteTodoItem([FromForm] TodoItemUpdateFormData todoItemDTO)
        {
            var result = new Response<bool>();
            try
            {
                var res = await _todoService.DeleteTodoItem(todoItemDTO);

                if (res)
                {
                    result.StatusCode = 0;
                    result.Result = res;
                }
                else
                {
                    result.StatusCode = -2;
                    result.Result = res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка метод DeleteTodoItem()");
                result.StatusCode = -1;
                result.ErrorMessage = ex.Message.ToString();
            }
            return Ok(result);
        }
    }
}
