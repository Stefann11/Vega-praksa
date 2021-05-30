using Model.Dtos;
using Model.Entities;
using System.Collections.Generic;

namespace Contracts.Interface.Service
{
    public interface IReportService
    {
        public byte[] GeneratePdf(List<DailyTimeSheet> dailyTimeSheets);
        public void GenerateExcel(List<DailyTimeSheet> dailyTimeSheets);
        public void PrintPdf();
    }
}
