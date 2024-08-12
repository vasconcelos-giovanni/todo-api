using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interface;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public class TodoServices : ITodoServices
    {
        /* -------------------------------------------------------------------------- */
        /*                                 Properties                                 */
        /* -------------------------------------------------------------------------- */
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoServices> _logger;
        private readonly IMapper _mapper;
        /* -------------------------------------------------------------------------- */

        /* -------------------------------------------------------------------------- */
        /*                                 Constructor                                */
        /* -------------------------------------------------------------------------- */
        public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        /* -------------------------------------------------------------------------- */

        /* -------------------------------------------------------------------------- */
        /*                                   Methods                                  */
        /* -------------------------------------------------------------------------- */
        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        /* -------------------------------------------------------------------------- */
        /* ------------------------------- QUESTION!!! ------------------------------ */
        /* ----- How to use "soft deletes" or some logging of the deleted value? ---- */
        /* -------------------------------------------------------------------------- */
        public async Task DeleteTodoAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            else
            {
                /* -------------------------------------------------------------------------- */
                /* ------------------------------- QUESTION!!! ------------------------------ */
                /* ---- Could I have an interface for exceptions and heirs for each model --- */
                /* -- to avoid error messages repetitions and inconsistences between them? -- */
                /* -------------------------------------------------------------------------- */
                throw new Exception($"No todo with ID \"{id}\" found.");
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception("No Todo items found.");
            }
            
            return todo;
        }

        public Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"No todo with ID \"{id}\" found.");
            }

            return todo;
        }

        /* -------------------------------------------------------------------------- */
        /* ------------------------------- QUESTION!!! ------------------------------ */
        /* ---------------- How to make something as the `$fillable` ---------------- */
        /* --------------------- property of the Laravel models? -------------------- */
        /* -------------------------------------------------------------------------- */
        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo = null)
                {
                    throw new Exception($"Todo with ID \"{id}\" not found.");
                }

                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }

                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }

                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }

                if (request.DueDate != null)
                {
                    todo.DueDate = request.DueDate.Value;
                }

                if (request.Priority != null)
                {
                    todo.Priority = request.Priority.Value;
                }

                todo.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    $"An error occurred while updating the todo with ID \"{id}\"."
                    );
                throw;
            }
        }
        /* -------------------------------------------------------------------------- */
    }
}