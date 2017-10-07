using System;
using System.Collections.Generic;
using Ical.Net;
using StruggleApplication.api;

namespace StruggleApplication.framework
{
    
    // TODO use _googleClient for implementation
    
    public class GoogleCalendarInstance : ICalendarInstance
    {
        private GoogleClient _googleClient = new GoogleClient();

        public void Initialize()
        {
            _googleClient.Authenticate();
        }
        
        public Calendar GetCalendar()
        {
            _googleClient.getEvents(DateTime.Now, false);
            return null;
        }

        public List<CalendarEvent> GetEventsForDate(DateTime date)
        {
            _googleClient.getEvents(date, true);
            return null;
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