using ceTe.DynamicPDF.Printing;
using Contracts.Interface.Service;
using Model.Entities;
using RandomSolutions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BusinessLogic.Implementation
{
    public class ReportService : IReportService
    {
        public byte[] GeneratePdf(List<DailyTimeSheet> dailyTimeSheets)
        {
            byte[] pdf = dailyTimeSheets.ToPdf(scheme =>
            {
                scheme.Title = "Report";
                scheme.PageOrientation = ArrayToPdfOrientations.Portrait;
                scheme.PageMarginLeft = 15;
                scheme.AddColumn("Date", dailyTimeSheet => dailyTimeSheet.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
                scheme.AddColumn("Team Member", dailyTimeSheet => dailyTimeSheet.Employee.Name);
                scheme.AddColumn("Projects", dailyTimeSheet => dailyTimeSheet.Project.Name);
                scheme.AddColumn("Categories", dailyTimeSheet => dailyTimeSheet.Category.Name);
                scheme.AddColumn("Description", dailyTimeSheet => dailyTimeSheet.Description);
                scheme.AddColumn("Time", dailyTimeSheet => dailyTimeSheet.Time + dailyTimeSheet.Overtime);
            });
            File.WriteAllBytes(@"..\Report\report.pdf", pdf);
            return pdf;
        }

        public void GenerateExcel(List<DailyTimeSheet> dailyTimeSheets)
        {
            byte[] excel = dailyTimeSheets.ToExcel(scheme =>
            {
                scheme.AddColumn("Date", dailyTimeSheet => dailyTimeSheet.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
                scheme.AddColumn("Team Member", dailyTimeSheet => dailyTimeSheet.Employee.Name);
                scheme.AddColumn("Projects", dailyTimeSheet => dailyTimeSheet.Project.Name);
                scheme.AddColumn("Categories", dailyTimeSheet => dailyTimeSheet.Category.Name);
                scheme.AddColumn("Description", dailyTimeSheet => dailyTimeSheet.Description);
                scheme.AddColumn("Time", dailyTimeSheet => dailyTimeSheet.Time + dailyTimeSheet.Overtime);
            });
            File.WriteAllBytes(@"..\Report\report.xlsx", excel);
        }

        public void PrintPdf()
        {
            PrintJob printJob = new PrintJob(Printer.Default, @"..\Report\report.pdf");
            printJob.Print();
        }
    }
}
