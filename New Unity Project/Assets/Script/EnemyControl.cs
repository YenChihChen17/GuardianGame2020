using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject weapon;
    public GameObject Player;
    public float speed;
    private float timer;
    public float cooldownS;
    private float cooldown;
    private float attackcool;
    private bool attacked;
    private bool attack;

    public float stop_t;
    public GameObject PopUpDamage;
    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        attacked = false;
        cooldown = cooldownS;
        attackcool = 0.5f;
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (attack == false)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                Attack();
                cooldown = cooldownS;
            }
        }
        if(attack == true)
        {
            attackcool -= Time.deltaTime;
            if (attackcool <= 0)///攻擊重置
            {
                weapon.transform.Rotate(new Vector3(0, 0, -90));
                attackcool = 0.5f;
                attack = false;
            }
        }
     
        if (attacked == false)
        {
            transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime, Space.World);
        }
        if (attacked == true)
        {
            cooldown = cooldownS;
            if (timer >=stop_t )
            {
                attacked = false;
            }
        }
    /// Debug.Log(cooldown);
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Weapon" && attacked == false)
        {
            GameObject Player = GameObject.Find("Player");
            GameObject mObject = (GameObject)Instantiate(PopUpDamage, transform.position + new Vector3 (Random.Range(-1,2),2,0), Quaternion.identity);
            mObject.GetComponent<Damage>().Value = GameManeger.Damage_P;
            attacked = true;
            timer = 0;
            GameManeger.EnemyHP = GameManeger.EnemyHP - GameManeger.Damage_P;
            Debug.Log("Enemy"+GameManeger.EnemyHP);
            if(this.transform.position.x - Player.transform.position.x >=0)
            {
                
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(-4, 0, 0), ForceMode.Impulse);
            }
            else
            {
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(
                    4, 0, 0), ForceMode.Impulse);
            }
        }
    }
    private void Attack()
    {
        weapon.transform.Rotate(new Vector3(0, 0, 90));
        attack = true;
        ///Debug.Log("Attack");
    }
}
