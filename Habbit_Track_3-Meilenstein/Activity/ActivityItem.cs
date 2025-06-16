using System;
using System.Collections;
using System.Text.Json.Serialization;

public class ActivityItem : IActivity
{
    public string ActivityName { get; set; }
    public int TaskDone { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime LastCheckInDate { get; set; }
    Tracker activityTracker;

    [JsonIgnore]
    public ActivityBase TimeInterval { get; set; }
    public DateTime DueDate { get; set; }
    // saving TimeInterval as string for Json
    public string TimeIntervalType
    {
        get { return TimeInterval.GetType().Name; }
        set { TimeInterval = LoadTimeIntervalFromString(value); }
    }
    public ActivityItem()
    {
        TaskDone = 0;
        DateStart = DateTime.Now;
    }
    public ActivityItem(string activityName, ActivityBase timeInterval)
    {
        TaskDone = 0;
        ActivityName = activityName;
        TimeInterval = timeInterval;
        DateStart = DateTime.Now; // <-- Date ergänzen
        DueDate = WhenNeedToCheck();
        Console.WriteLine(DueDate);
    }

    public ActivityBase LoadTimeIntervalFromString(string type)
    {
        switch (type)
        {
            case "DailyActivity":
                return new DailyActivity();

            case "WeeklyActivity":
                return new WeeklyActivity();

            case "MonthlyActivity":
                return new MonthlyActivity();
        }
        throw new ArgumentException("Type not found");
    }

    // If the task is completed this time is set 
    public void CheckedInOnTime()
    {
        DateStart = this.WhenNeedToCheck();
    }

    // Calculates the next time, the task needs to be done
    public DateTime WhenNeedToCheck()
    {
        return TimeInterval.GetDueDate(DateStart);
    }
    
    public ActivityItem Clone()
    {
        return new ActivityItem(this.ActivityName, this.TimeInterval) // oder wie du das intern speicherst
        {
            DateStart = this.DateStart,
            TaskDone = this.TaskDone
        };
    }

}
