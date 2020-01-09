using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider[] volumes;
    public AudioSource click;

    public void Start()
    {
        if (PlayerPrefs.HasKey("master"))
            volumes[0].value = (PlayerPrefs.GetFloat("master"));
        else
        {
            volumes[0].value = 0;
            PlayerPrefs.SetFloat("master", 0);
        }
        if (PlayerPrefs.HasKey("bg"))
            volumes[1].value = (PlayerPrefs.GetFloat("bg"));
        else
        {
            volumes[1].value = 0;
            PlayerPrefs.SetFloat("bg", 0);
        }
        if (PlayerPrefs.HasKey("sfx"))
            volumes[2].value = (PlayerPrefs.GetFloat("sfx"));
        else
        {
            volumes[2].value = 0;
            PlayerPrefs.SetFloat("sfx", 0);
        }
    }

    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("master", volume);
        PlayerPrefs.SetFloat("master", volume);
    }

    public void setBgVolume(float volume)
    {
        audioMixer.SetFloat("bg", volume);
        PlayerPrefs.SetFloat("bg", volume);
    }

    public void setSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfx", volume);
    }

    public void clickAudio()
    {
        click.Play();
    }
}
