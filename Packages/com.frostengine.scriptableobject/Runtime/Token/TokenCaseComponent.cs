using System;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime;
using UnityEngine;

public class TokenCaseComponent : MonoBehaviour
{
    [SerializeField] private TokenCase<Token> tokenCase;

    public int Capacity
    {
        get { return tokenCase.capacity; }
        set { tokenCase.capacity = value; }
    }

    public List<Token> Tokens => tokenCase.Tokens;

    public void OnValidate()
    {
        if (tokenCase.capacity < -1)
            tokenCase.capacity = -1;
    }

    public void Add(Token _token)
    {
        tokenCase.Add(_token);
    }

    public List<Token> AddRange(IEnumerable<Token> _tokens)
    {
       return tokenCase.AddRange(_tokens);
    }

    public bool IsFull()
    {
        return tokenCase.IsFull();
    }

    public bool IsEmpty()
    {
        return tokenCase.IsEmpty();
    }

    public bool Exchange(Token _old, Token _new)
    {
        return tokenCase.Exchange(_old,_new);
    }

    public void Remove(Token _token)
    {
        tokenCase.Remove(_token);
    }

    public void RemoveRange(ICollection<Token> _tokens)
    {
        tokenCase.RemoveRange(_tokens);
    }

    public void Clear()
    {
        tokenCase.Clear();
    }

    public bool Contains(Token _token)
    {
        return tokenCase.Contains(_token);
    }

    public void Sort(TokenSort _tokenSort)
    {
        tokenCase.Sort(_tokenSort);
    }

    public List<Token> Get(Predicate<Token> _condition)
    {
        return tokenCase.Get(_condition);
    }
}