using Unity.VisualScripting;
using UnityEngine;

public class Test : SerializedMono
{
    [SerializeField] private Token test1;
    [SerializeField] private Token test2;
    [SerializeField] private Token test3;
    [SerializeField] private Token test4;

    [SerializeField] private Transform content;
    [SerializeField] private TokenUI tokenPrefab;

    private TokenCaseComponent tokenCase;

    private void Awake()
    {
        tokenCase = GetComponent<TokenCaseComponent>();
    }

    public void UpdateUI()
    {
        content.ClearChilds();
        foreach (Token t in tokenCase.Tokens)
        {
            TokenUI ui = Instantiate(tokenPrefab, content);
            ui.SetToken(t);
        }
    }

    public void AddRandom()
    {
        int r = Random.Range(0,4);
        switch (r) 
        {
            case 0:
                tokenCase.Add(test1);
                break;
            case 1:
                tokenCase.Add(test2);
                break;
            case 2:
                tokenCase.Add(test3);
                break;
            case 3:
                tokenCase.Add(test4);
                break;
        }

        UpdateUI();
    }

    public void Add(int _index)
    {
        switch (_index)
        {
            case 1:
                tokenCase.Add(test1);
                break;
            case 2:
                tokenCase.Add(test2);
                break;
            case 3:
                tokenCase.Add(test3);
                break;
            case 4:
                tokenCase.Add(test4);
                break;
        }

        UpdateUI();
    }

    public void Sort()
    {
        tokenCase.Sort(TokenSort.Alphabetic);

        UpdateUI();
    }

    public void Remove(int _index)
    {
        switch (_index)
        {
            case 1:
                tokenCase.Remove(test1);
                break;
            case 2:
                tokenCase.Remove(test2);
                break;
            case 3:
                tokenCase.Remove(test3);
                break;
            case 4:
                tokenCase.Remove(test4);
                break;
        }

        UpdateUI();
    }
}
