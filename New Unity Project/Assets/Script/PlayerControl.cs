using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public Transform AttackPos;
    public Transform CounterPos;
    public GameObject AttackRange;
    public GameObject CounterRange;

    private float speed;
    private float SpeedX; // 起始速度
    private float hurtX;
    private float defendX;
    private float AtkTime;// 攻擊判定時間
    private float HurtTime;// 受傷判定時間
    private float CounterTime;// 反擊判定時間
    private float DefendCD;// 防禦冷卻時間
    private float FallMutilpe;
    //public float LowJumpMutilpe;
    private float JumpVelocity;
    private float acceleration;
    private float deceleration;
    public static bool AttackEnemy;

    private bool KeyBoard;
    private float a_timer;
    private float b_timer;
    private float d_timer; //防禦冷卻計時
    private Rigidbody rig;
    private bool can_j;
    public bool hurt;
    public bool defend;
    public bool counter;
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
    //AudioSource audiosource;
    void Start()
    {
        KeyBoard = GameManager.KeyBoardControl;
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

        //audiosource = this.GetComponent < AudioSource > (); // 初始化AudioSource

        speed = GameManager._Speed;
        SpeedX = GameManager._SpeedX;
        hurtX = GameManager._hurtX;
        defendX = GameManager._defendX;
        AtkTime = GameManager._AtkTime;
        HurtTime = GameManager._HurtTime;
        CounterTime = GameManager._CounterTime;
        FallMutilpe = GameManager._FallMutilpe;
        JumpVelocity = GameManager._JumpVelocity;
        acceleration = GameManager._Acceleration;
        deceleration = GameManager._Deceleration;
        DefendCD = GameManager._DefendCD;
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
            if (b_timer >= HurtTime )
            {
                hurt = false;
            }
        }
        else if (GameStart == true && attack == false && hurt == false && GameManager.PlayerHP > 0) // 移動速度控制
        {
            rig.velocity = new Vector3(SpeedX, SpeedY, 0);
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
        Debug.Log(DoAtk);
    }

    private void Move() // 移動跳躍
    {
     
        if (KeyBoard == true)
        {
            #region 鍵盤操控
            if (Input.GetKey(KeyCode.RightArrow) && defend == false && hurt == false && attack == false) //控制角色方向
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
            else if (Input.GetKey(KeyCode.LeftArrow) && defend == false && hurt == false && attack == false)//控制角色方向
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
            if (Right == true && defend == false && hurt == false && attack == false) //控制角色方向, 虛擬搖桿操控
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
            else if (Left == true && defend == false && hurt == false && attack == false)//控制角色方向
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
            SoundManager.instance.Player_JumpUp();
            can_j = false;
            ground = false;
            //audiosource.PlayOneShot(JumpUp);
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
            //audiosource.PlayOneShot(JumpDown);
            SoundManager.instance.Player_JumpDown();
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy" && hurt == false && defend == false )///撞到敵人
        {
            SoundManager.instance.Player_Hurt();
            SpeedX = 0;
            rig.AddForce(new Vector3(-hurtX, 0, 0), ForceMode.Impulse);
            hurt = true;
            GameManager.PlayerHP = GameManager.PlayerHP - GameManager.Damage_E;
            b_timer = 0;
            //audiosource.PlayOneShot(HurtSE);
            move = false;
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
            //Debug.Log("Player" + GameManager.PlayerHP);
        }

    }

    public void Attack()
    {
        a_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && hurt == false && attack == false && defend == false && attack_timer == false)
        {
            SpeedX = 0;
            attack = true;
            SoundManager.instance.Player_Attack();
            a_timer = 0;
            attack_timer = true;
        }
        else if(DoAtk && attack == false && hurt == false && defend == false && attack_timer == false )
        {
            SpeedX = 0;
            attack = true;
            SoundManager.instance.Player_Attack();
            a_timer = 0;
            attack_timer = true;
        }

        if (a_timer >= AtkTime && attack_timer == true )//攻擊冷卻時間
        {
            attack = false;
            DoAtk = false; 
            attack_timer = false;
        }
    }
    private void Defend() 
    {
        d_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S) && attack == false && hurt == false && defend == false && can_j == true)
        {
            SoundManager.instance.Player_Defense();
            SpeedX = 0;
            b_timer = 0;
            counter = true;
            defend = true;
            GameManager.PlayerMana = GameManager.PlayerMana - GameManager.ManaConsume;//Sonic Add 魔力消耗時機點為按下S時
            d_timer = 0;
        }
        else if (DoDf && attack == false && hurt == false && defend == false && can_j == true)
        {
            SoundManager.instance.Player_Defense();
            SpeedX = 0;
            b_timer = 0;
            counter = true; 
            defend = true;
            GameManager.PlayerMana = GameManager.PlayerMana - GameManager.ManaConsume;//Sonic Add 魔力消耗時機點為按下S時
            d_timer = 0;
        }

        if(d_timer > DefendCD)
        {
            defend = false;
        }


        if (Input.GetKeyUp(KeyCode.S))
        {
            defend = false;
        }
        else if (DoDf == false && GameManager.KeyBoardControl==false)//當不使用鍵盤時才判定防禦虛擬按鍵
        {
            defend = false;
        }

        if (GameManager.PlayerMana <= -1) {
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
        attack = true;
    }
    public void StopAttack()
    {
        DoAtk = false;
        attack = false;
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
