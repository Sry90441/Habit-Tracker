using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Avalonia.Rendering.Composition;
using Habbit_Track_3_Meilenstein;
using System.Linq;

public class Tracker
{
    public List<ActivityItem> CheckIns { get; set; } = new List<ActivityItem>();
    public ActivityItem Activity { get; set; }
    public string TrackerName { get; set; }
    public int CheckedIn { get; set; }
    public int PartiallyChecked { get; set; }
    public int NotChecked { get; set; }
    public int TotalCheckIns { get; set; }

    public Tracker() { }

    public Tracker(string trackerName, ActivityItem activity)
    {
        TrackerName = trackerName;
        Activity = activity;
        CheckIns = new List<ActivityItem>();
    }

    public void AddToList(ActivityItem expiredActivity)
    {
            ActivityItem expired = expiredActivity.Clone();
            CheckIns.Add(expired);
            TotalCheckIns++;
            Tracking();
            Console.WriteLine("Tracking: " + TrackerName + " Not checked: " + NotChecked + " checked: " + CheckedIn +  "partially " + PartiallyChecked);
        

    }
    public void Tracking(int displayProgressOverTimeSpan = 0)
    {
        
        CheckedIn = 0;
        PartiallyChecked = 0;
        NotChecked = 0; 
        System.Console.WriteLine("Start Tracking");
        foreach (ActivityItem element in CheckIns)
        {
            Console.WriteLine("Tracking Tracking Tracking");
            switch (element.TaskDone)
            {
                case 10: CheckedIn++; break;
                case 5: PartiallyChecked++; break;
                case 0: NotChecked++; break;
            }
        }
    }
}