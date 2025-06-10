using System.Collections.Generic;
using Habbit_Track_3_Meilenstein;

class Tracker
{
    ActivityBase _trackedActivity;
    List<int> _progressionInNumbers;
    ActivityList<ActivityItem> _checkIn_s;
    public Tracker(ActivityBase trackedActivity)
    {
        _checkIn_s = new ActivityList<ActivityItem>();
        _progressionInNumbers = new List<int>();
        _trackedActivity = trackedActivity;
    }
    public void AddProgres(ActivityItem progressionActivity)
    {
        _checkIn_s.Add(progressionActivity);
    }
    public void TrackingProgress()
    {

    }

    /*
    
    */

}