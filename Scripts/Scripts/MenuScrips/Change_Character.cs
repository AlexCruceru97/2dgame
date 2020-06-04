using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Character : MonoBehaviour
{
    public GameObject avatar1, avatar2;
    public  int CharacterInt = 1;

    //public int Character
    //{
    //    get
    //    {
    //        return this.CharacterInt;
    //    }
    //    set
    //        {
    //        CharacterInt = value;
    //    }
    //}


    // Start is called before the first frame update
    private void Start()
    {
        avatar1.gameObject.SetActive(true);
        avatar2.gameObject.SetActive(false);
    }

    public void ChangeAvatar()
    {
        if (CharacterInt == 1) {

            CharacterInt = 2;

            avatar1.gameObject.SetActive(false);
            avatar2.gameObject.SetActive(true);

        }
            else if (CharacterInt == 2) { 

                CharacterInt = 1;

                avatar1.gameObject.SetActive(true);
                avatar2.gameObject.SetActive(false);

               

        }
    }



}
