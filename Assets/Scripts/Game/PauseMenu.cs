using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;

    public OptionsControllerPanel optionsController;
    public int pausePanelIndex = 0;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isPaused = false;

    public void PauseGame()
    {
        if (isPaused) return;

        isPaused = true;
        GamePaused = true;

        optionsController.ShowPanel(pausePanelIndex);
        Time.timeScale = 0;

        masterSlider.value = SettingsManager.Instance.masterVolume;
        musicSlider.value = SettingsManager.Instance.musicVolume;
        sfxSlider.value = SettingsManager.Instance.sfxVolume;

        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        isPaused = false;
        GamePaused = false;

        optionsController.HidePanel(pausePanelIndex);

        StartCoroutine(ResumeAfterDelay(0.35f));

        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }

    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
    }

    public void OnPauseButtonPressed()
    {
        PauseGame();
    }

    public void OnResumeButtonPressed()
    {
        ResumeGame();
    }
}