using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Rendering.Composition;
using Habbit_Track_3_Meilenstein;

class Tracker
{
    ActivityBase _trackedActivity;
    List<int> _progressionInNumbers;
    List<ActivityItem> _checkIn_s;
    int _averageForTrack;
    public Tracker(ActivityBase trackedActivity)
    {
        _checkIn_s = new List<ActivityItem>();
        _progressionInNumbers = new List<int>();
        _trackedActivity = trackedActivity;
    }
    public void AddProgres(ActivityItem progressionActivity)
    {
        _checkIn_s.Add(progressionActivity);
    }
    public void TrackingProgress()
    {
        double count = 0;
        foreach (ActivityItem item in _checkIn_s)
        {
            count++;
        }
       
    }
    public void TrackedInXP()
    {

    }
    /*
    Nico Notes & ToDo's:

    - zeitlicher abgleich und übergabe von Zeitwert
    → 7 Tage, 2 Wochen, 1 Monat (4 Wochen), 3 Monate, Für immer()
    dadurch ermitteln von mittel und ausgabe als diagram in form der Werte

    → XP Verteilung    

    Overall:
    
    → Beim schließen ungespeichert abfragen

    */

}