using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{     
    static public int PlayerHP = 10;
    static public int EnemyHP = 100;
    static public int Damage_P = 5;
    static public int Damage_E = 5;

    public GameObject Player;
    public GameObject Enemy;
    public GameObject PlayerP;
    public GameObject Home;

    public GameObject GameOverUI;
    public GameObject YouDied;

    public float RebornT;
    public int CloneNum;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerHP <= 0 && CloneNum >=0) 
        {
            if (GameObject.Find("Player") == true)
            {
                Destroy(Player);
            }
            else
            {
                Destroy(GameObject.Find("Player(Clone)"));
            }

            timer += Time.deltaTime;
            if (CloneNum>0)
            {
                YouDied.SetActive(true);
                if (timer >= RebornT)
                {
                    PlayerHP = 20;
                    Debug.Log(PlayerHP);
                    CloneNum = CloneNum - 1;
                    timer = 0;
                    YouDied.SetActive(false);
                    Instantiate(PlayerP,Home.transform.position, new Quaternion(0, 0, 0, 0));
                }
            }
        }
        if(EnemyHP<=0)
        {
            Destroy(Enemy);
        }
        if(CloneNum == 0 )
        {
            if (GameObject.Find("Player(Clone)") == false)
            {
                GameOverUI.SetActive(true);
            }
        }
    }
}
