using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject Home;
    public GameObject weapon;
    public GameObject Player;
    public GameObject Enemy;
    public float speed;
    public float StopPos;
    public float AttackedCoolDown;
    public float CounteredTime;
    public float HitHomeCoolDown;
    public float hitF;

    private float timer;
    private float cooldown;
    private float attackcool;
    private bool attacked;
    private bool attack;
    private bool HitHome;
    private float PosX;
    private bool counter;
    private float CounteredTimeP;
    private float HitTimer;

    public float stop_t;
    public GameObject PopUpDamage;
    // Start is called before the first frame update
    void Start()
    {
        counter = false;
        attack = false;
        attacked = false;
        cooldown = AttackedCoolDown;
        attackcool = 0.5f;
        CounteredTimeP = CounteredTime;
        HitHome = false;
        HitTimer = HitHomeCoolDown;
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       // PosX = this.transform.position.x - Home.transform.position.x; // 計算與家的距離

        if (attack == false)// Boss攻擊控制
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                Attack();
                cooldown = AttackedCoolDown;
            }
        }
        else if(attack == true && counter == false) // Boss攻擊控制
        {
            attackcool -= Time.deltaTime;
            if (attackcool <= 0)///攻擊重置
            {
                weapon.transform.Rotate(new Vector3(0, 0, -90));
                attackcool = 1f;
                attack = false;
            }
        }
     
        if (attacked == false && counter == false && HitHome == false ) // Boss 移動控制
        {
            transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime, Space.World);
        }
        else if (counter == true)//Boss 被反擊時
        {
            CounteredTime -= Time.deltaTime;
            if (CounteredTime<=0)
            {
                counter = false;
                CounteredTime = CounteredTimeP;
            }
        }

        if(HitHome == true) // 打到基地被彈飛後的計時器
        {
            HitTimer -= Time.deltaTime;
            if(HitTimer <= 0)
            {
                HitHome = false;
                HitTimer = HitHomeCoolDown;
            }
        }

        if (attacked == true) // 被攻擊計時器
        {
            cooldown = AttackedCoolDown;
            if (timer >=stop_t )
            {
                attacked = false;
            }
        }
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Weapon" && attacked == false)
        {
            GameObject Player = GameObject.Find("Player");
            /*GameObject mObject = (GameObject)Instantiate(PopUpDamage, transform.position + new Vector3 (Random.Range(-1,2),2,0), Quaternion.identity);//產生傷害數字
            mObject.GetComponent<Damage>().Value = GameManeger.Damage_P;*/
            attacked = true;
            timer = 0;
            GameManeger.EnemyHP = GameManeger.EnemyHP - GameManeger.Damage_P;
            Debug.Log("Enemy"+GameManeger.EnemyHP);
            if(this.transform.position.x - Player.transform.position.x >=0)
            {
                PlayerControl.AttackEnemy = true;
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 0, 0), ForceMode.Impulse);
            }
            else
            {
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(
                    5, 0, 0), ForceMode.Impulse);
                PlayerControl.AttackEnemy = true;
            }
        }
        if (PW.gameObject.tag == "Counter" && attack == true )
        {
            attacked = true;
            Debug.Log("Countered");
            counter = true;
        }
        if (PW.gameObject.tag == "Home")
        {
            HitHome = true;
            Enemy.GetComponent<Rigidbody>().AddForce(new Vector3(hitF, 0, 0), ForceMode.Impulse);
            Debug.Log("Hit");
            GameManeger.HomeHP = GameManeger.HomeHP - GameManeger.Damage_E;
        }

    }
    private void Attack()
    {
        weapon.transform.Rotate(new Vector3(0, 0, 90));
        attack = true;
    }
}
