using UnityEngine;

[CreateAssetMenu(menuName = "Quest/QuestDefinition")]
public class QuestDefinition : Token
{
    public string questName;
    public string questDescription;
    public string questID;

    public QuestData data;

    public bool QuestCompleted(TokenCase _case)
    {
        return data.QuestCompleted(_case);
    }
}
