using System;
using System.Collections.Generic;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.Serialization.iCalendar.Serializers;

namespace StruggleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var now = DateTime.Now;
            var later = now.AddHours(1);

//Repeat daily for 5 days
            var rrule = new RecurrencePattern(FrequencyType.Daily, 1) { Count = 5 };

            var e = new CalendarEvent
            {
                DtStart = new CalDateTime(now),
                DtEnd = new CalDateTime(later),
                RecurrenceRules = new List<RecurrencePattern> { rrule },
            };

            var calendar = new Calendar();
            calendar.Events.Add(e);

            var serializer = new CalendarSerializer(new SerializationContext());
            var serializedCalendar = serializer.SerializeToString(calendar);
            Console.Write(serializedCalendar);
        }
    }
}