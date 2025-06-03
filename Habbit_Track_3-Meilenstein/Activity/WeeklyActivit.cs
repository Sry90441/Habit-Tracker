using System;

class WeeklyActivity : Activity, IActivity
{
    const int _time = 7;
    DateTime _dueDate;
    public WeeklyActivity(string name) : base(name)
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