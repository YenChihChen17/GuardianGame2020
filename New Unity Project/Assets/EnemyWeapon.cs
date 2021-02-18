using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Player;
    public float hitF;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Home")
        {
            this.GetComponentInParent<EnemyControl>().HitHome = true;
            Boss.GetComponent<Rigidbody>().AddForce(new Vector3(hitF, 0, 0), ForceMode.Impulse);
            Debug.Log("Hit");
            GameManeger.HomeHP = GameManeger.HomeHP - GameManeger.Damage_E*2;
        }

        if (PW.gameObject.tag == "Counter" && Player.GetComponent<PlayerControl>().counter ==true )
        {
            Debug.Log("Countered");
            if(this.GetComponentInParent<EnemyControl>().attack)
            {
                this.GetComponentInParent<EnemyControl>().counter = true;
                this.GetComponentInParent<EnemyControl>().DoAttack = false;
            }
        }
    }
}
