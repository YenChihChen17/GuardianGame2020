using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject weapon;
    public float speed;
    public float StopPos;
    public float AttackedCoolDown;
    public float CounteredTime;
    public float HitHomeCoolDown;
    public float hitF;
    public float AttackPrepareTime;
    public float DistanceBetween;

    private float timer;
    private bool attacked;
    private bool attack;
    private bool HitHome;
    private bool counter;
    private float CounteredTimeP;
    private float HitTimer;
    private bool PlayerNearBy;
    private float Distance;

    public float stop_t;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Home = GameObject.Find("Home");
        counter = false;
        attack = false;
        attacked = false;
        CounteredTimeP = CounteredTime;
        HitHome = false;
        HitTimer = HitHomeCoolDown;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player")==true)
        {
            GameObject Player = GameObject.Find("Player");
            Distance = this.transform.position.x - Player.transform.position.x;
        }

        if (Distance <= DistanceBetween)
        {
            PlayerNearBy = true;
        }
        else if (Distance > DistanceBetween)
        {
            PlayerNearBy = false;
        }

        if (attacked == false && counter == false && HitHome == false && PlayerNearBy == false && attack == false ) // Boss 移動控制
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
            timer += Time.deltaTime;
            if (timer >= stop_t )
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
            attacked = true;
            timer = 0;
            GameManeger.EnemyHP = GameManeger.EnemyHP - GameManeger.Damage_P;
            Debug.Log("Enemy"+GameManeger.EnemyHP);
            if(this.transform.position.x - Player.transform.position.x >=0)
            {
                PlayerControl.AttackEnemy = true;
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0), ForceMode.Impulse);
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
            this.GetComponent<Rigidbody>().AddForce(new Vector3(hitF, 0, 0), ForceMode.Impulse);
            Debug.Log("Hit");
            GameManeger.HomeHP = GameManeger.HomeHP - GameManeger.Damage_E;
        }
    }
}
