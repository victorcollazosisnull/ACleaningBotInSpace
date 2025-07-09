using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverRankingDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] rankTexts;

    private void Start()
    {
        ShowRanking();
    }

    public void ShowRanking()
    {
        List<GameResult> ranking = RankingManager.GetRanking();

        for (int i = 0; i < rankTexts.Length; i++)
        {
            if (i < ranking.Count)
            {
                GameResult r = ranking[i];
                rankTexts[i].text = $"{r.playerName} - {r.total:F0} pts";
            }
            else
            {
                rankTexts[i].text = "";
            }
        }
    }
}