using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR || RELEASE || DEBUG
using UnityEditor;
#endif


public class ChronosManager : MonoBehaviour
{
    internal TimeUnit gameUnit,
                      realTimeUnit;
    public TimeUnit GameUnit
    {
        get { return gameUnit;  }
        set { gameUnit = value; }
    }
    public TimeUnit RealTimeUnit
    {
        get { return realTimeUnit; }
        set { realTimeUnit = value; }
    }
    internal float  gameSecondToRealSecond;

    internal int realTime, 
                 gameTime, 
                 numDaysInYear, 
                 numDaysInWeeks;

    public Calendar calendar;
    
    public int RealTime
    {
        get { return realTime; }
        set { realTime = value; }
    }

    public int GameTime
    {
        get { return gameTime; }
        set { gameTime = value; }
    }

    //How many game seconds pass in one second of game play
    public float GameSecondsToRealSeconds
    {
        get { return gameSecondToRealSecond;  }
        set { gameSecondToRealSecond = value; }
    }

#region Read Only Calendar Info

    public float GameSecondsInHour
    {
        get { return 60*60; }
    }
    public float GameSecondsInDay
    {
        get { return 60*60*Day.HoursInDay; }
    }

    public float GameSecondsInWeek
    {
        get { return GameSecondsInDay* calendar.DaysInWeek; }
    }

    public float GameSecondsInYear
    {
        get { return GameSecondsInDay*calendar.DaysInYear; }
    
    }
#endregion
	// Use this for initialization
	void Start ()
	{
	    Chronos.Instance.Init();

	}

    void CalcRatio()
    {
        float totalRealUnits = GetTotalSeconds(realTimeUnit, realTime);
        float totalGameUnits = GetTotalSeconds(gameUnit, gameTime);
        gameSecondToRealSecond = totalGameUnits/totalRealUnits;
        Debug.Log("Game seconds to real seconds: " + gameSecondToRealSecond);
    }

    float GetTotalSeconds(TimeUnit unit, int multiplier)
    {
         float totalGameSeconds = 0.0f;
        switch (GameUnit)
        {
            case TimeUnit.Second:
                totalGameSeconds = 1;
                break;
            case TimeUnit.Minute:
                totalGameSeconds = 60;
                break;
            case TimeUnit.Hour:
                totalGameSeconds = GameSecondsInHour;
                break;
            case TimeUnit.Day :
                totalGameSeconds = GameSecondsInDay;
                break;
            case TimeUnit.Week:
                totalGameSeconds = GameSecondsInWeek;
                break;
            case TimeUnit.Year:
                totalGameSeconds = GameSecondsInYear;
                break;
                
        }
        totalGameSeconds *= GameTime;
        Debug.Log("Total game seconds in ratio" + totalGameSeconds);
        return totalGameSeconds;
    }
	
}
