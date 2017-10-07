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
            createEventTest(instance);
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
    }
}