using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Quest 
{

    public QuestScript MyQuestScript { get; set; }

    [SerializeField]
    private string title;

    [SerializeField]
    private string description;
    [SerializeField]
    private CollectObjective[] collectObjectives;

    [SerializeField]
    private KillObjective[] killObjectives;
    public string MyTitle { get => title; set => title = value; }
    public string MyDescription { get => description; set => description = value; }
    public CollectObjective[] MyCollectObjectives { get => collectObjectives; set => collectObjectives = value; }
    public KillObjective[] MyKillObjectives { get => killObjectives;  }
    public bool IsComplete
    {
        get
        {
            foreach(Objective obj in collectObjectives)
            {
                if (!obj.IsComplete)
                {
                    return false;
                }
            }
            foreach (KillObjective kill in killObjectives)
            {
                if (!kill.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }

   
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int MyAmount { get => amount; set => amount = value; }
    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }
    public string MyType { get => type; set => type = value; }
    public bool IsComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}


[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount(Item item)
    {
        if (MyType.ToLower() == item.MyTitle.ToLower())
        {
            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(item.MyTitle);
            QuestLog.MyInstance.UpdateSelected();
            QuestLog.MyInstance.CheckCompletion();
        }
    }
    public void UpdateItemCount()
    {
       
            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(MyType);
            QuestLog.MyInstance.UpdateSelected();
            QuestLog.MyInstance.CheckCompletion();
        
    }
}

[System.Serializable]
public class KillObjective : Objective
{
  public void UpdateKillCount(Character character)
    {
        if (MyType == character.MyType)
        {
            MyCurrentAmount++;

            QuestLog.MyInstance.UpdateSelected();
            QuestLog.MyInstance.CheckCompletion();

        }
    }
}