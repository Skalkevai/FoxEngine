using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        MappingInputs mapping = NewInputSystem.Instance.GetMapping("InGame");
        ActionInput action = mapping.GetAction("Jump");

        NewInputSystem.Instance.IsBlock(action); //Check if the current mapping has this action blocked
        NewInputSystem.Instance.IsBlock("Jump"); //Check if the current mapping has this action blocked

        NewInputSystem.Instance.BlockInputs(); //Block all inputs in game
        NewInputSystem.Instance.UnBlockInputs(); //UnBlock all inputs in game

        NewInputSystem.Instance.BlockInput(mapping, action); //Block a action on a mapping
        NewInputSystem.Instance.BlockInput("InGame", "Jump"); //Block a action on a mapping
        mapping.BlockInput(action);//Block a action on a mapping
        mapping.BlockInputs(); //Block all inputs on this mapping

        NewInputSystem.Instance.UnBlockInput(mapping, action); //UnBlock a action on a mapping
        NewInputSystem.Instance.UnBlockInput("InGame", "Jump"); //UnBlock a action on a mapping
        mapping.UnBlockInput(action);//UnBlock a action on a mapping
        mapping.UnBlockInputs(); //UnBlock all inputs on this mapping
    }

    private void Update()
    {
        if (NewInputSystem.GetKeyDown("Jump"))
        {
            FoxEngine.Debug.Log("Test Jump");
        }
    }
} 
