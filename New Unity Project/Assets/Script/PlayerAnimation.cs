using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator p_animator;
    public GameObject player;
    bool atk;
    public GameObject Attackrange;

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
