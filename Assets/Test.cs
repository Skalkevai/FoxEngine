using UnityEngine;
using Frost;

public class Test : MonoBehaviour
{
    private TokenCase tokenCase;
    private QuestJournal journal;

    public void Awake()
    {
        journal = GetComponent<QuestJournal>();
        journal.InitJournal(tokenCase);    
    }
} 
