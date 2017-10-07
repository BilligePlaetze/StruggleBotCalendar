
using System;
using System.Collections.Generic;
using Ical.Net;

namespace StruggleApplication.api
{
    public interface ICalendarLibrary
    {
        List<CalendarEvent> GetEventsForDate(DateTime date);
    }
}