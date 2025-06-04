using System;

class SpecialTimeActivity : Activity, IActivity
{
    public SpecialTimeActivity(string name) : base(name)
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