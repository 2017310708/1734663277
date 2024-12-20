using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SW_Parcial_v3.Data;
using SW_Parcial_v3.Dtos;
using SW_Parcial_v3.Models;

namespace SW_Parcial_v3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            var project = await _context.Projects.FindAsync(taskDto.ProjectId);
            if (project == null)
                return NotFound(new ResponseDto
                {
                    Message = "Proyecto no encontrado",
                    Status = "fail"
                });

            if (taskDto.DueDate < project.StartDate)
                return BadRequest(new ResponseDto
                {
                    Message = "La fecha límite de la tarea no puede ser anterior a la fecha de inicio del proyecto.",
                    Status = "fail"
                });

            var task = new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = "Pending",
                Priority = taskDto.Priority,
                ProjectId = taskDto.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { projectId = task.ProjectId }, new ResponseDto
            {
                Message = "Tarea creada correctamente",
                Status = "success",
                Data = task
            });
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetTasks(int projectId, [FromQuery] string? status, [FromQuery] string? priority)
        {
            var query = _context.Tasks.Where(t => t.ProjectId == projectId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(t => t.Priority.Equals(priority, StringComparison.OrdinalIgnoreCase));

            var tasks = await query.ToListAsync();

            return Ok(new ResponseDto
            {
                Message = "Tareas obtenidas correctamente",
                Status = "success",
                Data = tasks
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound(new ResponseDto
                {
                    Message = "Tarea no encontrada",
                    Status = "fail"
                });

            var project = await _context.Projects.FindAsync(task.ProjectId);
            if (taskDto.DueDate < project.StartDate)
                return BadRequest(new ResponseDto
                {
                    Message = "La fecha límite de la tarea no puede ser anterior a la fecha de inicio del proyecto.",
                    Status = "fail"
                });

            task.Name = taskDto.Name;
            task.Description = taskDto.Description;
            task.DueDate = taskDto.DueDate;
            task.Priority = taskDto.Priority;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto
            {
                Message = "Tarea actualizada correctamente",
                Status = "success",
                Data = task
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound(new ResponseDto
                {
                    Message = "Tarea no encontrada",
                    Status = "fail"
                });

            task.IsActive = 0;
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto
            {
                Message = "Tarea eliminada correctamente",
                Status = "success"
            });
        }
    }
}
