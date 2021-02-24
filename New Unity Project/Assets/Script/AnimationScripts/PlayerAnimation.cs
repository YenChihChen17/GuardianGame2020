using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator p_animator;
    public GameObject player;
    public bool atk;
    public bool mov;
    public float movspeed;
    public bool jum;
    public bool isground;
    public bool def;
    public bool defsuc;
    public bool hurt;
    public GameObject Attackrange;
    public GameObject Counterrange;
    public Sprite shield_red;
    public Sprite shield_white;
    private bool Attacking;
    // Start is called before the first frame update
    void Start()
    {
        p_animator = this.transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region 角色攻擊動畫
        atk = player.GetComponent<PlayerControl>().attack;
        if (atk==true)
        {
            p_animator.SetBool("isAttack", true);
            
        }
        else
        {
            p_animator.SetBool("isAttack", false);
            
        }
        #endregion

        #region 角色移動動畫
        mov = player.GetComponent<PlayerControl>().move;
        movspeed =Mathf.Abs(player.GetComponent<PlayerControl>().SpeedX);
        p_animator.SetFloat("speed",movspeed);
        /*if (Mathf.Abs(movspeed) >1.7)
        {
            Debug.Log("SPEED:"+movspeed);
            p_animator.SetBool("isMove", true);
        }
        else
        {
            p_animator.SetBool("isMove", false);
        }*/
        /*if (Mathf.Abs(movSpeed)>0)
        {
            p_animator.SetFloat("movSpeed", movSpeed);
        }
        else if(Mathf.Abs(movSpeed) == 0)
        {
            p_animator.SetFloat("movSpeed", movSpeed);
        }*/
        #endregion

        #region 角色跳躍動畫
        jum = player.GetComponent<PlayerControl>().jumping;
        isground = player.GetComponent<PlayerControl>().ground;
        if (isground==false&&jum==true)
        {
            p_animator.SetBool("isJump", true);
        }
        else
        {
            p_animator.SetBool("isJump", false);
        }
        #endregion

        #region 角色防禦動畫
        def = player.GetComponent<PlayerControl>().defend;
        if (def == true)
        {
            p_animator.SetBool("isDef", true);
            p_animator.SetBool("isAttack", false);
            Counterrange.SetActive(true);
            Attackrange.SetActive(false);
            if (player.GetComponent<PlayerControl>().counter == true) //反擊有效期間符文變紅
            {
                Counterrange.GetComponent<SpriteRenderer>().sprite = shield_red;
            }
            else
            {
                Counterrange.GetComponent<SpriteRenderer>().sprite = shield_white;
            }
        }
        else
        {
            p_animator.SetBool("isDef", false);
            Counterrange.SetActive(false);
        }     
        #endregion

        #region 角色受傷動畫
        hurt = player.GetComponent<PlayerControl>().hurt;
        if (hurt==true && def==false)
        {
            p_animator.SetBool("isHit",true);
            Attackrange.SetActive(false);
        }
        else
        {
            p_animator.SetBool("isHit", false);
        }
        #endregion
    }
    #region 攻擊判定
    void OnAttackEnter()//進入攻擊判定
    {
        if (atk == true)
        {
            Attackrange.SetActive(true);//攻擊特效：20幀=2s
        }
    }
    void OnAttackExit()//離開攻擊判定
    {
        Attackrange.SetActive(false);
    }
    #endregion

    void atk_end()
    {
        player.GetComponent<PlayerControl>().attack = false;        
    }
}
