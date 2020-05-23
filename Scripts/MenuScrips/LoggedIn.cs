using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoggedIn : MonoBehaviour
{
    public TextMeshProUGUI playerDisplay;

    //Shows what user is logged in
    public void Start()
    {
        if (DBManager.LoggedIn)
        {

            playerDisplay.text = "Player: " + DBManager.username;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(4);
    }

    //Logs out the user and goes to main menu
    public void LogOut()
    {
        DBManager.LogOut();
        SceneManager.LoadScene(0);
    }
}
