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
    public float AtkTime;

    private float a_timer;
    private Rigidbody rig;
    private bool can_j;
    private bool ishurt;
    private bool isattack;
    private bool TurnAround; /// 判斷方向
    // Start is called before the first frame update
    void Start()
    {
        can_j = true;
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
        AttackPos.transform.position = this.transform.position;

    }

    private void Move()
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

        if (Input.GetKey(KeyCode.Space) && can_j == true)///跳
        {
            rig.AddForce(new Vector3(0, jumpF, 0), ForceMode.Impulse);
            can_j = false;
            ///  Debug.Log(can_j);
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
        if (Input.GetKey(KeyCode.A))
        {
            AttackRange.SetActive(true);
            isattack = true;
            a_timer = 0;
        }
        if(a_timer >= AtkTime && isattack == true)
        {
            AttackRange.SetActive(false);
            isattack = false;
        }
    }


}
