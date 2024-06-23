using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource backgroundMusic;

    public Button buttonOn;
    public Button buttonOff;

    private void Start()
    {
        if (backgroundMusic == null)
        {
            GameObject musicObject = GameObject.FindGameObjectWithTag("Music");
            if (musicObject != null)
            {
                backgroundMusic = musicObject.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogError("game object dengan tag 'music' tidak ditemukan. ");
            }
        }

        if (buttonOn == null || buttonOff == null)
        {
            Debug.LogError("bt on/off tidak ditetapkan diinspektor");
        }
        else
        {
            // debug u/ memeriksa penetapan button
            Debug.Log("bt on/off ditetapkan");
        }

        UpdateButtonStatus();
    }

    // untuk mengaktifkan/menonaktifkan backsound
    public void ToggleSound(bool isOn)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isOn;
        }
        UpdateButtonStatus();
    }

    // untuk mengatur volume backsound
    public void AdjustVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = Mathf.Clamp(volume, 0f, 1f);
            Debug.Log("Volume set to: " + backgroundMusic.volume);
        }
    }

    // memperbarui status button sesuai backsound
    private void UpdateButtonStatus()
    {
        if (backgroundMusic != null)
        {
            bool isMuted = backgroundMusic.mute;
            if (buttonOn != null)
            {
                buttonOn.gameObject.SetActive(isMuted);
            }
            else
            {
                Debug.LogError("bt on tidak ditetapkan");
            }

            if (buttonOff != null)
            {
                buttonOff.gameObject.SetActive(!isMuted);
            }
            else
            {
                Debug.LogError("bt off tidak ditetapkan");
            }
        }
        else
        {
            Debug.LogError("bg music tidak ditetapkan");
        }
    }

    // memastikan status button diperbarui
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // memperbarui referensi backgroundMusic dan status button setiap ganti scene
        GameObject musicObject = GameObject.FindGameObjectWithTag("Music");
        if (musicObject != null)
        {
            backgroundMusic = musicObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Game object dengan tag 'music' tidak ditemukan");
        }

        UpdateButtonStatus();
    }
}
