using System;
using UnityEngine;

namespace Frost
{
    [CreateAssetMenu]
    public class ControllerIcons : SerializedSO
    {
        public ControllerType controllerType;
        public FrostDictionary<string, Sprite> icons = new FrostDictionary<string, Sprite>();

        public Sprite GetIcon(string key)
        {
            if (icons.TryGetValue(key, out Sprite icon))
                return icon;
            return null;
        }
    }
}