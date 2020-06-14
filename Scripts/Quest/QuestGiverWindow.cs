using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverWindow : Window
{
    private QuestGiver questGiver;

    private static QuestGiverWindow instance;
    [SerializeField]
    private GameObject backBtn, acceptBtn, completeBtn,questDescription;

    private List<GameObject> quests = new List<GameObject>();

    private Quest selectedQuest;

    public static QuestGiverWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestGiverWindow>();
            }
            return instance;
        }
    }
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questArea;
    public void ShowQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        foreach(GameObject go in quests)
        {
            Destroy(go);
        }

        questArea.gameObject.SetActive(true);
        questDescription.SetActive(false);

        foreach (Quest quest in questGiver.MyQuests)
        {
            if (quest != null)
            {
                GameObject go = Instantiate(questPrefab, questArea);
                go.GetComponent<Text>().text = quest.MyTitle;

                go.GetComponent<QGQuestScript>().MyQuest = quest;

                quests.Add(go);
                if (QuestLog.MyInstance.HasQues(quest) && quest.IsComplete)
                {
                    go.GetComponent<Text>().text += "(C)";
                }
                else if (QuestLog.MyInstance.HasQues(quest))
                {
                    Color c = go.GetComponent<Text>().color;
                    c.a = 0.5f;
                    go.GetComponent<Text>().color = c;
                }
            }
           
        }
    }

    public override void Open(NPC npc)
    {
        ShowQuests((npc as QuestGiver));
        base.Open(npc);
    }

    public void ShowQuestInfo(Quest quest)
    {
        this.selectedQuest = quest;

        if (QuestLog.MyInstance.HasQues(quest) && quest.IsComplete)
        {
            acceptBtn.SetActive(false);
            completeBtn.SetActive(true);
        }
        else if (!QuestLog.MyInstance.HasQues(quest))
        {
            acceptBtn.SetActive(true);
        }
        backBtn.SetActive(true);
       
        questArea.gameObject.SetActive(false);
        questDescription.SetActive(true);

        string title = quest.MyTitle;
        string description = quest.MyDescription;
        string objectives = string.Empty;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.GetComponent<Text>().text = string.Format("{0}\n <size=10>{1}</size>", title, description);

    }

    public void Back()
    {
        backBtn.SetActive(false);
        acceptBtn.SetActive(false);
        ShowQuests(questGiver);
        completeBtn.SetActive(false);
    }

    public void Accept()
    {
        QuestLog.MyInstance.AcceptQuest(selectedQuest);
        Back();
    }

    public override void Close()
    {
        completeBtn.SetActive(false);
        base.Close();
    }

    public void CompelteQuest()
    {
        if (selectedQuest.IsComplete)
        {
            for (int i = 0; i < questGiver.MyQuests.Length; i++)
            {
                if (selectedQuest == questGiver.MyQuests[i])
                {
                    questGiver.MyQuests[i] = null;
                }
            }
            Back();
        }
    }
}
