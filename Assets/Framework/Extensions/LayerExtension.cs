using System;
using UnityEngine;

public static class LayerExtension
{
    public static LayerMask AllLayers = ~0;
    
    public static int GetLayerIndex(this LayerMask _layerMask)
    {
        return Convert.ToString(_layerMask.value, 2).Length - 1;
    }
}