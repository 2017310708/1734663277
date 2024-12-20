using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SW_Parcial_v3.Data;
using SW_Parcial_v3.Dtos;
using SW_Parcial_v3.Models;

namespace SW_Parcial_v3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] string? status)
        {
            var query = _context.Projects.AsQueryable();
            // Filtro para obtener solo los proyectos activos
            query = query.Where(p => p.IsActive == 1);
            // Incluir las tareas asociadas al proyecto
            query = query.Include(p => p.Tasks);

            if (!string.IsNullOrEmpty(status)){
                query = query.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            var projects = await query.ToListAsync();

            return Ok(new ResponseDto
            {
                Message = "Proyectos obtenidos correctamente",
                Status = "success",
                Data = projects
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
        {
            if (projectDto.StartDate > projectDto.EndDate)
                return BadRequest(new ResponseDto
                {
                    Message = "La fecha de inicio no puede ser posterior a la fecha de fin.",
                    Status = "fail"
                });

            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                Status = "Pendiente"
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, new ResponseDto
            {
                Message = "Proyecto creado correctamente",
                Status = "success",
                Data = project
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDto projectDto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound(new ResponseDto
                {
                    Message = "Proyecto no encontrado",
                    Status = "fail"
                });

            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Status = projectDto.Status;

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto
            {
                Message = "Proyecto actualizado correctamente",
                Status = "success",
                Data = project
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound(new ResponseDto
                {
                    Message = "Proyecto no encontrado",
                    Status = "fail"
                });

            project.IsActive = 0;
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto
            {
                Message = "Proyecto eliminado correctamente",
                Status = "success"
            });
        }
    }
}
