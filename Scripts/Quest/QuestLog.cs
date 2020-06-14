using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private CanvasGroup canvasGroup;


    private List<QuestScript> questScripts = new List<QuestScript>();

    private List<Quest> quests = new List<Quest>();

    [SerializeField]
    private Text questDescription;

    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<QuestLog>();
            }
            return instance;
        }
    }

    public void AcceptQuest(Quest quest)
    {
       
        foreach(CollectObjective obj in quest.MyCollectObjectives)
        {
            InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(obj.UpdateItemCount);
            obj.UpdateItemCount();
        }

        foreach(KillObjective kill in quest.MyKillObjectives)
        {
            GameManager.MyInstance.killConfirmedEvent += new KillConfirmed(kill.UpdateKillCount);
        }
        quests.Add(quest);

        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;//quest has reference to the quest script
        qs.MyQuest = quest;//quest script has reference to the quest
        questScripts.Add(qs);////////////////////////////////////////////////////
        go.GetComponent<Text>().text = quest.MyTitle;
        CheckCompletion();
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {


            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
            }

            string objectives = string.Empty;

            selected = quest;

            string title = quest.MyTitle;

            foreach (Objective obj in quest.MyCollectObjectives)
            {
                objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            foreach (Objective obj in quest.MyKillObjectives)
            {
                objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            questDescription.text = string.Format("\t{0}\n  <size=10>{1}</size>\n\n\tObjectives\n\t<size=10>{2}</size>", title, quest.MyDescription, objectives);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            OpenClose();
        }
    }
    public void CheckCompletion()
    {
        foreach(QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha == 1)
        {
            Close();
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts =true;
        }
    }
    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void AbandonQuest()
    {
        
    }

    public bool HasQues(Quest quest)
    {

        return quests.Exists(x => x.MyTitle == quest.MyTitle);
    }
}
