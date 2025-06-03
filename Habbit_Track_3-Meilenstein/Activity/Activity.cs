using System;
using Avalonia.Diagnostics;
using Microsoft.VisualBasic;
using Tmds.DBus.Protocol;

class Activity
{
    public string ActivityName { get; set; }
    public int TaskDone { get; set; }
    public DateTime DateStart { get; set; }
    public Activity(string activityName)
    {
        TaskDone = 0;
        ActivityName = activityName;
        DateStart = DateTime.Now.Date;

    }
}