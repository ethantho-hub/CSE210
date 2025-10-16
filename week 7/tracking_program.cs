using System;
using System.Collections.Generic;

// Base class
public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    // Abstract methods (must be overridden in derived classes)
    public abstract double GetDistance(); // in miles or km
    public abstract double GetSpeed();    // in mph or kph
    public abstract double GetPace();     // in min per mile or km

    // Summary method (uses other methods)
    public virtual string GetSummary()
    {
        return $"{Date.ToShortDateString()} {GetType().Name} ({Minutes} min): " +
               $"Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}

// Derived class 1: Running
public class Running : Activity
{
    private double _distance; // miles

    public Running(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;
    public override double GetSpeed() => (_distance / Minutes) * 60;
    public override double GetPace() => Minutes / _distance;
}

// Derived class 2: Cycling
public class Cycling : Activity
{
    private double _speed; // mph

    public Cycling(DateTime date, int minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance() => _speed * (Minutes / 60.0);
    public override double GetSpeed() => _speed;
    public override double GetPace() => 60 / _speed;
}

// Derived class 3: Swimming
public class Swimming : Activity
{
    private int _laps; // each lap = 50 meters

    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double distanceInKm = (_laps * 50) / 1000.0;
        return distanceInKm * 0.62; // convert km to miles
    }

    public override double GetSpeed() => (GetDistance() / Minutes) * 60;
    public override double GetPace() => Minutes / GetDistance();
}

// Main program
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2025, 10, 15), 30, 3.0),
            new Cycling(new DateTime(2025, 10, 14), 45, 15.0),
            new Swimming(new DateTime(2025, 10, 13), 25, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
