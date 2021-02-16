using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    Animator b_animator;
    public GameObject Enemy_boss;
    bool mov;
    bool atk;
    // Start is called before the first frame update
    void Start()
    {
        b_animator = this.transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region 移動
        mov = Enemy_boss.GetComponent<EnemyControl>().isMove;
        if (mov==true)
        {
            b_animator.SetBool("isMove", true);
        }
        else
        {
            b_animator.SetBool("isMove", false);
        }
        #endregion

        #region 攻擊
        atk = Enemy_boss.GetComponent<EnemyControl>().attack;
        if (atk==true)
        {
            b_animator.SetBool("isAttack", true);
        }
        else
        {
            b_animator.SetBool("isAttack", false);
        }
        #endregion
    }
}
