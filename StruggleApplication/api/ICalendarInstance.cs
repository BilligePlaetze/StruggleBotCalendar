
using System;
using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;
using Ical.Net;
using Calendar = Ical.Net.Calendar;

namespace StruggleApplication.api
{
    public interface ICalendarInstance
    {
        void Initialize();
        
        List<Event> GetCalendar();
        List<Event> GetEventsForDate(DateTime date);

        void CreateCalendar(Calendar calendar);
        void CreateEvent(String title, String description, EventDateTime startTime, EventDateTime endTime);
        void DeleteCalendar(String guid);
        void DeleteEvent(String guid);
    }
}