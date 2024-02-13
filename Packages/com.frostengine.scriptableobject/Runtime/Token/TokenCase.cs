using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class TokenSort : Enumeration
{
    public static TokenSort Alphabetic = new(1,nameof(Alphabetic));
    public static TokenSort InstanceID = new(2,nameof(InstanceID));

    public TokenSort(int id, string name) : base(id, name){}
}

[Serializable]
public class TokenCase
{
    [ReadOnly,SerializeField] private List<Token> tokens;
    public int capacity = -1; // -1 = infinity

    public List<Token> Tokens => tokens;
    public int Count => tokens.Count;

    public void Add(Token _token)
    {
        if(capacity < 0 || tokens.Count < capacity)
            tokens.Add(_token);
    }

    public void Add(Token _token, int _nb)
    {
        for (int i = 0; i < _nb; i++)
            Add(_token);
    }

    public List<Token> AddRange(IEnumerable<Token> _tokens)
    {
        List<Token> tokensNoAdded = new List<Token>();

        foreach(var t in _tokens) 
        {
            if (capacity < 0 || tokens.Count < capacity)
                tokens.Add(t);
            else
                tokensNoAdded.Add(t);
        }

        return tokensNoAdded;
    }

    public bool IsFull()
    {  
        return capacity < 0 && tokens.Count >= capacity;
    }

    public bool IsEmpty()
    {
        return tokens.Count == 0;
    }

    public bool Exchange(Token _old, Token _new)
    {
        if (Contains(_old))
        {
            tokens.Remove(_old);
            tokens.Add(_new);

            return true;
        }

        return false;
    }

    public void Remove(Token _token)
    {
        tokens.Remove(_token);
    }

    public void Remove(Token _token, int _nb)
    {
        for (int i = 0;i < _nb;i++)
            Remove(_token);
    }

    public void RemoveRange(ICollection<Token> _tokens)
    {
        foreach (Token _token in _tokens) 
            tokens.Remove(_token);
    }

    public void Clear()
    {
        tokens.Clear();
    }

    public bool Contains(Token _token)
    {
        return tokens.Contains(_token);
    }

    public bool Contains(Token _token, int _nb)
    {
        int count = 0;
        foreach (var item in tokens)
        {
            if (item == _token)
                count++;
        }

        return count >= _nb;
    }

    public virtual void Sort(TokenSort _tokenSort)
    {
        if (_tokenSort == TokenSort.Alphabetic)
            tokens = tokens.OrderBy(x => x.name).ToList();
        else if (_tokenSort == TokenSort.InstanceID)
            tokens = tokens.OrderBy(x => x.GetInstanceID()).ToList();
    }

    public List<Token> Get(Predicate<Token> _condition)
    { 
        return tokens.Where(x => _condition(x)).ToList();
    }
}
