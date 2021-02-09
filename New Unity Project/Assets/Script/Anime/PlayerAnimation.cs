using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator p_animator;
    public GameObject player;
    bool atk;
    bool mov;
    bool jum;
    bool isground;
    bool def;
    bool defsuc;
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
            //Debug.Log("OK");
            p_animator.SetBool("isAttack", true);

        }
        else
        {
            p_animator.SetBool("isAttack", false);
        }
        #endregion
        #region 角色移動動畫
        mov = player.GetComponent<PlayerControl>().move;
        if (mov == true)
        {
            //Debug.Log("OK");
            p_animator.SetBool("isMove", true);

        }
        else
        {
            p_animator.SetBool("isMove", false);
        }
        #endregion
        #region 角色跳躍動畫
        jum = player.GetComponent<PlayerControl>().jumping;
        isground = player.GetComponent<PlayerControl>().ground;
        if (isground==false&&jum==true)
        {
            //Debug.Log("OK");
            p_animator.SetBool("isJump", true);

        }
        else
        {
            p_animator.SetBool("isJump", false);
        }
        #endregion
        #region 角色防禦動畫
        def = player.GetComponent<PlayerControl>().defend;
        Counterrange.GetComponent<SpriteRenderer>().sprite = shield_red;
        if (def == true/*Input.GetKeyDown(KeyCode.S)*/)
        {
            //Debug.Log("OK");
            p_animator.SetBool("isDef", true);
            Counterrange.SetActive(true);
            Counterrange.GetComponent<SpriteRenderer>().sprite = shield_white;
        }
        else
        {
            p_animator.SetBool("isDef", false);
            Counterrange.SetActive(false);
            Counterrange.GetComponent<SpriteRenderer>().sprite = shield_white;
        }
        defsuc = player.GetComponent<PlayerControl>().defsuccess;
        if (defsuc==true)
        {
            Counterrange.GetComponent<SpriteRenderer>().sprite = shield_red;
        }
        else
        {
            Counterrange.GetComponent<SpriteRenderer>().sprite = shield_white;
        }
        #endregion
    }
    #region 攻擊判定
    void OnAttackEnter()//進入攻擊判定
    {
        Attackrange.SetActive(true);//攻擊特效：20幀=2s
    }
    void OnAttackExit()//離開攻擊判定
    {
        Attackrange.SetActive(false);
    }
    #endregion


}
