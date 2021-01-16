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
    public static bool AttackEnemy;

    private float a_timer;
    private float b_timer;
    private Rigidbody rig;
    private bool can_j;
    private bool hurt;
    private bool attack;
    private bool defend;
    private bool counter;
    private bool TurnAround; /// 判斷方向
    private float SpeedX;
    private float SpeedY;
    private bool GameStart;
    private bool EnemyPos;
    private bool Right;
    private bool Left;
  //  private Vector3 X;
   // Start is called before the first frame update
    void Start()
    {
        can_j = false;
        hurt = false;
        attack = false;
        defend = false;
        GameStart = false;
        TurnAround = true;
        CounterRange.SetActive(false);
        AttackRange.SetActive(false);
        rig =this. GetComponent<Rigidbody>();
        AttackEnemy = false;
        Right = false;
        Left = false;

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.right); 
        RaycastHit hit;
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
            AttackEnemy = false;
        }

        if(hurt == true) // 受傷判定
        {
            b_timer += Time.deltaTime;
            if (b_timer >= HurtTime && rig.velocity.x == 0f)
            {
                hurt = false;
            }
        }
        if (counter == true) // 控制反擊collider 關閉
        {
            b_timer += Time.deltaTime;
            if (b_timer >= CounterTime)
            {
                CounterRange.SetActive(false);
                counter = false;
            }
        }
        CounterPos.transform.position = this.transform.position;
        AttackPos.transform.position = this.transform.position;

        if (GameStart == true && hurt == false && AttackEnemy == false) // 移動時速度控制
        {
            rig.velocity = new Vector3(SpeedX, SpeedY, 0);
        }

        if (Physics.Raycast(ray, out hit, 100.0f)) // 偵測防禦方向是否正確
        {
            // 如果與物體發生碰撞，在Scene檢視中繪製射線  
           //Debug.DrawLine(ray.origin, hit.point, Color.red);
            // 列印射線檢測到的物體的名稱  
            //Debug.Log( hit.transform.name);
            if(hit.collider.tag == "Enemy")
            {
                EnemyPos = true;
            }
            else
            {
                EnemyPos = false;
            }
        }
        //Debug.Log(EnemyPos);
    }

    private void Move() // 移動跳躍
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && hurt == false && defend == false ) ///給予加速度
        {
          SpeedX = Mathf.Lerp(SpeedX, speed, Time.deltaTime * acceleration);
        }
        if ((Right == true || Left == true) && hurt == false && defend == false) ///給予加速度, 虛擬搖桿操控
        {
            SpeedX = Mathf.Lerp(SpeedX, speed, Time.deltaTime * acceleration);
        }

        if (Input.GetKey(KeyCode.RightArrow) && TurnAround == false && defend == false) //控制角色方向
        {
            target.transform.Rotate(new Vector3(0, 180, 0));
            AttackPos.transform.Rotate(new Vector3(0, 180, 0));
            CounterPos.transform.Rotate(new Vector3(0, 180, 0));
            TurnAround = true;
            if (SpeedX < 0)
            {
                SpeedX = SpeedX * -1;
                speed = speed * -1;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && TurnAround == true && defend == false)//控制角色方向
        {
            target.transform.Rotate(new Vector3(0, 180, 0));
            AttackPos.transform.Rotate(new Vector3(0, 180, 0));
            CounterPos.transform.Rotate(new Vector3(0, 180, 0));
            TurnAround = false;
            if (SpeedX > 0)
            {
                SpeedX = SpeedX * -1;
                speed = speed * -1;
            }
        }

        if (Right == true && TurnAround == false && defend == false) //控制角色方向, 虛擬搖桿操控
        {
            target.transform.Rotate(new Vector3(0, 180, 0));
            AttackPos.transform.Rotate(new Vector3(0, 180, 0));
            CounterPos.transform.Rotate(new Vector3(0, 180, 0));
            TurnAround = true;
            if (SpeedX < 0)
            {
                SpeedX = SpeedX * -1;
                speed = speed * -1;
            }
        }
        
        else if (Left == true && TurnAround == true && defend == false)//控制角色方向
        {
            target.transform.Rotate(new Vector3(0, 180, 0));
            AttackPos.transform.Rotate(new Vector3(0, 180, 0));
            CounterPos.transform.Rotate(new Vector3(0, 180, 0));
            TurnAround = false;
            if (SpeedX > 0)
            {
                SpeedX = SpeedX * -1;
                speed = speed * -1;
            }
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Mathf.Abs(SpeedX) >0 && can_j == true && hurt == false) //放開控制鍵後減速
        {
            SpeedX = Mathf.Lerp(SpeedX, 0, Time.deltaTime *deceleration);
        }
        if (Right == false && Left == false && Mathf.Abs(SpeedX) > 0 && can_j == true && hurt == false) //放開控制鍵後減速
        {
            SpeedX = Mathf.Lerp(SpeedX, 0, Time.deltaTime * deceleration);
        }

        if (Input.GetKeyDown(KeyCode.Space) && can_j == true && defend == false) // 跳躍
         {
             SpeedY = JumpVelocity;
         }
        if (can_j == false) //落下加速
        {
            if (SpeedY != 0)
            {
                SpeedY += Physics.gravity.y * (FallMutilpe - 1) * Time.deltaTime;
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
            GameStart = true;
            SpeedY = 0;
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy" && hurt == false && counter == false)///撞到敵人
        {
            SpeedX = 0;
            rig.AddForce(new Vector3(-hurtX, 0, 0), ForceMode.Impulse);
            hurt = true;
            GameManeger.PlayerHP = GameManeger.PlayerHP - GameManeger.Damage_E;
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }
        if (Enemy.gameObject.tag == "Enemy" && hurt == false && defend== true && EnemyPos == true)///防禦敵人
        {
            SpeedX = 0;
            rig.AddForce(new Vector3(-defendX, 0, 0), ForceMode.Impulse);
            hurt = true;
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }
        if (Enemy.gameObject.tag == "Enemy"  && defend == true && EnemyPos == true)///防禦敵人
        {
            SpeedX = 0;
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }

    }

    private void Attack()
    {
        a_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && attack == false && defend == false)
        {
            SpeedX = 0;
            AttackRange.SetActive(true);
            attack = true;
            a_timer = 0;
        }
    }
    private void Defend() 
    {
        if (Input.GetKeyDown(KeyCode.S) && attack == false && hurt == false && defend == false && can_j == true)
        {
            SpeedX = 0;
            b_timer = 0;
            counter = true;
            CounterRange.SetActive(true);
            defend = true;
           // Debug.Log("Defend");
        }
        if (Input.GetKeyUp(KeyCode.S) )
        {
            defend = false;
           // Debug.Log("Break");
        }
    }
    public void GoRight()
    {
        Left = false;
        Right = true;
        Debug.Log("Right");
    }
    public void GoLeft()
    {
        Right = false;
        Left = true;
        Debug.Log("Left");
    }
    public void Stop()
    {
        Right = false;
        Left = false;
        Debug.Log("Stop");
    }
}
