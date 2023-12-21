using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxEngine
{
    [CreateAssetMenu]
    public class MappingInputs : SerializedSO
    {
        public string ID;

        [SerializeField] protected List<ActionInput> actionInputs = new List<ActionInput>();
        protected FoxDictionary<string, ControlsPair[]> controls = new FoxDictionary<string, ControlsPair[]>();
        private bool allBlocked = false;
        private List<ActionInput> blockedInputs = new List<ActionInput>();

        public FoxDictionary<string, ControlsPair[]> Controls => controls;

        public void GenerateDictionnary()
        {
            controls.Clear();

            foreach (var action in actionInputs)
            {
                if(!controls.ContainsKey(action.key))
                    controls.Add(action.key, action.controls);
            }
        }

        public ActionInput GetAction(string _key)
        {
            foreach (var action in actionInputs)
            { 
                if(action.key == _key)
                    return action;
            }

            return null;
        }

        public void UnBlockInputs()
        {
            allBlocked = false;
        }

        public void BlockInputs()
        { 
            allBlocked = true;
        }

        public void BlockInput(ActionInput _action)
        {
            blockedInputs.Add(_action);
        }

        public void UnBlockInput(ActionInput _action)
        {
            blockedInputs.Remove(_action);
        }

        public bool IsBlocked(ActionInput _action)
        {
            if (allBlocked || blockedInputs.Contains(_action))
                return true;

            return false;
        }

        public void ClearBlockInputs()
        {
            allBlocked = false;
            blockedInputs.Clear();
        }
    }
}
