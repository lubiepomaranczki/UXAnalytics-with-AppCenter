using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AppCenter.Analytics;
using XamForms.Enhanced.ViewModels;

namespace UXAnalytics
{
    public interface ILogger<T> where T : BaseViewModel
    {
        /// <summary>
        /// Log event with page name
        /// </summary>
        /// <param name="properties"></param>
        void LogEvent(Dictionary<string, string> properties = null);

        /// <summary>
        /// Start logging time on page
        /// </summary>
        void StartLogging();

        /// <summary>
        /// End logging time on page and log to AppCenter Analytics
        /// </summary>
        void EndLogging();
    }

    public class Logger<T> : Logger, ILogger<T> where T : BaseViewModel
    { 
        private Stopwatch stopWatch;

        public void LogEvent(Dictionary<string, string> properties = null)
        {
            var name = GetSectionName();

            if (properties != null)
            {
                Analytics.TrackEvent(name, properties);
                return;
            }

            Analytics.TrackEvent(name);
        }

        public void StartLogging()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public void EndLogging()
        {
            stopWatch.Stop();

            var timeElapsed = stopWatch.Elapsed.TotalMinutes;
            var timeToLog = string.Empty;
            if (timeElapsed < 1)
            {
                timeToLog = "Under 1 minute";
            }
            if (timeElapsed >= 1 && timeElapsed < 5)
            {
                timeToLog = "1-5 minutes";
            }
            if (timeElapsed >= 5 && timeElapsed < 10)
            {
                timeToLog = "5-10 minutes";
            }
            if (timeElapsed >= 10)
            {
                timeToLog = "Above 10 minutes";
            }

            LogEvent(new Dictionary<string, string> { { "TimeSpent on page", timeToLog } });
        }

        private string GetSectionName()
        {
            if (_sections.TryGetValue(typeof(T), out var name))
            {
                return name;
            }

            return typeof(T).Name;
        }
    }

    public class Logger
    {
        protected static Dictionary<Type, string> _sections = new Dictionary<Type, string>
        {
            //You can either register your pages here by
            //{ typeof(MainPage), "Test page" },
        };

        /// <summary>
        /// Register page for Analytics
        /// </summary>
        /// <param name="pageType">Type of page to register</param>
        /// <param name="pageName">Name of page to be shown in AppCenter Analytics</param>
        public static void RegisterPage(Type pageType, string pageName)
        {
            _sections.Add(pageType, pageName);
        }
    }
}
