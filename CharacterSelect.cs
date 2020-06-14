using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public void ChooseCharacter(int characterIndex)
    {
        PlayerPrefs.SetInt("SelectedCjaracter", characterIndex);
    }



}
