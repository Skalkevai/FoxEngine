using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TokenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;

    public void SetToken(Token t)
    {
        name.text = t.name;
    }

   
}
