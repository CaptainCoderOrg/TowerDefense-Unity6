using UnityEngine;

public class GameStats
{
    public float StartTime { get; set; }
    public float EndTime { get; set; }
    public string Time => SecondsToDisplayTime(EndTime - StartTime);

    public static string SecondsToDisplayTime(float seconds)
    {
        int totalSeconds = Mathf.RoundToInt(seconds);
        int totalMinutes = totalSeconds / 60;
        int displaySeconds = totalSeconds % 60;
        return $"{totalMinutes}:{displaySeconds:00}";
    }
}