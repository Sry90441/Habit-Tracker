using System;

public abstract class ActivityBase
{
    public abstract DateTime GetDueDate(DateTime due);
}

public class DailyActivity : ActivityBase
{
    public override DateTime GetDueDate(DateTime due)
    {
        return due.AddSeconds(10);
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
