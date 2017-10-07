using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ical.Net;
using StruggleApplication.api;
using StruggleApplication.framework;
using Google.Apis.Calendar.v3.Data;

namespace StruggleApplication.domain
{
    class Planner
    {
        private int _learingTimeHours;
        private int _learingTimeMinutes;
        private int _learingTimeSeconds;

        private int _pomodoroTime;
        private int _maximumPomodorosPerDay;

        private ICalendarInstance _instance;

        // 
        public Planner()
        {
            this._learingTimeHours = 8;
            this._learingTimeMinutes = 0;
            this._learingTimeSeconds = 0;

            this._pomodoroTime = 70;
            this._maximumPomodorosPerDay = 5;

            this._instance = new GoogleCalendarInstance();
            _instance.Initialize();
        }

        // 
        public void planExamPreparation(DateTime start, DateTime exam, String examTitle, int effort, int maxLearningHoursPerDay)
        {
            Console.WriteLine("Start: " + start);
            Console.WriteLine("Exam '" + examTitle + "': " + exam);
            Console.WriteLine("Effort: " + effort + "h");

            int numOfWeeks = GetNumberOfWeeks(start, exam);
            double estimatedEffort = effort * 60;
            double pomodorosPerDay = (effort * 1.2) / numOfWeeks;
            double carryOverPomodoros = 0;


            DateTime currentDay = exam.AddDays(-1);

            if (currentDay.DayOfWeek == DayOfWeek.Saturday)
                currentDay = exam.AddDays(-1);
            if (currentDay.DayOfWeek == DayOfWeek.Sunday)
                currentDay = exam.AddDays(-2);

            // plan one day
            TimeSpan availableTimeOfDay = GetAvailableTimeOfDay(currentDay);
            if (availableTimeOfDay.TotalMinutes > this._pomodoroTime)
            {
                int numberOfPomodoros = 0;
                while (numberOfPomodoros < pomodorosPerDay && numberOfPomodoros < this._maximumPomodorosPerDay)
                {
                    numberOfPomodoros++;
                    AddPomodoroToDay(currentDay);

                    availableTimeOfDay = GetAvailableTimeOfDay(currentDay);

                    if (availableTimeOfDay.TotalMinutes < this._pomodoroTime)
                    {
                        break;
                    }
                }//while
                carryOverPomodoros = pomodorosPerDay - numberOfPomodoros;
            }//if


        }


        // 
        private void AddPomodoroToDay(DateTime currentDay)
        {
            // TODO
            Console.WriteLine("Add a pomodoro meeting at: " + currentDay);
        }


        // 
        public int GetNumberOfWeeks(DateTime start, DateTime exam)
        {
            // Weeks(02.10.17, 09.10.17) = 1
            // Weeks(02.10.17, 03.10.17) = 0
            TimeSpan diff = exam.AddDays(-1).Subtract(start);
            return (int)Math.Ceiling(diff.TotalDays / 7);
        }

        // 
        public TimeSpan GetAvailableTimeOf(DateTime start, DateTime end)
        {
            TimeSpan availableTime = new TimeSpan(0, 0, 0);

            double dayDiff = end.Subtract(start).TotalDays;
            int numberOfDays = (int)Math.Ceiling(dayDiff) + 1;
            // Diff(08.10.17, 02.10.17) = 6 days ; 6 + 1 = 7 days (start and end inclusive)
            // Diff(05.10.17, 02.10.17) = 3 days ; 3 + 1 = 4 days (start and end inclusive)
            // ...

            // available time of start
            availableTime = availableTime.Add(GetAvailableTimeOfDay(start));

            // available time till end (inclusive)
            DateTime currentDay = start;
            for (int i = 1; i < numberOfDays; i++)
            {
                currentDay = currentDay.AddDays(1);
                availableTime = availableTime.Add(GetAvailableTimeOfDay(currentDay));
            }

            return availableTime;
        }

        // 
        public TimeSpan GetAvailableTimeOfDay(DateTime day)
        {
            TimeSpan availableTime = new TimeSpan(_learingTimeHours, _learingTimeMinutes, _learingTimeSeconds);
            List<Event> events = this._instance.GetEventsForDate(day);

            foreach (Object obj in events)
            {
                CalendarEvent currentEvent = (CalendarEvent)obj;
                availableTime = availableTime.Subtract(currentEvent.Duration);
            }

            if (availableTime.TotalMilliseconds < 0)
            {
                return new TimeSpan(0, 0, 0);
            }

            return availableTime;
        }






    }//class
}//namespace
