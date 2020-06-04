using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;
    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    
    public void CallRegister()
    {
        StartCoroutine(Register());
    }

   
    IEnumerator Register()
    {
       WWWForm form = new WWWForm();
      
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest www =  UnityWebRequest.Post("http://localhost:81/sqlconnect/register.php", form);

        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.Log("Error While Sending: " + www.error);
        }
        else
        {
            Debug.Log("Received: " + www.downloadHandler.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 5 && passwordField.text.Length >= 5);
    }
}
