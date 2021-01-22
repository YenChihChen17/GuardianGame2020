using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator p_animator;
    public GameObject player;
    bool atk;
    public GameObject Attackrange;
    // Start is called before the first frame update
    void Start()
    {
        p_animator = this.transform.GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        #region attack animation
        atk = player.GetComponent<PlayerControl>().attack;
        if (atk==true)
        {
            //Debug.Log("yes");
            p_animator.SetBool("isAttack", true);
        }
        else
        {
            p_animator.SetBool("isAttack", false);
        }
        #endregion
    }

    void AttackTime()
    {
        Attackrange.SetActive(true);
    }
    void AttackTimeOut()
    {
        Attackrange.SetActive(false);
    }
}
