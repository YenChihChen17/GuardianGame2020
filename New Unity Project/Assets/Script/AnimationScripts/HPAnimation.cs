using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAnimation : MonoBehaviour
{
    public GameObject player_HPbar;
    int player_HP;
    public int HPlimit;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = player_HPbar.transform.GetComponent<Animator>();
        HPlimit = 5;
    }

    // Update is called once per frame
    void Update()
    {
        player_HP = GameManeger.PlayerHP;
        if (player_HP < HPlimit)
        {
            animator.SetBool("isHPdown",true);
        }
        else
        {
            animator.SetBool("isHPdown", false);
        }
    }
}
