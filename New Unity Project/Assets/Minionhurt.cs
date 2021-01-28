using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minionhurt : MonoBehaviour
{
    private int FullHP;
    // Start is called before the first frame update
    void Start()
    {
        FullHP = GameManeger.MinnionHP;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(800 * ((float)this.GetComponentInParent<EnemyGroundMinnionControl>().HP / (float)FullHP) - 800, 0, 0) ;
    }
}
