using System;

public class ActivityItem : IActivity
{
    public string ActivityName { get; set; }
    public int TaskDone { get; set; }
    public DateTime DateStart { get; set; }
    public ActivityBase TimeInterval { get; set; }
    public ActivityItem(string activityName, ActivityBase timeInterval)
    {
        TaskDone = 0;
        ActivityName = activityName;
        TimeInterval = timeInterval;
        DateStart = DateTime.Now.Date;

    }

    // If the task is completed this time is set 
    public void CheckedInOnTime()
    {
        DateStart = DateTime.Now;
    }

    // Calculates the next time, the task needs to be done
    public DateTime WhenNeedToCheck()
    {
        return TimeInterval.GetDueDate(DateStart);
    }
}
