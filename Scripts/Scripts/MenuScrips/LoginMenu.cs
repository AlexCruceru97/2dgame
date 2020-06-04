using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
  public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;


    public void CallLogin()
    {
        StartCoroutine(Login());
    }


    IEnumerator Login() {
        WWWForm form = new WWWForm();

        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:81/sqlconnect/login.php", form);

        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            Debug.Log("User Logged with success " + www.downloadHandler.text);
        }
        else
        {
            Debug.Log("User login failed error # " + www.downloadHandler.text);
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 5 && passwordField.text.Length >= 5);
    }

}
