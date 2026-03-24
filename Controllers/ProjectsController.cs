using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointScore.Models;
using PointScore.Data;
using PointScore.Models.DTOs;

namespace PointScore.Controllers;

/// <summary>
/// Controller for managing projects.
/// Provides CRUD endpoints for the Project entity.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly CoreDbContext _context;

    public ProjectsController(CoreDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="projectDto">The DTO object containing the project data.</param>
    /// <returns>The newly created project.</returns>
    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject(ProjectDto projectDto)
    {
        var project = new Project
        {
            Name = projectDto.Name
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }
    
    /// <summary>
    /// Retrieves a list of all projects, showing only the associated FeatureScoreResult ID.
    /// </summary>
    /// <returns>A list of projects with score data.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProjectResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetProjects()
    {
        // Usa una proyección para mapear directamente a tu DTO.
        return await _context.Projects
            .Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name ?? "Unknown",
                FeatureScoreResultId = p.FeatureScoreResult != null ? p.FeatureScoreResult.Id : null
            })
            .ToListAsync();
    }
    
    /// <summary>
    /// Retrieves a specific project by its ID, showing only the associated FeatureScoreResult ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <returns>The project with the specified ID and its score data.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectResponseDto), 200)]
    public async Task<ActionResult<ProjectResponseDto>> GetProject(int id)
    {
        // Usa una proyección para mapear directamente a tu DTO.
        var project = await _context.Projects
            .Where(p => p.Id == id)
            .Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name ?? "Unknown",
                FeatureScoreResultId = p.FeatureScoreResult != null ? p.FeatureScoreResult.Id : null
            })
            .FirstOrDefaultAsync();

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="projectDto">The project object with the updated data.</param>
    /// <returns>An HTTP 204 No Content response if the update was successful.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectDto projectDto)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }

        // Update only the properties sent in the DTO
        project.Name = projectDto.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Projects.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a project by its ID. The associated FeatureScoreResult will also be deleted
    /// due to the cascade delete configuration.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>An HTTP 204 No Content response if the deletion was successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}