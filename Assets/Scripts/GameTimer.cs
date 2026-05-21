using System;
using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private int elapsedSeconds;
    private bool isRunning;
    public void StartTimer()
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
    IEnumerator TickOneSecond()
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(1);
            if (!isRunning) break;
            elapsedSeconds += 1;
        }
    }
    public void ResetTimer()
    {
        elapsedSeconds = 0;
    }
}
