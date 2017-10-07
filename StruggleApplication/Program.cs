using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.Serialization.iCalendar.Serializers;
using StruggleApplication.api;
using StruggleApplication.framework;
using Calendar = Ical.Net.Calendar;

namespace StruggleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Put testcode here
            GoogleClient client = new GoogleClient();
            client.authenticate();
            client.getEvents();
        }
    }
}