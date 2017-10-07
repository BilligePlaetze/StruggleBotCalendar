using Google.Apis.Calendar.v3.Data;
using StruggleApplication.api;
using StruggleApplication.framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StruggleApplication.domain
{
    class Plan
    {

        private ICalendarInstance _instance;

        private int _pomodoroTimeLearing;
        private int _pomodoroTimeBreak;

        public Plan()
        {
            this._instance = new GoogleCalendarInstance();
            this._instance.Initialize();

            this._pomodoroTimeLearing = 50; // Minutes
            this._pomodoroTimeBreak = 10; // Minutes

        }


        


        public TimeSpan CalculateAvailableTime(List<Event> events, DateTime from, DateTime to)
        {
            TimeSpan time = from.Subtract(to);

            // TODO: overlapping events
            // ATM: The calculation is wrong if the input events are overlapping

            foreach (Object obj in events)
            {
                Event currentEvent = (Event) obj;
                DateTime eventstart = currentEvent.Start.DateTime.Value;
                DateTime eventEnd = currentEvent.End.DateTime.Value;
                time = time.Subtract(eventEnd.Subtract(eventstart));
            }

            return time;
        }

        public int GetNumberOfDays(DateTime from, DateTime to)
        {
            double diff = to.Subtract(from).TotalDays;
            int numberOfDays = (int)Math.Ceiling(diff) + 1;

            return numberOfDays;
        }

        public int GetNumberOfWeeks(DateTime from, DateTime to)
        {
            double diff = to.AddDays(-1).Subtract(from).TotalDays;
            return (int)Math.Ceiling(diff / 7);
        }

        public void AddPomodoro(DateTime startDateTime, String titleOfExam)
        {
            // learn
            AddEvent(startDateTime, this._pomodoroTimeLearing, "Lernen", titleOfExam);
            // break
            startDateTime = startDateTime.AddMinutes(this._pomodoroTimeLearing);
            AddEvent(startDateTime, this._pomodoroTimeBreak, "Pause", "");
        }

        private void AddEvent(DateTime startDateTime, int durationMinutes, String title, String desc)
        {
            EventDateTime eventStart = new EventDateTime();
            EventDateTime eventEnd = new EventDateTime();

            eventStart.DateTime = startDateTime;
            eventEnd.DateTime = eventStart.DateTime.Value.AddMinutes(durationMinutes);

            // create event
            this._instance.CreateEvent(title, desc, eventStart, eventEnd);
        }


    }
}
