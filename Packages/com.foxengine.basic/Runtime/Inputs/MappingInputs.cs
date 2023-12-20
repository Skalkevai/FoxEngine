using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxEngine
{
    [CreateAssetMenu]
    public class MappingInputs : SerializedSO
    {
        [SerializeField] protected List<ActionInput> actionInputs = new List<ActionInput>();
        protected FoxDictionary<string, ControlsPair[]> controls = new FoxDictionary<string, ControlsPair[]>();

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
    }
}
