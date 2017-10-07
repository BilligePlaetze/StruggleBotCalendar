using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        static void Main(string[] args)
        {
            /*
            IClientConfiguration config = new ClientConfiguration
            {
                IsEnabled = true,
                ClientId = "568573738857-daojp1d40q0fpknf6k7iq4v1imfsp98b.apps.googleusercontent.com",
                ClientSecret = "THaVfmWKg-CpB5_n8ItBiNQe",
                RedirectUri = "http://localhost",
                Scope = "https://www.googleapis.com/auth/calendar"
            };
            OAuth2.Client.Impl.GoogleClient client = new OAuth2.Client.Impl.GoogleClient(
                new RequestFactory(), config);
            Console.WriteLine(client.GetLoginLinkUri());
            Console.ReadLine();
            Console.WriteLine(client.AccessToken);
            */
            // TODO Put testcode here
            ICalendarInstance instance = new GoogleCalendarInstance();
            instance.Initialize();
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