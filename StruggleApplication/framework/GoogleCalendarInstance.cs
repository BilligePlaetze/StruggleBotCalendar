using System;
using System.Collections.Generic;
using Ical.Net;
using StruggleApplication.api;

namespace StruggleApplication.framework
{
    
    // TODO use _googleClient for implementation
    
    public class GoogleCalendarInstance : ICalendarInstance
    {
        private GoogleClient _googleClient;

        public void Initialize()
        {
            _googleClient.Authenticate();
        }
        
        public Calendar GetCalendar(Uri uri)
        {
            throw new NotImplementedException();
        }

        public List<CalendarEvent> GetEventsForDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void CreateCalendar(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public void CreateEvent(CalendarEvent e)
        {
            throw new NotImplementedException();
        }

        public void DeleteCalendar(string guid)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(string guid)
        {
            throw new NotImplementedException();
        }
    }
}