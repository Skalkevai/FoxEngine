using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestData")]
public class QuestData : ScriptableObject
{
    public Token tokenComplete;
    public List<QuestStep> steps = new List<QuestStep>();

    public bool QuestCompleted(TokenCase _case)
    {
        if(_case.Contains(tokenComplete))
            return true;

        int stepCompleted = 0;
        foreach (QuestStep step in steps) 
        {
            if(step.IsCompleted(_case))
                stepCompleted++;
        }

        return stepCompleted >= steps.Count;
    }
}

[Serializable]
public class QuestStep
{
    public string name;
    public string description;

    public int amount;
    public Token token;

    public bool IsCompleted(TokenCase _case)
    {
       return _case.Contains(token, amount);
    }
}
