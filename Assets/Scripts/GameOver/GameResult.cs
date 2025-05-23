using System;

[Serializable]
public class GameResult
{
    public float points;
    public float time;
    public float total;

    public GameResult(float points, float time)
    {
        this.points = points;
        this.time = time;
        this.total = points + time;
    }
}