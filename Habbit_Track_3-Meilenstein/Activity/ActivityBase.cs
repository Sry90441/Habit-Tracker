using System;
using Avalonia.Diagnostics;
using Microsoft.VisualBasic;
using Tmds.DBus.Protocol;

public abstract class ActivityBase
{
    public abstract DateTime GetDueDate(DateTime due);
}

public class DailyActivity : ActivityBase
{
    public override DateTime GetDueDate(DateTime due)
    {
        return due.AddDays(1);
    }
}

public class WeeklyActivity : ActivityBase
{
    public override DateTime GetDueDate(DateTime due)
    {
        return due.AddDays(7);
    }
}

public class MonthlyActivity : ActivityBase
{
    public override DateTime GetDueDate(DateTime due)
    {
        return due.AddMonths(1);
    }
}