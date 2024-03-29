using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct FrostPair<T>
{
    public string key;
    public T value;
}

[Serializable]
public struct FrostPair<T, K>
{
    public T key;
    public K value;
}


[Serializable]
public class FrostDictionary<T, K>
{
    [SerializeField] public List<FrostPair<T, K>> dictionary = new List<FrostPair<T, K>>();

    private Dictionary<T, K> realDictionary = null;

    public Dictionary<T, K> Dictionary
    {
        get
        {
            if (realDictionary == null)
            {
                realDictionary = new Dictionary<T, K>();
                foreach (var pair in dictionary)
                    realDictionary.Add(pair.key, pair.value);

                return realDictionary;
            }
            else
                return realDictionary;
        }
    }

    public int Count => Dictionary.Count;

    public K this[T _key] 
    { 
        get 
        { 
            return Dictionary[_key];
        }
        set 
        {
            Dictionary[_key] = value;
        }
    }

    public K this[int _index]
    { 
        get 
        { 
            return Dictionary.ElementAtOrDefault(_index).Value;
        }
        set
        {
            var key = Dictionary.ElementAtOrDefault(_index).Key;
            Dictionary[key] = value;
        }
    }

    public List<KeyValuePair<T,K>> List => Dictionary.ToList();

    public bool ContainsKey(T _key)
    {
        return Dictionary.ContainsKey(_key);
    }

    public bool TryGetValue(T _key, out K _value)
    {
        if (ContainsKey(_key))
        {
            _value = Dictionary[_key];
            return true;
        }
        else
        {
            _value = default;
            return false;
        }
    }

    public Dictionary<T,K>.KeyCollection GetKeys()
    {
        return Dictionary.Keys;
    }

    public Dictionary<T, K>.ValueCollection GetValues()
    {
        return Dictionary.Values;
    }

    public void Clear()
    {
        Dictionary.Clear();
    }

    public void Remove(T _key)
    { 
        Dictionary.Remove(_key);
    }

    public void Add(T _key, K _value)
    {
        Dictionary.Add(_key, _value);
    }

    public void Set(T _key, K _newValue)
    {
        Dictionary[ _key] = _newValue;
    }
}


