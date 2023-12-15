using FoxEngine;
using UnityEngine;

[CreateAssetMenu()]
public class PoolList : ScriptableObject
{
    public FoxDictionary<PoolItem, int> items = new FoxDictionary<PoolItem, int>();
}
