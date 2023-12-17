using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxEngine;

public class Test : MonoBehaviour
{
    public MappingInputs testMapping;

    private void Start()
    {
        NewInputSystem.Instance.ChangeMapping(testMapping);
    }

    private void Update()
    {
        if(NewInputSystem.GetKeyDown("Jump"))
            FoxEngine.Debug.Log("Jump !!");
    }
}
