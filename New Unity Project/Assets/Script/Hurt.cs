using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    private int FullHP;
    // Start is called before the first frame update
    void Start()
    {
        FullHP = GameManeger.EnemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(800 * ((float)GameManeger.EnemyHP/(float)FullHP)-800, 0, 0);
    }
}
