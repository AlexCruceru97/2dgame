using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    //public GameObject[] players;
    public GameObject[] players;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("SelectedCharacter") == 0)
        {
            Instantiate(players[(0)], Vector2.zero, Quaternion.identity);


            // if (PlayerPrefs.GetInt("SelectedCharacter") == 1)
        }
        else
        {
            Instantiate(players[(1)], Vector2.zero, Quaternion.identity);

        }
    }


    // Assign the prefab in the inspector
    //public GameObject EnemyPrefab;
    ////Singleton
    //private static CharacterSpawner m_Instance = null;
    //public static CharacterSpawner Instance
    //{
    //    get
    //    {
    //        if (m_Instance == null)
    //        {
    //            m_Instance = (CharacterSpawner)FindObjectOfType(typeof(CharacterSpawner));
    //        }
    //        return m_Instance;
    //    }
    //}


}
