using System;

[Serializable]
public class GameResult
{
    public string playerName;
    public float points;
    public float time;
    public float total;

    public GameResult(float points, float time, string playerName)
    {
        this.points = points;
        this.time = time;
        this.total = points + time;
        this.playerName = playerName;
    }
}