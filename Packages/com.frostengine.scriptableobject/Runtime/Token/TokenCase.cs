using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class TokenSort : Enumeration<TokenSort>
{
    public static TokenSort Alphabetic = new(1,nameof(Alphabetic));
    public static TokenSort InstanceID = new(2,nameof(InstanceID));

    public TokenSort(int id, string name) : base(id, name){}
}

[Serializable]
public class TokenCase<T> where T : Token
{
    [ReadOnly,SerializeField] private List<T> tokens;
    public int capacity = -1; // -1 = infinity

    public List<T> Tokens => tokens;
    public int Count => tokens.Count;

    public void Add(T _token)
    {
        if(capacity < 0 || tokens.Count < capacity)
            tokens.Add(_token);
    }

    public void Add(T _token, int _nb)
    {
        for (int i = 0; i < _nb; i++)
            Add(_token);
    }

    public List<T> AddRange(IEnumerable<T> _tokens)
    {
        List<T> tokensNoAdded = new List<T>();

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

    public bool Exchange(T _old, T _new)
    {
        if (Contains(_old))
        {
            tokens.Remove(_old);
            tokens.Add(_new);

            return true;
        }

        return false;
    }

    public void Remove(T _token)
    {
        tokens.Remove(_token);
    }

    public void Remove(T _token, int _nb)
    {
        for (int i = 0;i < _nb;i++)
            Remove(_token);
    }

    public void RemoveRange(ICollection<T> _tokens)
    {
        foreach (T _token in _tokens) 
            tokens.Remove(_token);
    }

    public void Clear()
    {
        tokens.Clear();
    }

    public bool Contains(T _token)
    {
        return tokens.Contains(_token);
    }

    public bool Contains(T _token, int _nb)
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

    public List<T> Get(Predicate<T> _condition)
    { 
        return tokens.Where(x => _condition(x)).ToList();
    }
}
