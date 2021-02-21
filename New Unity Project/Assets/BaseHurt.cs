using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHurt : MonoBehaviour
{
    private int FullHP;
    // Start is called before the first frame update
    void Start()
    {
        FullHP = GameManager.HomeHP;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(0, 40 * ((float)GameManager.HomeHP / (float)FullHP) - 40, 0);
    }
}
