using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public Transform AttackPos;
    public Transform CounterPos;
    public float speed;
    public float SpeedX; // 起始速度
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
    private bool KeyBoard;

    private float a_timer;
    private float b_timer;
    private Rigidbody rig;
    private bool can_j;
    private bool hurt;
    public bool defend;
    private bool counter;
    private bool attack_timer;
    //動畫用
    public bool attack;
    public bool move;
    public bool jumping;
    public bool ground;

    private float SpeedY;
    private bool GameStart;
    private bool EnemyPos;
    private bool Right;
    private bool Left;
    private bool Jump;
    private bool DoAtk;
    private bool DoDf;
   // Start is called before the first frame update
    void Start()
    {
        KeyBoard = GameManeger.KeyBoardControl;
        can_j = false;
        hurt = false;
        attack = false;
        defend = false;
        GameStart = false;
        attack_timer = false;
        CounterRange.SetActive(false);
        AttackRange.SetActive(false);
        rig =this. GetComponent<Rigidbody>();
        AttackEnemy = false;
        Right = false;
        Left = false;
        DoAtk = false;
        DoDf = false;

    }

    // Update is called once per frame
    void Update()
    {
        CounterPos.transform.position = this.transform.position;
        AttackPos.transform.position = this.transform.position;

        Ray ray = new Ray(transform.position, transform.right); 
        RaycastHit hit;

        Move();
        Attack();
        Defend();

        if(hurt == true) // 受傷判定冷卻
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

        if (GameStart == true && hurt == false && AttackEnemy == false && GameManeger.PlayerHP >0) // 移動速度控制
        {
            rig.velocity = new Vector3(SpeedX, SpeedY, 0);
        }

        if (Physics.Raycast(ray, out hit)) // 偵測防禦方向是否正確
        {
            // 如果與物體發生碰撞，在Scene檢視中繪製射線  
            Debug.DrawLine(ray.origin, hit.point, Color.red);
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
        // Debug.Log(this.GetComponent<Rigidbody>().velocity);

        // Debug.Log(move);
        //Debug.Log(defend);


    }

    private void Move() // 移動跳躍
    {
     
        if (KeyBoard == true)
        {
            #region 鍵盤操控
            if (Input.GetKey(KeyCode.RightArrow) && defend == false && attack_timer == false && hurt == false) //控制角色方向
            {
                if (SpeedX < 0)
                {
                    SpeedX = SpeedX * -1;
                    speed = speed * -1;
                    target.transform.Rotate(new Vector3(0, 180, 0));
                    AttackPos.transform.Rotate(new Vector3(0, 180, 0));
                    CounterPos.transform.Rotate(new Vector3(0, 180, 0));
                }
                SpeedX = Mathf.Lerp(SpeedX, speed, Time.deltaTime * acceleration);
                move = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && defend == false && attack_timer == false && hurt == false)//控制角色方向
            {

                if (SpeedX > 0)
                {
                    SpeedX = SpeedX * -1;
                    speed = speed * -1;
                    target.transform.Rotate(new Vector3(0, 180, 0));
                    AttackPos.transform.Rotate(new Vector3(0, 180, 0));
                    CounterPos.transform.Rotate(new Vector3(0, 180, 0));
                }
                SpeedX = Mathf.Lerp(SpeedX, speed, Time.deltaTime * acceleration);
                move = true;
            }

            if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Mathf.Abs(SpeedX) > 0 && can_j == true && hurt == false) //放開控制鍵後減速
            {
                SpeedX = Mathf.Lerp(SpeedX, 0, Time.deltaTime * deceleration);
                move = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && can_j == true && defend == false) // 跳躍
            {
                SpeedY = JumpVelocity;
                jumping = true;
            }
            else if (can_j == false) //落下加速
            {
                if (SpeedY != 0)
                {
                    SpeedY += Physics.gravity.y * (FallMutilpe - 1) * Time.deltaTime;
                    jumping = true;
                }
                /* else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) 長按影響高度
                 {
                     rig.velocity += Vector3.up * Physics.gravity.y * (LowJumpMutilpe - 1) * Time.deltaTime;
                 }*/
            }

            #endregion
        }
        else
        {
            #region 虛擬鍵盤操控
            if (Right == true && defend == false && attack_timer == false && hurt == false) //控制角色方向, 虛擬搖桿操控
            {
                if (SpeedX < 0)
                {
                    SpeedX = SpeedX * -1;
                    speed = speed * -1;
                    target.transform.Rotate(new Vector3(0, 180, 0));
                    AttackPos.transform.Rotate(new Vector3(0, 180, 0));
                    CounterPos.transform.Rotate(new Vector3(0, 180, 0));
                }
                SpeedX = Mathf.Lerp(SpeedX, speed, Time.deltaTime * acceleration);
                move = true;
            }
            else if (Left == true && defend == false && attack_timer == false && hurt == false)//控制角色方向
            {
                if (SpeedX > 0)
                {
                    SpeedX = SpeedX * -1;
                    speed = speed * -1;
                    target.transform.Rotate(new Vector3(0, 180, 0));
                    AttackPos.transform.Rotate(new Vector3(0, 180, 0));
                    CounterPos.transform.Rotate(new Vector3(0, 180, 0));
                }
                SpeedX = Mathf.Lerp(SpeedX, speed, Time.deltaTime * acceleration);
                move = true;
            }

            if (Right == false && Left == false && Mathf.Abs(SpeedX) > 0 && can_j == true && hurt == false) //放開控制鍵後減速
            {
                SpeedX = Mathf.Lerp(SpeedX, 0, Time.deltaTime * deceleration);
                move = false;
            }


            if (Jump == true && can_j == true && defend == false) // 跳躍
            {
                SpeedY = JumpVelocity;
                jumping = true;
            }
            else if (can_j == false) //落下加速
            {
                Jump = false;
                if (SpeedY != 0)
                {
                    SpeedY += Physics.gravity.y * (FallMutilpe - 1) * Time.deltaTime;
                    jumping = true;
                }
                /* else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) 長按影響高度
                 {
                     rig.velocity += Vector3.up * Physics.gravity.y * (LowJumpMutilpe - 1) * Time.deltaTime;
                 }*/
            }
            if (ground==true)
            {
                jumping = false;
            }
            //Debug.Log(jumping);
            #endregion
        }
    }
    
    private void OnCollisionExit(Collision collision)///離開地面
    {
        if (collision.gameObject.tag == "Ground")
            {
                can_j = false;
                ground = false;
        }
    }

    private void OnCollisionEnter(Collision collision)///碰到地面
    {
        if (collision.gameObject.tag == "Ground")
        {
            can_j = true;
            GameStart = true;
            SpeedY = 0;
            ground = true;
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy" && hurt == false && defend == false )///撞到敵人
        {
            SpeedX = 0;
            rig.AddForce(new Vector3(-hurtX, 0, 0), ForceMode.Impulse);
            hurt = true;
            GameManeger.PlayerHP = GameManeger.PlayerHP - GameManeger.Damage_E;
            b_timer = 0;
            Debug.Log("Hurt");
        }

        else if (Enemy.gameObject.tag == "Enemy" && hurt == false && defend == true && EnemyPos == true)///防禦敵人
        {
            SpeedX = 0;
            rig.AddForce(new Vector3(-defendX, 0, 0), ForceMode.Impulse);
            hurt = true;
            b_timer = 0;
            Debug.Log("Defend");
        }

        else if (Enemy.gameObject.tag == "Enemy"  && defend == true && EnemyPos == true)///防禦敵人
        {
            SpeedX = 0;
            b_timer = 0;
            //Debug.Log("Player" + GameManeger.PlayerHP);
        }

    }

    public void Attack()
    {
        a_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && attack == false && defend == false && attack_timer == false)
        {
            SpeedX = 0;
            attack = true;
            a_timer = 0;
            attack_timer = true;
        }
        else if(DoAtk && attack == false && defend == false && attack_timer == false)
        {
            SpeedX = 0;
            attack = true;
            a_timer = 0;
            attack_timer = true;
        }

        if(a_timer >= 0.5)
        {
            attack = false;
        }
        
        if (a_timer >= AtkTime && attack_timer == true )//攻擊冷卻時間
        {
            AttackEnemy = false;
            attack_timer = false;
            DoAtk = false;
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
            GameManeger.PlayerMana = GameManeger.PlayerMana - GameManeger.ManaConsume;//Sonic Add 魔力消耗時機點為按下S時

        }
        else if (DoDf && attack == false && hurt == false && defend == false && can_j == true)
        {
            SpeedX = 0;
            b_timer = 0;
            counter = true;
            CounterRange.SetActive(true);
            defend = true;
            GameManeger.PlayerMana = GameManeger.PlayerMana - GameManeger.ManaConsume;//Sonic Add 魔力消耗時機點為按下S時
        }

        if (Input.GetKeyUp(KeyCode.S) )
        {
            defend = false;
        }
        else if (DoDf == false)
        {
            defend = false;
        }

    }

    #region 虛擬鍵盤控制
    public void GoRight()
    {
        Left = false;
        Right = true;
       // Debug.Log("Right");
    }
    public void GoLeft()
    {
        Right = false;
        Left = true;
       // Debug.Log("Left");
    }
    public void StopRight()
    {
        Right = false;
       // Debug.Log("Stop");
    }
    public void StopLeft()
    {
        Left = false;
        // Debug.Log("Stop");
    }
    public void DoJump()
    {
        Jump = true;
        // Debug.Log("Left");
    }
    public void DoAttack()
    {
        DoAtk = true;
    }
    public void DoDefend()
    {
        DoDf = true;
    }
    public void ResetDefend()
    {
        DoDf = false;
    }
    #endregion 

}
