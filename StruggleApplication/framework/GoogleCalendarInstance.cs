using System;
using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;
using Ical.Net;
using StruggleApplication.api;
using Calendar = Ical.Net.Calendar;

namespace StruggleApplication.framework
{
    
    // TODO use _googleClient for implementation
    
    public class GoogleCalendarInstance : ICalendarInstance
    {
        private GoogleClient _googleClient = new GoogleClient();

        public void Initialize()
        {
            _googleClient.SendAuthenticationRequest();
        }
        
        public List<Event> GetCalendar()
        {
            return _googleClient.getEventsRequest(DateTime.Now, false);
        }

        public List<Event> GetEventsForDate(DateTime date)
        {
            return _googleClient.getEventsRequest(date, true);
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