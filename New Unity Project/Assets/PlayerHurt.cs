using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private int FullHP;
    // Start is called before the first frame update
    void Start()
    {
        FullHP = GameManeger.PlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(320 * ((float)GameManeger.PlayerHP / (float)FullHP) - 320, 0, 0);
    }
}
