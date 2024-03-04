using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

[Serializable]
public abstract class Enumeration<T> : IComparable where T : Enumeration<T>
{
    private static List<T> enums = new List<T>();

    public string Name { get; private set; }

    public int Id { get; private set; }

    protected Enumeration(int _id, string _name)
    {
        Id = _id;
        Name = _name;

        if(enums == null)
            enums = new List<T>();
    }

    protected void AddEnumeration(T _enum)
    {
        if (enums == null)
            enums = new List<T>();

        enums.Add(_enum);
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration<T> =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    public override bool Equals(object obj)
    {
        if (obj is not Enumeration<T> otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);
        var nameMatches = Name.Equals(otherValue.Name);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object other) => Id.CompareTo(((Enumeration<T>)other).Id);

    // Other utility methods ...

    public static T Parse(string _name)
    {
        foreach (var item in enums)
        {
            if (item.Name == _name)
                return item;
        }

        return null;
    }
}