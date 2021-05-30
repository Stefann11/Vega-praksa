using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;

namespace Contracts.Interface.Repository
{
    public interface IDailyTimeSheetRepository
    {
        DailyTimeSheet Save(DailyTimeSheet obj, int employeeId);
        DailyTimeSheet Edit(DailyTimeSheet obj, int employeeId);
        IEnumerable<DailyTimeSheet> GetAll();
        DailyTimeSheet GetById(int id);
        DailyTimeSheet Delete(DailyTimeSheet dailyTimeSheet);
        IEnumerable<DailyTimeSheet> GetAllByDate(DateTime date, int employeeId);
        IEnumerable<DailyTimeSheet> GetAllBetweenDates(DateTime startDate, DateTime endDate, int employeeId);
        IEnumerable<DailyTimeSheet> GetAllBySearch(SearchDto searchDto);
        IEnumerable<DailyTimeSheet> GetByEmployee(SearchDto searchDto);
    }
}
