using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Habbit_Track_3_Meilenstein;

class ActivityList<T>
{
    private List<T> _activities = new List<T>();
    public void Add(T activity)
    {
        _activities.Add(activity);
    }
    public T Get(int index)
    {
        return _activities[index];
    }

    public int Count => _activities.Count;
}

/*

    Wenn done 
    --> dann verschwindet es aus der Anzeige
    --> wenn gelöscht verschwindet aus anzeige und lsite
*/