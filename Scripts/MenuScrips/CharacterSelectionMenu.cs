
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionMenu : MonoBehaviour
{
 
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene(5);
    }
    public void Back() {
        SceneManager.LoadScene(3);
    }

}
