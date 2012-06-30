
using System;
using UnityEngine;

public enum TimeUnit
{
    Second,
    Minute,
    Hour,
    Day,
    Week,
    Year,
}
public sealed class Chronos : Singleton<Chronos>
{
    
    //The calendar is a read only object.
    //
    private const string keyName = "SucellusStartTime";
    private const int NUM_MIN_IN_HOUR = 60, NUM_SEC_IN_MIN = 60;

    private DateTime startedTime;

    private float secondsMultiplier = 1; //How many game seconds are in one game hour
    

    private float totalGameTime; // the total game time played in seconds

    private bool runWhileOff;

    
                //All the current times
    private int gMin,
                gHour,
                gSec,
                gDayMonth,
                gMonth,
                gYear,
                //Start information
                startMonth,
                startDay,
                startHour,
                //Ratio Setters
                realTime,
                gameTime;
    private Calendar currentCalendar = new Calendar();
        
    private void Initilize()
    {
        if(!PlayerPrefs.HasKey(keyName))
        {
            Debug.Log("No Prev key found starting new calendar");
            startedTime = DateTime.UtcNow;
            Debug.Log("Start time: "+ startedTime);
            
        }
    }

    private float elapsedSinceStart;
    public void Update()
    {
        float gameTimeElapsedSeconds = Time.timeSinceLevelLoad*secondsMultiplier;

        gMin = (int)(gameTimeElapsedSeconds/NUM_SEC_IN_MIN);
        gHour = (int)(gMin / NUM_MIN_IN_HOUR);
        Debug.Log("Realtime: "+  Time.timeSinceLevelLoad + " Game time elapsed time: " + gHour + ":"+gMin);
    }

   
}

