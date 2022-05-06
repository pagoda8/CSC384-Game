using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimeManager : MonoBehaviour
{
    public static PlaytimeManager shared;

    public int gamesPlayed;
    public int playtime;
    public int seconds;
    public int minutes;
    public int hours;
    public int days;

    private void Awake() {
        shared = this;
    }

    public void StartTimer() {
        LoadPlayerPrefs();
        StartCoroutine("PlayTimer");
    }

    public void StopTimer() {
        StopCoroutine("PlayTimer");
        gamesPlayed++;
        SavePlayerPrefs();
    }

    private void LoadPlayerPrefs() {
        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed", 0);
        playtime = PlayerPrefs.GetInt("totalPlaytime", 0);
        seconds = PlayerPrefs.GetInt("totalSeconds", 0);
        minutes = PlayerPrefs.GetInt("totalMinutes", 0);
        hours = PlayerPrefs.GetInt("totalHours", 0);
        days = PlayerPrefs.GetInt("totalDays", 0);
    }

    private void SavePlayerPrefs() {
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
        PlayerPrefs.SetInt("totalPlaytime", playtime);
        PlayerPrefs.SetInt("totalSeconds", seconds);
        PlayerPrefs.SetInt("totalMinutes", minutes);
        PlayerPrefs.SetInt("totalHours", hours);
        PlayerPrefs.SetInt("totalDays", days);
    }

    private IEnumerator PlayTimer() {
        while (true) {
            yield return new WaitForSeconds(1);
            playtime++;
            seconds = playtime % 60;
            minutes = (playtime / 60) % 60;
            hours = (playtime / 3600) % 24;
            days = (playtime / 86400) % 365;
        }
    }
}
