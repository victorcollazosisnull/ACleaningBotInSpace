using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void OnPlayPressed()
    {
        AudioManager.Instance.PlayClickSound();
        SceneManager.LoadScene("Game");
    }
    public void OnBotonsPressed()
    {
        AudioManager.Instance.PlayClickSound();
    }
}
