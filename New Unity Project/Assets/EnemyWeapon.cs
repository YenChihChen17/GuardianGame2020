using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject Boss;
    public float hitF;
    // Start is called before the first frame update
    void Start()
    {
        
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
            GameManeger.HomeHP = GameManeger.HomeHP - GameManeger.Damage_E;
        }

        if (PW.gameObject.tag == "Counter")
        {
            Debug.Log("Countered");
            this.GetComponentInParent<EnemyControl>().counter = true;
            this.GetComponentInParent<EnemyControl>().DoAttack = false;
        }
    }
}
