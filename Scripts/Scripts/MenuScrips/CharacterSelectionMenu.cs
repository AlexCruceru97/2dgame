
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionMenu : MonoBehaviour
{

    private int Character = 1;
    //public static int CharacterInt = 2;
   
    public void Play()
    {
       
        if (Character == 1)
        {
            SceneManager.LoadScene("Player_Male");
        }
        else if (Character == 2)
        {
            SceneManager.LoadScene("Player_Female");
        }
    }
        public void Next()
        {
            if (Character == 1)
            {
                Character = 2;
            }
            else if (Character == 2)
            {
                Character = 1;
            }
        }
    public void Back() {
        SceneManager.LoadScene(3);
    }

}
