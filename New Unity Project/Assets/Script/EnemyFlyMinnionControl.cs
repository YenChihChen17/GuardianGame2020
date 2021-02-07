using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyMinnionControl : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float hitF;
    public GameObject Whole;

    public int HP;
    private bool Dead;
    private int up;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        HP = GameManeger.MinnionHP;
        up = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >=1.5)
        {
            up = up * -1;
            timer = 0;
        }
        if (Dead == false)
        {
            transform.Translate(new Vector3(-speedX, 0, 0) * Time.deltaTime, Space.World);

            if(up == 1)
            {
                transform.Translate(new Vector3(0, speedY, 0) * Time.deltaTime, Space.World);
            }
            else if (up == -1)
            {
                transform.Translate(new Vector3(0, -speedY, 0) * Time.deltaTime, Space.World);
            }
            
        }

        if (HP <= 0)
        {
            Destroy(GetComponent<Collider>());
            Destroy(Whole.gameObject);
            Dead = true;
        }
        //Debug.Log(HP);
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Weapon")
        {
            GameObject Player = GameObject.Find("Player");
            HP = HP - GameManeger.Damage_P;
            if (this.transform.position.x - Player.transform.position.x >= 0)
            {
                PlayerControl.AttackEnemy = true;
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(-hitF, 0, 0), ForceMode.Impulse);
            }
            else
            {
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(hitF, 0, 0), ForceMode.Impulse);
                PlayerControl.AttackEnemy = true;
            }
        }

        if (PW.gameObject.tag == "Home")
        {
            Destroy(this.gameObject);
            GameManeger.HomeHP = GameManeger.HomeHP - GameManeger.Damage_E;
        }

    }
}
