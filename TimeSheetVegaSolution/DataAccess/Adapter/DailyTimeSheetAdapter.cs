using DataAccess.Adaptee;
using DataAccess.Target;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Adapter
{
    public class DailyTimeSheetAdapter : ITarget
    {
        private readonly DailyTimeSheetAdaptee dailyTimeSheetAdaptee;
        public DailyTimeSheetAdapter(DailyTimeSheetAdaptee dailyTimeSheetAdaptee)
        {
            this.dailyTimeSheetAdaptee = dailyTimeSheetAdaptee;
        }
        public object ConvertSql(DataRow dataRow)
        {
            return dailyTimeSheetAdaptee.ConvertSqlDataReaderToDailyTimeSheet(dataRow);
        }
    }
}
