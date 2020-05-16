﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {


    
  public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }
    
  public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }

  public void ExitGame()
    {
        Application.Quit();
    }
}
