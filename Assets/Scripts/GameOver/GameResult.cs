using System;

[Serializable]
public class GameResult
{
    public int points;
    public int time;
    public int total;

    public GameResult(int points, int time)
    {
        this.points = points;
        this.time = time;
        this.total = points + time;
    }
}