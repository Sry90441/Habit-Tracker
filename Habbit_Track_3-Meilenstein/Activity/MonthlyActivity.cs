using System;

class MonthlyActivity : Activity, IActivity
{

    const int _time = 28;
    DateTime _dueDate;
    public MonthlyActivity(string name) : base(name)
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