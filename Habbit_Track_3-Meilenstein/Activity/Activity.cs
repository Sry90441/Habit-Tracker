using System;
using Avalonia.Diagnostics;

class Activity
{
    public string ActivityName { get; set; }
    public int TaskDone { get; set; }
    public DateTime Date { get; set; }
    public Activity(string activityName)
    {
        TaskDone = 0;
        ActivityName = activityName;
        Date = DateTime.Now;
    }
    
    
    
}