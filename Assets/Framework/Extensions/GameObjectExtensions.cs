using UnityEngine;

public static class GameObjectExtensions
{
    public static void ClearChilds(this GameObject _gameObject)
    {
        _gameObject.transform.ClearChilds();
    }
    
    public static bool IsInLayerMask(this GameObject _obj, LayerMask _layerMask)
    {
        return ((_layerMask.value & (1 << _obj.layer)) > 0);
    }
}
