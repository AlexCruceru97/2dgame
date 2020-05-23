using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Character : MonoBehaviour
{
    public GameObject PlayerMale;
    public GameObject PlayerFemale;
    private Vector3 CharacterPosition;
    private Vector3 OffScreen;
    private int CharacterInt = 0;
    private SpriteRenderer PlayerMaleRender, PlayerFemaleRender;
    

    //Punem imaginile caracterelor pe screen si off screen
    private void Awake()
    {
        CharacterPosition = PlayerMale.transform.position;
        OffScreen = PlayerFemale.transform.position;
        PlayerMaleRender = PlayerMale.GetComponent<SpriteRenderer>();
        PlayerFemaleRender = PlayerFemale.GetComponent<SpriteRenderer>();
    }

    public void NextCharacter()
    {
        //Alegem primul character 
        if (CharacterInt == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", 0);
            PlayerMaleRender.enabled = false;
            PlayerMale.transform.position = OffScreen;
            PlayerFemale.transform.position = CharacterPosition;
            PlayerFemaleRender.enabled = true;
            CharacterInt = 0;
        }
        //Alegem al doilea caracter
        else if (CharacterInt==0)
        {
            PlayerPrefs.SetInt("SelectedCharacter", 1);
            PlayerFemaleRender.enabled = false;
            PlayerFemale.transform.position = OffScreen;
            PlayerMale.transform.position = CharacterPosition;
            PlayerMaleRender.enabled = true;
            CharacterInt = 1;
        }
       
    }

   

}
