﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}