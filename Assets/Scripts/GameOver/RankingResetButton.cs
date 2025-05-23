using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingResetButton : MonoBehaviour
{
    public GameOverRankingDisplay rankingDisplay;

    public void ResetRanking()
    {
        PlayerPrefs.DeleteKey("Ranking");
        PlayerPrefs.Save();

        if (rankingDisplay != null)
        {
            rankingDisplay.ShowRanking();
        }
    }
}