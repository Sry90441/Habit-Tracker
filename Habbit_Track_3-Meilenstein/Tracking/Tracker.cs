using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Avalonia.Rendering.Composition;
using Habbit_Track_3_Meilenstein;

public class Tracker
{
    public List<ActivityItem> CheckIns { get; set; } = new List<ActivityItem>();
    public ActivityItem Activity { get; set; }
    public string TrackerName { get; set; }
    public int CheckedIn { get; set; }
    public int PartiallyChecked { get; set; }
    public int NotChecked { get; set; }
    public int TotalCheckIns { get; set; }

    // Parameterloser Konstruktor notwendig für Deserialisierung
    public Tracker() {}

    public Tracker(string trackerName ,ActivityItem activity)
    {
        TrackerName = trackerName;
        Activity = activity;
        CheckIns = new List<ActivityItem>();
    }

    public void AddToList(ActivityItem expiredActivity)
    {
        CheckIns.Add(expiredActivity);
        Console.WriteLine("Tracking: " + TrackerName + " Not checked: " + NotChecked + " checked: " + CheckedIn);
        TotalCheckIns++;
    }

    public void Tracking(int displayProgressOverTimeSpan)
    {
        if (CheckedIn + PartiallyChecked + NotChecked != TotalCheckIns)
        {
            foreach (ActivityItem element in CheckIns)
            {
                if (element.TaskDone == 10)
                {
                    CheckedIn++;
                }
                else if (element.TaskDone == 5)
                {
                    NotChecked++;
                }
                else if (element.TaskDone == 0)
                {
                    TotalCheckIns++;
                }
            }
        } 
    }
}
