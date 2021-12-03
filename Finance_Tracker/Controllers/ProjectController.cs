using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Models;
using Finance_Tracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Finance_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        public IProjectService _projectService;
        private readonly FinanceTrackerDB _financeTrackerDB;
        public ProjectController(IProjectService projectService, FinanceTrackerDB financeTrackerDB)
        {
            _projectService = projectService;
            _financeTrackerDB = financeTrackerDB;
        }
        // GET: api/Projects/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(Guid id)
        {
            var project = await _projectService.GetProjectById(id);
            {
                if (project == null)
                {
                    return NotFound();
                }
                return project;
            }
        }
        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult> GetProject()
        {
            var projects = await _projectService.GetProject();
            {
                if (projects == null)
                {
                    return NotFound();
                }

                return Ok(projects);
            }
        }
        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult> CreateProject([FromBody] Project project)
        {
            try
            {
                _projectService.CreateProject(project);
                await _financeTrackerDB.SaveChangesAsync();
                return Ok(project);
            }
            catch
            {
                throw;
            }
        }
        // PUT: api/Projects/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(Guid id, [FromBody] Project project)
        {
            try
            {
                if (id != project.Id)
                {
                    return BadRequest();
                }
                project.UpdatedAt = DateTime.Now;

                _financeTrackerDB.Entry(project).State = EntityState.Modified;

                await _financeTrackerDB.SaveChangesAsync();
                return Ok(project);
            }
            catch (Exception e)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e);
                }
            }
        }
        private bool ProjectExists(Guid id)
        {
            return _financeTrackerDB.Projects.Any(e => e.Id == id);
        }
        // DELETE: api/Projects/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectById(Guid id)
        {
            try
            {
                if (await _projectService.DeleteProjectById(id))
                {
                    await _financeTrackerDB.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound(id.ToString());
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
