using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{     
    static public int PlayerHP ;
    static public int EnemyHP ;
    static public int Damage_P ;
    static public int Damage_E ;

    public GameObject PlayerClone;
    public GameObject Enemy;
    public GameObject Home;
    public GameObject GameOverUI;
    public GameObject YouDied;
    public float RebornT;
    public int CloneNum;

    public int Player_HP;
    public int Enemy_HP;
    public int Player_Damage;
    public int Enemy_Damage;

    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerHP = Player_HP;
        EnemyHP = Enemy_HP;
        Damage_P = Player_Damage;
        Damage_E = Enemy_Damage;
        Instantiate(PlayerClone, Home.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerHP <= 0 && CloneNum >=0) 
        {
            
                Destroy(GameObject.Find("PlayerContent(Clone)"));

            timer += Time.deltaTime;
            if (CloneNum>0)
            {
                YouDied.SetActive(true);
                if (timer >= RebornT)
                {
                    PlayerHP = Player_HP;
                    Debug.Log(PlayerHP);
                    CloneNum = CloneNum - 1;
                    timer = 0;
                    YouDied.SetActive(false);
                    Instantiate(PlayerClone,Home.transform.position, new Quaternion(0, 0, 0, 0));
                }
            }
        }
        if(EnemyHP<=0)
        {
            Destroy(Enemy);
        }
        if(CloneNum == 0 )
        {
            if (GameObject.Find("PlayerContent(Clone)") == false)
            {
                GameOverUI.SetActive(true);
            }
        }
    }
}
