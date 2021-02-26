using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    Animator b_animator;
    public GameObject Enemy_boss;
    public GameObject player;
    //int bossHP;
    //bool mov;
    //bool atk;
    //bool hurt;
    public bool Attackover;
    public bool atk_far_start;
    // Start is called before the first frame update
    void Start()
    {
        b_animator = this.transform.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        Attackover = false;
        atk_far_start = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region boss移動
        //mov = Enemy_boss.GetComponent<EnemyControl>().isMove;
        if (Enemy_boss.GetComponent<EnemyControl>().isMove == true)
        {
            b_animator.SetBool("isMove", true);
        }
        else
        {
            b_animator.SetBool("isMove", false);
        }
        #endregion

        #region boss攻擊
        //atk = Enemy_boss.GetComponent<EnemyControl>().attack;
        if (Enemy_boss.GetComponent<EnemyControl>().attack == true)
        {
            b_animator.SetBool("isAttack", true);
        }
        else
        {
            b_animator.SetBool("isAttack", false);
        }
        #endregion

        #region boss死亡
        //bossHP = GameManager.EnemyHP;
        if (GameManager.EnemyHP <= 0)
        {
            b_animator.SetBool("isDead",true);
        }
        #endregion

        #region boss受創
        //hurt = Enemy_boss.GetComponent<EnemyControl>().attacked;
        if (Enemy_boss.GetComponent<EnemyControl>().attacked == true)
        {
            b_animator.SetBool("isHit", true);
        }
        else
        {
            b_animator.SetBool("isHit", false);
        }
        #endregion

        #region boss遭到反擊
        if (Enemy_boss.GetComponent<EnemyControl>().counter==true)
        {
            b_animator.SetBool("isCount", true);
        }
        else
        {
            b_animator.SetBool("isCount",false);
        }
        #endregion

        #region boss嘴砲攻擊
        if (Enemy_boss.GetComponent<EnemyControl>().atk_far_start == true)
        {
            b_animator.SetBool("isAttackFar", true);
            //Enemy_boss.GetComponent<EnemyControl>().stopatk = false;
        }
        else
        {
            b_animator.SetBool("isAttackFar", false);
        }
        #endregion

    }
    #region 普攻停止判定
    void boss_atk_start()
    {
        Enemy_boss.GetComponent<EnemyControl>().stopatk = false;
    }
    void boss_atk_end()
    {
        Enemy_boss.GetComponent<EnemyControl>().stopatk=true;
    }
    #endregion

    #region 嘴砲生成＆停止判定
    void boss_atk_far()
    {
        Instantiate(Enemy_boss.GetComponent<EnemyControl>().bullet, Enemy_boss.GetComponent<EnemyControl>().bullet_pos.transform.position, new Quaternion(0, 0, 0, 0));//生成bullet
        Enemy_boss.GetComponent<EnemyControl>().atk_far_start = false;
        SoundManager.instance.BossAttackFarAudio();
        Enemy_boss.GetComponent<EnemyControl>().stopatk = false;
    }
    void boss_atk_far_end()
    {
        Enemy_boss.GetComponent<EnemyControl>().stopatk = true;
    }
    #endregion

    #region 死亡判定
    void dead()
    {
        //player.GetComponent<PlayerAnimation>().mov = false;
        b_animator.SetBool("isMove", false);
        Destroy(Enemy_boss);
    }
    #endregion
}
