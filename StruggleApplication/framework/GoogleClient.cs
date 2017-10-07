using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
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

                AuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = "411401250926-e5gu2e7ff7ickohmnv06vcjiv7dtio54.apps.googleusercontent.com",
                        ClientSecret = "XqCJM1CAX_eWPp00HNrT5g7Y"
                    },
                    Scopes = new[] { CalendarService.Scope.Calendar },
                    DataStore = new FileDataStore(credPath, true)
                });
                
                TokenResponse token = new TokenResponse();
                token.AccessToken = "ya29.GlvdBKJ08MAKTkaVTw63egRtyaEfZDGHveJ8PS1eAF8cP9OpvlnvzEnNi7UWhWqIgyPJbQ0ELL3NND2UZGhGNNDod8GiSwcnTOGCtYHGuQvPKBrNpMlnt0Jo0RhH";
                token.RefreshToken = "1/wRLwL6fWjaB8BYZ7xzaYVHAAN4H6yuJAAk11uDdgVBU";
                token.ExpiresInSeconds = 3600;
                token.TokenType = "Bearer";
               
                credential = new UserCredential(flow, "d.seledtsova@gmail.com", token);
                
                /*
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                    */
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