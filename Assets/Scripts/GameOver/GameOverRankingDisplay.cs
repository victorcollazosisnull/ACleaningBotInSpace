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
                rankTexts[i].text = $"{i + 1}: {r.total} pts";
            }
            else
            {
                rankTexts[i].text = $"{i + 1}:";
            }
        }
    }

    public void ClearRankingTexts()
    {
        if (rankTexts == null)
            return;

        for (int i = 0; i < rankTexts.Length; i++)
        {
            if (rankTexts[i] != null)
            {
                rankTexts[i].text = "";
            }
        }
    }
}