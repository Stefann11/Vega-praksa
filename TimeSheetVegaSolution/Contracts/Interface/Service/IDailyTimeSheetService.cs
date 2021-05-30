using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;

namespace Contracts.Interface.Service
{
    public interface IDailyTimeSheetService
    {
        DailyTimeSheetDto Save(DailyTimeSheet dailyTimeSheet, int employeeId);
        DailyTimeSheetDto Edit(DailyTimeSheet dailyTimeSheet, int employeeId);
        IEnumerable<DailyTimeSheetDto> GetAll();
        DailyTimeSheetDto GetById(int id);
        DailyTimeSheetDto Delete(DailyTimeSheet dailyTimeSheet);
        IEnumerable<DailyTimeSheetDto> GetAllByDate(DateTime date, int employeeId);
        List<CalendarItemDto> GetCalendarItems(DateTime date, int employeeId);
        IEnumerable<DailyTimeSheetDto> GetAllBySearch(SearchDto searchDto);
        IEnumerable<DailyTimeSheetDto> GetByEmployee(SearchDto searchDto);
    }
}
