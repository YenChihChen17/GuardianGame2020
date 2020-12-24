using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public Transform AttackPos;
    public Transform CounterPos;
    public float speed;
    public float hurtX;
    public float defendX;
    public GameObject AttackRange;
    public GameObject CounterRange;
    public float AtkTime;// 攻擊判定時間
    public float HurtTime;// 受傷判定時間
    public float CounterTime;// 反擊判定時間
    public float FallMutilpe;
    public float LowJumpMutilpe;
    public float JumpVelocity;
    public float acceleration;
    public float deceleration;

    private float a_timer;
    private float b_timer;
    private Rigidbody rig;
    private bool can_j;
    private bool hurt;
    private bool attack;
    private bool defend;
    private bool counter;
    private bool TurnAround; /// 判斷方向
    private Vector3 X;
    // Start is called before the first frame update
    void Start()
    {
        can_j = false;
        hurt = false;
        attack = false;
        defend = false;
        TurnAround = true;
        CounterRange.SetActive(false);
        AttackRange.SetActive(false);
        rig =this. GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {  
        Move();
        Attack();
        Defend();
        if(a_timer >= 0.2)// 攻擊判定存在時間
        {
            AttackRange.SetActive(false);
        }
        if (a_timer >= AtkTime && attack == true)//攻擊冷卻時間
        {
            attack = false;
        }

        if(hurt == true)
        {
            b_timer += Time.deltaTime;
            if (b_timer >= HurtTime && rig.velocity.x == 0f)
            {
                hurt = false;
            }
        }
        if(counter == true)
        {
            b_timer += Time.deltaTime;
            if(b_timer >= CounterTime)
            {
                CounterRange.SetActive(false);
                counter = false;
            }
        }
        Debug.Log(X);
        CounterPos.transform.position = this.transform.position;
        AttackPos.transform.position = this.transform.position;
        
    }

    private void Move() // 移動跳躍
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && hurt == false && can_j == true) ///給予加速度
        {
         X.x = Mathf.Lerp(X.x, speed, Time.deltaTime * acceleration);
         rig.velocity = X;
        }

        if (Input.GetKey(KeyCode.RightArrow) && TurnAround == false) //控制速度方向
        {
            target.transform.Rotate(new Vector3(0, 180, 0));
            AttackPos.transform.Rotate(new Vector3(0, 180, 0));
            CounterPos.transform.Rotate(new Vector3(0, 180, 0));
            TurnAround = true;
            if (X.x < 0)
            {
                X = X * -1;
                speed = speed * -1;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && TurnAround == true)//控制速度方向
        {
            target.transform.Rotate(new Vector3(0, 180, 0));
            AttackPos.transform.Rotate(new Vector3(0, 180, 0));
            CounterPos.transform.Rotate(new Vector3(0, 180, 0));
            TurnAround = false;
            if (X.x > 0)
            {
                X = X * -1;
                speed = speed * -1;
            }
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Mathf.Abs(X.x) >0 && can_j == true && hurt == false) //減速
        {
            X.x = Mathf.Lerp(X.x, 0, Time.deltaTime *deceleration);
            rig.velocity = X;
        }

        if (Input.GetKeyDown(KeyCode.Space) && can_j == true)
         {
             rig.velocity = Vector3.up * JumpVelocity;
         }
        if (can_j == false)
        {
            if (rig.velocity.y < 0)
            {
                rig.velocity += Vector3.up * Physics.gravity.y * (FallMutilpe - 1) * Time.deltaTime;
            }
           /* else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) 長按影響高度
            {
                rig.velocity += Vector3.up * Physics.gravity.y * (LowJumpMutilpe - 1) * Time.deltaTime;
            }*/
        }
    }
    
    private void OnCollisionExit(Collision collision)///離開地面
    {
        if (collision.gameObject.tag == "Ground")
            {
                can_j = false;
            }
    }

    private void OnCollisionEnter(Collision collision)///碰到地面
    {
        if (collision.gameObject.tag == "Ground")
        {
            can_j = true;
            hurt = false;
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy" && hurt == false && defend == false && counter == false )///撞到敵人
        {
            rig.velocity = new Vector3(0, 0, 0);
            rig.AddForce(new Vector3(-hurtX, 0, 0), ForceMode.Impulse);
            hurt = true;
            GameManeger.PlayerHP = GameManeger.PlayerHP - GameManeger.Damage_E;
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }
        if (Enemy.gameObject.tag == "Enemy" && hurt == false && defend== true && counter == false)///防禦敵人
        {
            rig.AddForce(new Vector3(-defendX, 0, 0), ForceMode.Impulse);
            hurt = true;
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }
        if (Enemy.gameObject.tag == "Enemy"  && counter == true)///防禦敵人
        {
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }

    }

    private void Attack()
    {
        a_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && attack == false && defend == false)
        {
            X.x = 0f;
            rig.velocity = new Vector3(0, 0, 0);
            AttackRange.SetActive(true);
            attack = true;
            a_timer = 0;
        }
    }
    private void Defend()
    {
        if (Input.GetKeyDown(KeyCode.S) && attack == false && hurt == false && defend == false)
        {
            X.x = 0f;
            rig.velocity = new Vector3(0, 0, 0);
            b_timer = 0;
            CounterRange.SetActive(true);
            counter = true;
            defend = true;
            Debug.Log("Defend");
        }
        if (Input.GetKeyUp(KeyCode.S) )
        {
            X.x = 0f;
            rig.velocity = new Vector3(0, 0, 0);
            defend = false;
            Debug.Log("Break");
        }

    }

}
