using UnityEngine;

public class GameStats
{
    public int EnemiesSpawned { get; set; }
    public int EnemiesDefeated { get; set; }
    public float StartTime { get; set; }
    public float EndTime { get; set; }
    public float DamageSustained { get; set; }
    public string Time => SecondsToDisplayTime(EndTime - StartTime);

    public static string SecondsToDisplayTime(float seconds)
    {
        int totalSeconds = Mathf.RoundToInt(seconds);
        int totalMinutes = totalSeconds / 60;
        int displaySeconds = totalSeconds % 60;
        return $"{totalMinutes}:{displaySeconds:00}";
    }
}