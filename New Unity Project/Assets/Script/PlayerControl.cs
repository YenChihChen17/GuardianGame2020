using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public Transform AttackPos;
    public float speed;
    public float jumpF;
    public float hurtX;
    public float hurtY;
    public GameObject AttackRange;
    public float AtkTime;// 攻擊判定時間
    public float FallMutilpe;
    public float LowJumpMutilpe;
    public float JumpVelocity;

    private float a_timer;
    private Rigidbody rig;
    private bool can_j;
    private bool ishurt;
    private bool isattack;
    private bool TurnAround; /// 判斷方向

    // Start is called before the first frame update
    void Start()
    {
        can_j = false;
        ishurt = false;
        isattack = false;
        TurnAround = true;
        AttackRange.SetActive(false);
        rig =this. GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        a_timer += Time.deltaTime;
        if(a_timer >= 0.2)// 攻擊判定存在時間
        {
            AttackRange.SetActive(false);
        }
        if (a_timer >= AtkTime && isattack == true)//攻擊冷卻時間
        {
            isattack = false;
        }
        AttackPos.transform.position = this.transform.position;
    }

    private void Move() // 移動跳躍
    {
        float xm = 0;

        if (Input.GetKey(KeyCode.RightArrow) && ishurt == false) ///移動
        {
            if (TurnAround == false)
            {
                target.transform.Rotate(new Vector3(0, 180, 0));
                AttackPos.transform.Rotate(new Vector3(0, 180, 0));
                TurnAround = true;
            }
            xm += speed * Time.deltaTime;
            target.Translate(new Vector3(xm,0,0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && ishurt == false)///移動
        {
            if (TurnAround == true)
            {
                target.transform.Rotate(new Vector3(0, 180, 0));
                AttackPos.transform.Rotate(new Vector3(0, 180, 0));
                TurnAround = false;
            }
            xm += speed * Time.deltaTime;
            target.Translate(new Vector3(xm, 0, 0));
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
            else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rig.velocity += Vector3.up * Physics.gravity.y * (LowJumpMutilpe - 1) * Time.deltaTime;
            }
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
            ishurt = false;
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy" && ishurt == false)///撞到敵人
        {
            rig.AddForce(new Vector3(-hurtX, hurtY, 0), ForceMode.Impulse);
            ishurt = true;
            GameManeger.PlayerHP = GameManeger.PlayerHP - GameManeger.Damage_E;
            Debug.Log("Player" + GameManeger.PlayerHP);
        }
    }

    private void Attack()
    {
        a_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && isattack == false)
        {
            AttackRange.SetActive(true);
            isattack = true;
            a_timer = 0;
        }
    }


}
