using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgMusicLoop : MonoBehaviour
{
    
    private void Awake()
    {
        GameObject[] bgMusic = GameObject.FindGameObjectsWithTag("music");
        if (bgMusic.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
