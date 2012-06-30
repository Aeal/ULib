using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Calendar 
{
    public int DaysInWeek
    {
        get { return Days.Count; }
    }
    public int DaysInYear
    {
        get
        {
            return Months.Sum(m => m.DaysInMonth);
        }
    }
    public int NumMonths
    {
        get { return Months.Count; }
    }
    List<Month> Months;
    List<Day>   Days;
    private int numMonths, numDays;

}
[Serializable]
public class Month
{
    int numDaysInMonth;
    public int DaysInMonth
    {
        get { return numDaysInMonth;  }
        set { numDaysInMonth = value; }
    }
    string monthName;
    public string Name
    {
        get { return monthName;  }
        set { monthName = value; }
    }
}

[Serializable]
public class Day
{
    static int hoursInDay = 24;
    public static int HoursInDay
    {
        get { return hoursInDay;  }
        set { hoursInDay = value; }
    }
    string name;
}
