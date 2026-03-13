using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public GameOverRankingDisplay rankingDisplay;

    public void OnRetryButton()
    {
        if (rankingDisplay != null)
        {
            rankingDisplay.ClearRankingTexts();
        }

        StartCoroutine(LoadGameAndPlayMusic());
    }

    public void OnMenuButton()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetGame(); 
        SceneManager.LoadScene("Menu");
    }
    private IEnumerator LoadGameAndPlayMusic()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Game");

        yield return new WaitForSeconds(0.1f); 
        AudioManager.Instance.PlayGameplayMusic();
    }
}
