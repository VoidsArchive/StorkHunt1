using System;
using System.Collections;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    private int elapsedSeconds;
    private bool isRunning;
    public void StartTimer(Action methodToCallWhenTimeIsOver)
    {
        StopAllCoroutines();
        isRunning = true;
        elapsedSeconds = 0;
        StartCoroutine(TickOneSecond());
    }
    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }
    public void AddSeconds(int seconds)
    {
        if (seconds <= 0) return;
        elapsedSeconds += seconds;
    }
    public void RemoveSeconds(int seconds)
    {
        if (seconds <= 0) return;
        elapsedSeconds -= seconds;
        if (elapsedSeconds < 0)
        {
            elapsedSeconds = 0;
        }
    }
    public string GetTimeAsString()
    {
        int minutes = elapsedSeconds / 60;
        int seconds = elapsedSeconds - (minutes * 60);
        string minutesAsString = String.Format("{0:00}", minutes);
        string secondsAsString = String.Format("{0:00}", seconds);
        return minutesAsString + ":" + secondsAsString;
    }
    public int GetSecondsRemaining()
    {
        return elapsedSeconds;
    }
    public bool IsRunning()
    {
        return isRunning;
    }
    IEnumerator TickOneSecond()
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(1);
            if (!isRunning) break;
            elapsedSeconds = elapsedSeconds + 1;
        }
    }
}
