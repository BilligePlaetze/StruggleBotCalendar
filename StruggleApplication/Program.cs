using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using StruggleApplication.api;
using StruggleApplication.framework;
using GoogleClient = OAuth2.Client.Impl.GoogleClient;

namespace StruggleApplication
{
    class Program
    {
        static  void Main(string[] args)
        {

         DoWork().Wait();
        }

        static async Task  DoWork()
        {
            // TODO Put testcode here
            ICalendarInstance instance = new GoogleCalendarInstance();
//            Console.WriteLine(OAuthoriser.GenerateLink());
            String code = Console.ReadLine();
            await instance.Initialize(code);
            instance.GetEventsForDate(new DateTime(2017, 12, 27));
            
            Console.WriteLine(instance.GetEventsForDate(new DateTime(2017, 12, 27)).First().Summary);
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