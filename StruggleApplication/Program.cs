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
    }
}