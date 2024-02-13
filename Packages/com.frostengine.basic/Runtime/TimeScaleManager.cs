using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frost
{
    [Serializable]
    public class TimeScaleModifier
    {
        [ReadOnly] public Guid ID;
        [ReadOnly] public string name;
        [ReadOnly] public float timeScale;
    }

    public class TimeScaleManager : Singleton<TimeScaleManager>
    {
        [SerializeField] private List<TimeScaleModifier> stackModifiers;
        private FrostDictionary<Guid, TimeScaleModifier> modifiers;

        public override void Awake()
        {
            base.Awake();
            modifiers = new FrostDictionary<Guid, TimeScaleModifier>();
            stackModifiers = new List<TimeScaleModifier>();
        }

        public void SetTimeScaleModifier(TimeScaleModifier _modifier, bool setOnTop = false)
        {
            modifiers[_modifier.ID] = _modifier;
            if (!stackModifiers.Contains(_modifier))
            {
                stackModifiers.Add(_modifier);
                Time.timeScale = _modifier.timeScale;
            }
            else if (setOnTop)
            {
                stackModifiers.Remove(_modifier);
                stackModifiers.Add(_modifier);
            }

            if (GetLastHandler() == _modifier)
                Time.timeScale = _modifier.timeScale;
        }

        public TimeScaleModifier GetLastHandler()
        {
            return stackModifiers[stackModifiers.Count - 1];
        }

        public TimeScaleModifier CreateTimeScaleModifier(string _name, float _timeScale = 1f)
        {
            TimeScaleModifier newModifier = new TimeScaleModifier() { ID = Guid.NewGuid(), name = _name, timeScale = _timeScale };
            return newModifier;
        }

        public void RemoveTimeScaleModifier(TimeScaleModifier _modifier)
        {
            modifiers.Remove(_modifier.ID);
            stackModifiers.Remove(_modifier);

            if (stackModifiers.Count <= 0)
                Time.timeScale = 1.0f;
            else
            {
                TimeScaleModifier lastModifier = GetLastHandler();
                Time.timeScale = modifiers[lastModifier.ID].timeScale;
            }
        }

        public bool HasModifier(TimeScaleModifier _modifier)
        {
            return stackModifiers.Contains(_modifier) || modifiers.ContainsKey(_modifier.ID);
        }

        public void ClearTimeScaleModifiers()
        {
            modifiers.Clear();
            stackModifiers.Clear();
            Time.timeScale = 1.0f;
        }

#if UNITY_EDITOR
        public void OnGUI()
        {
            GUI.color = Color.yellow;
            GUILayout.Label($"TimeScale : {Time.timeScale}", GUI.skin.box);
        }
#endif
    }
}