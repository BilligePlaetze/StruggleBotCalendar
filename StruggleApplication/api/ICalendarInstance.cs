
using System;
using System.Collections.Generic;
using Ical.Net;

namespace StruggleApplication.api
{
    public interface ICalendarInstance
    {
        Calendar GetCalendar(Uri uri);
        List<CalendarEvent> GetEventsForDate(DateTime date);

        void CreateCalendar(Calendar calendar);
        void CreateEvent(CalendarEvent e);

        void DeleteCalendar(String guid);
        void DeleteEvent(String guid);
    }
}