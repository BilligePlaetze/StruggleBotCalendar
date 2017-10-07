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