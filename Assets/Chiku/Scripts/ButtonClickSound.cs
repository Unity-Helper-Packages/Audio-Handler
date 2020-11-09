﻿using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            AudioManager.inst.Play(Audio.ButtonClick);
        });        
    }
}
