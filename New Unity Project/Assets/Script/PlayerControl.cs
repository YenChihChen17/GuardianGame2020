using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Rigidbody rig;
    public float jumpF;
    public float hurtX;
    public float hurtY;
    private bool can_j;
    private bool ishurt;
    private bool isattack;
    public GameObject sword;
    public GameObject swordcol;
    private float a_timer;
    public float attackcooldown;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        can_j = true;
        ishurt = false;
        isattack = false;
        swordcol.GetComponent<Collider>().enabled = false;
        /// Debug.Log(PlayerHP);
    }

    // Update is called once per frame
    void Update()
    {
        a_timer += Time.deltaTime;
        float xm = 0;
        if (Input.GetKey(KeyCode.RightArrow)&& ishurt==false) ///移動
        {
            xm += speed * Time.deltaTime;
        }
        target.Translate(new Vector3(xm, 0, 0));
        if (Input.GetKey(KeyCode.LeftArrow))///移動
        {
            xm -= speed * Time.deltaTime;
        }
       
        
        if (Input.GetKey(KeyCode.Space)&& can_j == true)///跳
        {
            rig.AddForce(new Vector3(0, jumpF, 0), ForceMode.Impulse);
            can_j = false;
          ///  Debug.Log(can_j);
        }
        if (Input.GetKeyDown(KeyCode.A) && isattack == false)///攻擊觸發
        {
            Attack();
            a_timer = 0;
        }
        if (a_timer >= attackcooldown && isattack == true)///攻擊重置
        {
            sword.transform.Rotate(new Vector3(0, 0, 90));
            isattack = false;
            swordcol.GetComponent<Collider>().enabled = false;
        }
       /// Debug.Log(can_j);
        target.Translate(new Vector3(xm, 0, 0));
        ///Debug.Log(a_timer);
        
    }
    private void OnCollisionEnter(Collision collision) {///判斷是否在地板上
        if (collision.gameObject.tag == "Ground")
        {
            can_j = true;
            ishurt = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
         if (collision.gameObject.tag == "Ground")
            {
                can_j = false;
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
            ///Debug.Log("Hurt");
        }
    }

    private void Attack() {///劍旋轉
        swordcol.GetComponent<Collider>().enabled = true; ///關閉武器碰撞
        sword.transform.Rotate(new Vector3(0, 0, -90));
        isattack = true;
    }

}
