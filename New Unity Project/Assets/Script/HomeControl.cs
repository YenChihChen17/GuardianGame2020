using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Enemy" && Col.gameObject.name != "Bullet")
        {
            GameManager.HomeHP = GameManager.HomeHP - GameManager.Damage_E;
        }
    }

}
