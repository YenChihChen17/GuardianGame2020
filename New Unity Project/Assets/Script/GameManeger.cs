using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{     
    static public int PlayerHP ;
    static public int EnemyHP ;
    static public int Damage_P ;
    static public int Damage_E ;
    static public int HomeHP;
    static public int MinnionHP;

    public GameObject PlayerClone;
    public GameObject Enemy;
    public GameObject Home;
    public GameObject PlayerRespawn;
    public GameObject GameOverUI;
    public GameObject YouDied;
    public GameObject Minnion;
    public GameObject EnemyRespawnPoint;
    public GameObject Boss;
    public float RebornT;
    public int CloneNum;
    public float[] Waves;
    public float BossBornTimer;

    public int Player_HP;
    public int Enemy_HP;
    public int Player_Damage;
    public int Enemy_Damage;
    public int Home_HP;
    public int Minnion_HP;

    private float timer;
    private bool Born;
    private bool Run;
    private float S;
    private int i = 0;
    private bool BossTime;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerHP = Player_HP;
        EnemyHP = Enemy_HP;
        HomeHP = Home_HP;
        Damage_P = Player_Damage;
        Damage_E = Enemy_Damage;
        MinnionHP = Minnion_HP;
        Born = false;
        Run = true;
        BossTime = true;
        Instantiate(PlayerClone, PlayerRespawn.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateMinnion();

        if (Run == false && BossTime == true)
        {
            BossBornTimer -= Time.deltaTime;
            if(BossBornTimer <= 0)
            {
                Instantiate(Boss, EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                BossTime = false;
            }
        }

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
                    Instantiate(PlayerClone,PlayerRespawn.transform.position, new Quaternion(0, 0, 0, 0));
                }
            }
        }

        if(CloneNum == 0 )
        {
            if (GameObject.Find("PlayerContent(Clone)") == false)
            {
                GameOverUI.SetActive(true);
            }
        }

        if(HomeHP<=0)
        {
            Destroy(Home);
        }
    }
    private void InstantiateMinnion()
    {
        if(Run == true)
        {
            if (Born == false)
            {
                if(i < Waves.Length)
                {
                    S = Waves[i];
                    Born = true;
                }
            }
            S -= Time.deltaTime;
            if (S < 0 && Born == true)
            {
                Instantiate(Minnion, EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                Born = false;
                if (i < Waves.Length)
                {
                    i++;
                }
            }
            if(i == Waves.Length)
            {
                Run = false;
            }
        }
    }
}
