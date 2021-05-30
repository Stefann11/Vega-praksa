using Contracts.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Entities;
using System;
using System.Linq;
using System.Security.Claims;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Worker, Admin")]
    [ApiController]
    public class DailyTimeSheetController : ControllerBase
    {
        private readonly IDailyTimeSheetService _dailyTimeSheetService;

        public DailyTimeSheetController(IDailyTimeSheetService dailyTimeSheetService)
        {
            _dailyTimeSheetService = dailyTimeSheetService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dailyTimeSheetService.GetAll());
        }

        [HttpGet("calendar")]
        public IActionResult GetCalendarItem([FromQuery] DateTime date)
        {
            return Ok(_dailyTimeSheetService.GetCalendarItems(date, int.Parse(User.FindFirst("id")?.Value)));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_dailyTimeSheetService.GetById(id));
        }

        [HttpPost]
        public IActionResult Save(DailyTimeSheet dailyTimeSheet)
        {
            return Ok(_dailyTimeSheetService.Save(dailyTimeSheet, int.Parse(User.FindFirst("id")?.Value)));
        }

        [HttpPut]
        public IActionResult Edit(DailyTimeSheet dailyTimeSheet)
        {
            return Ok(_dailyTimeSheetService.Edit(dailyTimeSheet, int.Parse(User.FindFirst("id")?.Value)));
        }

        [HttpDelete]
        public IActionResult Delete(DailyTimeSheet dailyTimeSheet)
        {
            return Ok(_dailyTimeSheetService.Delete(dailyTimeSheet));
        }

        [HttpGet("day")]
        public IActionResult GetDayItems([FromQuery] DateTime date)
        {
            return Ok(_dailyTimeSheetService.GetAllByDate(date, int.Parse(User.FindFirst("id")?.Value)));
        }

        [HttpGet("search")]
        public IActionResult SearchDailyTimeSheets([FromQuery] string employee,
                                                   [FromQuery] string client,
                                                   [FromQuery] string project,
                                                   [FromQuery] string category,
                                                   [FromQuery] string startDate,
                                                   [FromQuery] string endDate)
        {
            SearchDto searchDto = new SearchDto
            {
                Employee = employee,
                Client = client,
                Project = project,
                Category = category,
                StartDate = startDate,
                EndDate = endDate
            };
            return Ok(_dailyTimeSheetService.GetAllBySearch(searchDto));
        }

        [HttpGet("searchEmployee")]
        public IActionResult SearchDailyTimeSheetsEmployee([FromQuery] string employee,
                                                   [FromQuery] string client,
                                                   [FromQuery] string project,
                                                   [FromQuery] string category,
                                                   [FromQuery] string startDate,
                                                   [FromQuery] string endDate)
        {
            SearchDto searchDto = new SearchDto
            {
                Employee = User.FindFirst("id")?.Value,
                Client = client,
                Project = project,
                Category = category,
                StartDate = startDate,
                EndDate = endDate
            };
            return Ok(_dailyTimeSheetService.GetByEmployee(searchDto));
        }
    }
}
