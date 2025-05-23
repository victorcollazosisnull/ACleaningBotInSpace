using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public OptionsControllerPanel optionsController;
    public int pausePanelIndex = 0; 

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            optionsController.ShowPanel(pausePanelIndex);
            Time.timeScale = 0;

            masterSlider.value = SettingsManager.Instance.masterVolume;
            musicSlider.value = SettingsManager.Instance.musicVolume;
            sfxSlider.value = SettingsManager.Instance.sfxVolume;

            // Conectar sliders
            masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
        else
        {
            optionsController.HidePanel(pausePanelIndex);

            StartCoroutine(ResumeAfterDelay(0.35f)); 

            masterSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
        }
    }

    private System.Collections.IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); 
        Time.timeScale = 1;
    }

    public void OnPauseButtonPressed() => TogglePause();
    public void OnResumeButtonPressed() => TogglePause();
}