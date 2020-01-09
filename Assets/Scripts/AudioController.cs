using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource click;

    public void clickAudio()
    {
        click.Play();
    }
}
