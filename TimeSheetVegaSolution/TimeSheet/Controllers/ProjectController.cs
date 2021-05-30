using Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Contracts.Interface.Service;
using Microsoft.AspNetCore.Authorization;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Authorize(Roles = "Worker, Admin")]
        public IActionResult GetAll()
        {
            return Ok(_projectService.GetAll());
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllByPageAndQuery([FromQuery] string name = "", [FromQuery] int page = 1, [FromQuery] string letter = "")
        {
            Response.Headers.Add("Number-Of-Pages", _projectService.GetNumberOfPages().ToString());
            return Ok(_projectService.GetAllByPageAndQuery(name, page, letter));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            return Ok(_projectService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Save(Project project)
        {
            return Ok(_projectService.Save(project));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Project project)
        {
            return Ok(_projectService.Edit(project));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Project project)
        {
            return Ok(_projectService.Delete(project));
        }

        [HttpGet("byClient/{clientId}")]
        [Authorize(Roles = "Worker, Admin")]
        public IActionResult GetAllByClient(int clientId)
        {
            return Ok(_projectService.GetAllByClient(clientId));
        }
    }
}
