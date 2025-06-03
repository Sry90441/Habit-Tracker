using System;
using Avalonia.Automation;

class DailyActivity : Activity, IActivity
{
    const int _time = 1;
    DateTime _dueDate;
    public DailyActivity(string name) : base(name)
    {
        ActivityName = name;
        TaskDone = 0;
    }
    public void CheckedInOnTime()
    {
            
    }
    public DateTime WhenNeedToCheck()
    {
        _dueDate = DateStart.AddDays(_time);
        return _dueDate;
    }
}