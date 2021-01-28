using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundMinnionControl : MonoBehaviour
{
    public float speed;

    //private int HP; sonic changed:抓不到生命值
    private bool Dead;
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        HP = GameManeger.MinnionHP;  //sonic changed:抓不到生命值
    }

    // Update is called once per frame
    void Update()
    {
        if(Dead == false)
        {
            transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime, Space.World);
        }
        
        if (HP<=0) //sonic changed:抓不到生命值
        {
            Destroy(GetComponent<Collider>());
            Destroy(this.gameObject,3);
            Dead = true;
        }
        //Debug.Log(HP);
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Weapon" )
        {
            GameObject Player = GameObject.FindWithTag("Player");
            HP = HP - GameManeger.Damage_P;

            if (this.transform.position.x - Player.transform.position.x >= 0)//受攻擊給玩家反作用力
            {
                PlayerControl.AttackEnemy = true;
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(-6, 0, 0), ForceMode.Impulse);
            }
            else
            {
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(6, 0, 0), ForceMode.Impulse);
                PlayerControl.AttackEnemy = true;
            }
        }
        else if (PW.gameObject.tag == "Home")
        {
            Destroy(this.gameObject);
            GameManeger.HomeHP = GameManeger.HomeHP - GameManeger.Damage_E;
        }

    }

}
