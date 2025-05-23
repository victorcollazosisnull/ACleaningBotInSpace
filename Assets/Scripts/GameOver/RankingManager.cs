using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RankingManager
{
    private const int MaxRankingSize = 5;
    private const string RankingKey = "GameRanking";

    public static void SaveResult(GameResult newResult)
    {
        List<GameResult> ranking = GetRanking();
        ranking.Add(newResult);

        // Ordena de mayor a menor por total
        ranking.Sort((a, b) => b.total.CompareTo(a.total));

        // Mantiene solo los 5 mejores
        if (ranking.Count > MaxRankingSize)
        {
            ranking.RemoveRange(MaxRankingSize, ranking.Count - MaxRankingSize);
        }

        string json = JsonUtility.ToJson(new GameResultList(ranking));
        PlayerPrefs.SetString(RankingKey, json);
        PlayerPrefs.Save();
    }

    public static List<GameResult> GetRanking()
    {
        if (!PlayerPrefs.HasKey(RankingKey))
            return new List<GameResult>();

        string json = PlayerPrefs.GetString(RankingKey);
        GameResultList list = JsonUtility.FromJson<GameResultList>(json);
        return list.results;
    }

    [System.Serializable]
    private class GameResultList
    {
        public List<GameResult> results;

        public GameResultList(List<GameResult> results)
        {
            this.results = results;
        }
    }
}