using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace StruggleApplication.framework
{
    // TODO implement methods needed by GoogleCalendarInstance
    public class GoogleClient
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/struggle-bud.json
        private static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        private static readonly string ApplicationName = "StruggleBud";
        private static readonly string CalendarId = "primary";

        private CalendarService service;

        public void SendAuthenticationRequest()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("google-api-secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/struggle-bud.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            
            // Create Google Calendar API service.
            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public List<Event> getEventsRequest(DateTime timeMin, bool forDate)
        {
            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List(CalendarId);
            if (!forDate)
            {
                timeMin = DateTime.Now;
            }
            else
            {
                request.TimeMax = timeMin.AddDays(1);
            }
            
            request.TimeMin = timeMin;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            
            // List events.
            Events events = request.Execute();
            return events.Items.ToList();
        }

        public void sendInsertRequest(Event newEvent)
        {
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, CalendarId);
            Event createdEvent = request.Execute();
            Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);
        }

        public void deleteEventRequest(string guid)
        {
            service.Events.Delete(CalendarId, guid).Execute();
        }
    }
}