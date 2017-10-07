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

        // TODO probably not needed for prototype
        public void CreateCalendar(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public void CreateEvent(String title)
        {
            Event newEvent = new Event()
            {
                Summary = "Google I/O 2015",
                Location = "800 Howard St., San Francisco, CA 94103",
                Description = "A chance to hear more about Google's developer products.",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2015-05-28T09:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2015-05-28T17:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                Attendees = new EventAttendee[] {
                    new EventAttendee() { Email = "lpage@example.com" },
                    new EventAttendee() { Email = "sbrin@example.com" },
                },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
                        new EventReminder() { Method = "email", Minutes = 24 * 60 },
                        new EventReminder() { Method = "sms", Minutes = 10 },
                    }
                }
            };

            _googleClient.sendInsertRequest(e);
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