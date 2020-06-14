using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
   
    public Quest MyQuest { get; set; }


    private bool MarkedComplete = false;
    public void Select()
    {
        GetComponent<Text>().color = Color.green;
        QuestLog.MyInstance.ShowDescription(MyQuest);
        
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.white;
    }

    public void IsComplete()
    {
        if (MyQuest.IsComplete && !MarkedComplete)
        {
            MarkedComplete = true;
            GetComponent<Text>().text += "(COMPLETED)";
        }
        else if (!MyQuest.IsComplete)
        {
            MarkedComplete = false;
            GetComponent<Text>().text = MyQuest.MyTitle;
        }
       
    }
}
