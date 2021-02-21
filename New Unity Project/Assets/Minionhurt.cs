using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minionhurt : MonoBehaviour
{
    public enum type
    {
        Ground = 0,
        Fly = 1,
    }
    public type Minion;
    private int FullHP;
    
    // Start is called before the first frame update
    void Start()
    {
        FullHP = GameManager.MinnionHP;
    }

    // Update is called once per frame
    void Update()
    {
        int a = (int)Minion;
        if(a == 0)
        {
            this.transform.localPosition = new Vector3(800 * ((float)this.GetComponentInParent<EnemyGroundMinnionControl>().HP / (float)FullHP) - 800, 0, 0);
        }
        else if (a == 1)
        {
            this.transform.localPosition = new Vector3(800 * ((float)this.GetComponentInParent<EnemyFlyMinnionControl>().HP / (float)FullHP) - 800, 0, 0);
        }

    }
}
