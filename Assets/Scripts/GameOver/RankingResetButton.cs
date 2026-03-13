using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingResetButton : MonoBehaviour
{
    public GameOverRankingDisplay rankingDisplay;

    public void ResetRanking()
    {
        PlayerPrefs.DeleteKey("GameRanking"); 
        PlayerPrefs.Save();

        if (rankingDisplay != null)
        {
            rankingDisplay.ClearRankingTexts(); 
        }

        Debug.Log("Ranking reseteado");
    }
}