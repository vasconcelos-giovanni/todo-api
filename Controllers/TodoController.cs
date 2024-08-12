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

                string test = "TEST";

                return Ok(new { message = "Successfully retrieved all todos.", data = todo });
            }
            catch (Exception ex)
            {                
                return StatusCode(500, new { message = "An error occurred while retrieving all todos.", error = ex.Message });
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
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
        }
        /* -------------------------------------------------------------------------- */
    }
}