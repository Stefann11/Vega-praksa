using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class DailyTimeSheetService : IDailyTimeSheetService
    {
        private readonly IDailyTimeSheetRepository _dailyTimeSheetRepository;

        public DailyTimeSheetService(IDailyTimeSheetRepository dailyTimeSheetRepository)
        {
            _dailyTimeSheetRepository = dailyTimeSheetRepository;
        }

        public IEnumerable<DailyTimeSheetDto> GetAll()
        {
            return _dailyTimeSheetRepository.GetAll()
                .Select(dailyTimeSheet => new DailyTimeSheetDto(dailyTimeSheet)).ToList();
        }

        public IEnumerable<DailyTimeSheetDto> GetAllByDate(DateTime date, int employeeId)
        {
            return _dailyTimeSheetRepository.GetAllByDate(date, employeeId)
                .Select(dailyTimeSheet => new DailyTimeSheetDto(dailyTimeSheet)).ToList();
        }

        public List<CalendarItemDto> GetCalendarItems(DateTime date, int employeeId)
        {
            List<DateTime> dates = GetAllDates(date);

            List<CalendarItemDto> calendarItemDtos = GetCreatedCalendarItems(dates, employeeId);
            
            return CreateMonthlyCalendarItems(dates, calendarItemDtos);
        }

        public DailyTimeSheetDto GetById(int id)
        {
            return new DailyTimeSheetDto(_dailyTimeSheetRepository.GetById(id));
        }

        public DailyTimeSheetDto Save(DailyTimeSheet dailyTimeSheet, int employeeId)
        {
            return new DailyTimeSheetDto(_dailyTimeSheetRepository.Save(dailyTimeSheet, employeeId));
        }

        public DailyTimeSheetDto Edit(DailyTimeSheet dailyTimeSheet, int employeeId)
        {
            return new DailyTimeSheetDto(_dailyTimeSheetRepository.Edit(dailyTimeSheet, employeeId));
        }

        public DailyTimeSheetDto Delete(DailyTimeSheet dailyTimeSheet)
        {
            return new DailyTimeSheetDto(_dailyTimeSheetRepository.Delete(dailyTimeSheet));
        }

        private List<DateTime> GetAllDates(DateTime date)
        {
            List<DateTime> datesInThisMonth = GetDatesInThisMonth(date.Month, date.Year);
            int dayOfWeek = datesInThisMonth.FirstOrDefault().DayOfWeek == 0
                ? 7 : (int)datesInThisMonth.FirstOrDefault().DayOfWeek - 1;
            List<DateTime> datesFromLastMonth = GetDatesFromLastMonth(dayOfWeek, date.Month, date.Year);

            int numberOfFutureDates = 42 - datesInThisMonth.Count - datesFromLastMonth.Count;
            List<DateTime> datesFromNextMonth = GetDatesFromNextMonth(numberOfFutureDates, date.Month, date.Year);

            return datesFromLastMonth.Concat(datesInThisMonth).Concat(datesFromNextMonth).ToList();
        }

        private List<CalendarItemDto> CreateMonthlyCalendarItems(List<DateTime> dates, List<CalendarItemDto> calendarItemDtos)
        {
            return dates.Select(dateTime => calendarItemDtos
            .Any(calendarItem => calendarItem.Date.Date == dateTime.Date) ?
            calendarItemDtos.FirstOrDefault(calendarItem => calendarItem.Date.Date == dateTime.Date) :
            new CalendarItemDto
            {
                Date = dateTime,
                TotalHours = 0
            }).ToList();
        }

        private List<CalendarItemDto> GetCreatedCalendarItems(List<DateTime> dates, int employeeId)
        {
            return _dailyTimeSheetRepository
                    .GetAllBetweenDates(dates.FirstOrDefault(), dates.LastOrDefault(), employeeId)
                    .GroupBy(DailyTimeSheet => DailyTimeSheet.Date)
                    .Select(calendarItemDto => new CalendarItemDto
                    {
                        Date = calendarItemDto.First().Date,
                        TotalHours = calendarItemDto.Sum(c => c.Time + c.Overtime),
                    }).ToList();
        }

        private List<DateTime> GetDatesInThisMonth(int month, int year)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                             .Select(day => new DateTime(year, month, day))
                             .ToList();
        }

        private List<DateTime> GetDatesFromLastMonth(int numberOfDays, int month, int year)
        {
            return Enumerable.Range(1, numberOfDays)
                            .Select(day => new DateTime(year, month, 1).AddDays(-day)).Reverse()
                            .ToList();
        }

        private List<DateTime> GetDatesFromNextMonth(int numberOfDays, int month, int year)
        {
            if (month == 12)
            {
                month = 1;
                year++;
            } else
            {
                month++;
            }
            return Enumerable.Range(1, numberOfDays)
                             .Select(day => new DateTime(year, month, day)).ToList();
        }

        public IEnumerable<DailyTimeSheetDto> GetAllBySearch(SearchDto searchDto)
        {
            return _dailyTimeSheetRepository.GetAllBySearch(searchDto)
                .Select(dailyTimeSheet => new DailyTimeSheetDto(dailyTimeSheet)).ToList();
        }

        public IEnumerable<DailyTimeSheetDto> GetByEmployee(SearchDto searchDto)
        {
            return _dailyTimeSheetRepository.GetByEmployee(searchDto)
                .Select(dailyTimeSheet => new DailyTimeSheetDto(dailyTimeSheet)).ToList();
        }
    }
}
