using UnityEngine;

public class QuestJournal : SerializedMono
{
    private TokenCase questCase;
    public QuestDefinition currentQuest;

    public void InitJournal(TokenCase _questCase)
    { 
        questCase = _questCase;
    }

    public void AddQuest(QuestDefinition _quest)
    {
        if (questCase == null)
            return;

        questCase.Add(_quest);
        if (currentQuest == null)
            currentQuest = _quest;
    }

    public bool HasQuest(QuestDefinition _quest)
    {
        if (questCase != null)
            return questCase.Contains(_quest);
        else return false;
    }

    public bool QuestCompleted(QuestDefinition _quest)
    {
       if(questCase != null)
            return _quest.QuestCompleted(questCase);

       return false;
    }
}
