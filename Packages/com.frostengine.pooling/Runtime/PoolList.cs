using Frost;
using UnityEngine;

[CreateAssetMenu()]
public class PoolList : ScriptableObject
{
    public FrostDictionary<PoolItem, int> items = new FrostDictionary<PoolItem, int>();
}
