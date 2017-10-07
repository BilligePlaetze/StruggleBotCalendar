
using System;
using System.Collections.Generic;
using Ical.Net;

namespace StruggleApplication.api
{
    public interface ICalendarInstance
    {
        void Initialize();
        
        Calendar GetCalendar();
        List<CalendarEvent> GetEventsForDate(DateTime date);

        void CreateCalendar(Calendar calendar);
        void CreateEvent(CalendarEvent e);

        void DeleteCalendar(String guid);
        void DeleteEvent(String guid);
    }
}