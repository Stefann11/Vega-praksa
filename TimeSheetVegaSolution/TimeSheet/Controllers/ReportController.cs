using Contracts.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using System.Collections.Generic;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Worker, Admin")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("pdf")]
        public IActionResult GeneratePdf([FromBody] List<DailyTimeSheet> dailyTimeSheets)
        {
            _reportService.GeneratePdf(dailyTimeSheets);
            string filename = "report.pdf";
            string filepath = @"..\Report\report.pdf";
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());

            return File(filedata, "application/octet-stream", "report.pdf");
        }

        [HttpPost("excel")]
        public IActionResult GenerateExcel([FromBody] List<DailyTimeSheet> dailyTimeSheets)
        {
            _reportService.GenerateExcel(dailyTimeSheets);
            string filename = "report.xlsx";
            string filepath = @"..\Report\report.xlsx";
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());

            return File(filedata, "application/octet-stream", "report.xlsx");
        }

        [HttpGet("print")]
        public IActionResult PrintPdf()
        {
            _reportService.PrintPdf();
            return Ok();
        }
    }
}
