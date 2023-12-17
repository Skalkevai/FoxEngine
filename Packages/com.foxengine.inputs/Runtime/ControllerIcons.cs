using System;
using UnityEngine;

namespace FoxEngine
{
    [CreateAssetMenu]
    public class ControllerIcons : SerializedSO
    {
        public ControllerType controllerType;
        public FoxDictionary<string, Sprite> icons = new FoxDictionary<string, Sprite>();

        public Sprite GetIcon(string key)
        {
            if (icons.TryGetValue(key, out Sprite icon))
                return icon;
            return null;
        }
    }
}