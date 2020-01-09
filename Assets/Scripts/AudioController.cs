using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource click;

    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("master", volume);
    }

    public void setBgVolume(float volume)
    {
        audioMixer.SetFloat("bg", volume);
    }

    public void setSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
    }

    public void clickAudio()
    {
        click.Play();
    }
}
