using System;
using System.Collections.Generic;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Services
{
    public interface IFlowService
    {
        List<TimeSpanByHourPageItem> GetTimeSpanByHourList(string domain, DateTime day);

        List<TimeSpanByDayPageItem> GetTimeSpanByDayList(string domain, DateTime beginDate, DateTime endDate);

        List<PredictTimeSpanByHourPageItem> GetPredictTimeSpanByHourList(string domain, DateTime day);

        List<TimeSpanByMinutePageItem> GetTimeSpanByMinuteList(string domain, int leftMinute);
    }
}
