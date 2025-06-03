using System;

class SpecialTimeActivity : Activity, IActivity
{
    public SpecialTimeActivity(string name, int count) : base(name)
    {

    }
    public void CheckedInOnTime()
    {
        
    }
    public DateTime WhenNeedToCheck()
    {
        return DateTime.Now;
    }    
}