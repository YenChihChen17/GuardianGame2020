﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCosuming : MonoBehaviour
{
    private float FullHP;
    // Start is called before the first frame update
    void Start()
    {
        FullHP = GameManager.PlayerMana;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(320 * ((float)GameManager.PlayerMana / (float)FullHP) - 320, 0, 0);
        
    }

    

}

