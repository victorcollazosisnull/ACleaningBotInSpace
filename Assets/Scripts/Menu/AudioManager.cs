using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    [Header("SFX Clips")]
    public AudioClip clickSound;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip getCoinSound;
    public AudioClip teleportSound;
    public AudioClip explosionSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnSceneChanged;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.outputAudioMixerGroup = GetAudioGroup("Music");

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = GetAudioGroup("SFX");

        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SettingsManager.Instance.masterVolume = master;
        SettingsManager.Instance.musicVolume = music;
        SettingsManager.Instance.sfxVolume = sfx;

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;

        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        PlayMenuMusic();
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        Debug.Log("Scene changed to: " + newScene.name);

        if (newScene.name == "Game")
        {
            PlayGameplayMusic();
        }
        else if (newScene.name == "GameOver")
        {
            StopMusic();
            PlayDeathSound();
        }
        else
        {
            PlayMenuMusic();
        }
    }

    private AudioMixerGroup GetAudioGroup(string groupName)
    {
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(groupName);
        return groups.Length > 0 ? groups[0] : null;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        SettingsManager.Instance.masterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        SettingsManager.Instance.musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        SettingsManager.Instance.sfxVolume = volume;
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null)
            return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayClickSound() => sfxSource.PlayOneShot(clickSound);
    public void PlayDamageSound() => sfxSource.PlayOneShot(damageSound);
    public void PlayDeathSound() => sfxSource.PlayOneShot(deathSound);
    public void PlayJumpSound() => sfxSource.PlayOneShot(jumpSound);
    public void PlayGetCoinSound() => sfxSource.PlayOneShot(getCoinSound);
    public void PlayTeleportSound() => sfxSource.PlayOneShot(teleportSound);
    public void PlayExplosionSound() => sfxSource.PlayOneShot(explosionSound);
}