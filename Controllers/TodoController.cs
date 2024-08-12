using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interface;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        /* -------------------------------------------------------------------------- */
        /*                                 Properties                                 */
        /* -------------------------------------------------------------------------- */
        private readonly ITodoServices _todoServices;
        /* -------------------------------------------------------------------------- */

        /* -------------------------------------------------------------------------- */
        /*                                 Constructor                                */
        /* -------------------------------------------------------------------------- */
        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }
        /* -------------------------------------------------------------------------- */

        /* -------------------------------------------------------------------------- */
        /*                                   Methods                                  */
        /* -------------------------------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todo = await _todoServices.GetAllAsync();
                if (todo == null || !todo.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }

                return Ok(new { message = "Successfully retrieved all todos.", data = todo });
            }
            catch (Exception ex)
            {                
                return StatusCode(500, new {
                    message = "An error occurred while retrieving all todos.",error = ex.Message
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"No todo with ID \"{id}\" found." });
                }

                return Ok(new {
                    message = $"Successfully retrieved todo with 'ID' {id}.",
                    data = todo
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {
                    message = $"An error occurred while retrieving the todo item with ID \"{id}\".",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _todoServices.CreateTodoAsync(request);
                return Ok(new {message = "Todo created successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {
                    message = "An error occurred while creating the  crating Todo Item",
                    error = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo with ID \"{id}\" not found" });
                }

                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = $"Todo with ID \"{id}\" successfully updated." });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new {
                        message = $"An error occurred while updating todo with ID \"{id}\".",
                        error = ex.Message
                });
            }
        }

        /* ------------------------------- QUESTION!!! ------------------------------ */
        /* ------------- Why do I need `("{id:guid}")` in `[HttpDelete]` ------------ */
         /* --------------------- if I have it in the parameters? -------------------- */
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new { message = "Todo with ID \"{id}\" successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new {
                        message = $"An error occurred while deleting todo with ID \"{id}\"",
                        error = ex.Message
                });
            }
        }
        /* -------------------------------------------------------------------------- */
    }
}