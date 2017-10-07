using System;
using System.Collections.Generic;
using Google.Apis.Calendar.v3.Data;
using StruggleApplication.api;
using StruggleApplication.framework;

namespace StruggleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO Put testcode here
            ICalendarInstance instance = new GoogleCalendarInstance();
            instance.Initialize();
        }


        private static void createEventTest(ICalendarInstance instance)
        {
            var start = new EventDateTime()
            {
                DateTime = DateTime.Parse("2018-05-28T09:00:00-07:00"),
                TimeZone = "America/Los_Angeles",
            };

            var end = new EventDateTime()
            {
                DateTime = DateTime.Parse("2018-05-28T17:00:00-07:00"),
                TimeZone = "America/Los_Angeles",
            };

            instance.CreateEvent("Hackathon easy gewinnen", "Weil Billige Plätze", start, end);

        }

        private static void DeleteEventTest(ICalendarInstance instance)
        {
            List<Event> eventsOct = instance.GetEventsForDate(new DateTime(2017, 10, 8));
            String summary = "Tea at the Ritz";
            String guid = "";

            foreach (var e in eventsOct)
            {
                if (e.Summary.Equals(summary))
                {
                    guid = e.Id;
                }
                Console.WriteLine(e.Summary);
            }

            instance.DeleteEvent(guid);
            eventsOct = instance.GetEventsForDate(new DateTime(2017, 10, 8));
            PrintLoop(eventsOct);

            void PrintLoop(List<Event> events)
            {
                foreach (var e in events)
                {
                    Console.WriteLine(e.Summary);
                }
            }
        }
    }
}