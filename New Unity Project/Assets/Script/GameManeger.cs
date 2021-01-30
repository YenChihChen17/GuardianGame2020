using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    static public int PlayerHP;
    static public int EnemyHP;
    static public int Damage_P;
    static public int Damage_E;
    static public int HomeHP;
    static public int MinnionHP;
    static public int PlayerMana;//Sonic add
    static public int ManaConsume;//Sonic add 魔力消耗

    public GameObject PlayerClone;
    public GameObject Enemy;
    public GameObject Home;
    public GameObject PlayerRespawn;
    public GameObject GameOverUI;
    public GameObject YouDied;
    public GameObject Minnion;
    public GameObject EnemyRespawnPoint;
    public GameObject Boss;
    public GameObject FlyMinnion;
    public float BreakTime;
    public int Waves;
    public float BossBornTimer;

    private float timer;
    private float wavetimer;
    private bool Born;
    private bool Run;
    private float S;
    private int i ;
    private bool BossTime;
    private int type;
    private bool inCal;
    private int a ;
    // Start is called before the first frame update
    [System.Serializable]
    public struct MinionWaves
    {
        public float[] seconds;
        public int[] type;
    }
    [System.Serializable]
    public struct PlayerSetting
    {
        public int Player_HP;
        public int Player_Damage;
        public int Player_Mana;//Sonic add
        public int CloneNum;
        public float RebornT;
        public int Mana_consume;// 魔力消耗
        public int Home_HP;
    }
    [System.Serializable]
    public struct EnemySetting
    {
        public int Enemy_HP;
        public int Enemy_Damage;
        public int Minnion_HP;
    }
    public MinionWaves[] minionWaves;
    public PlayerSetting playerSetting;
    public EnemySetting enemySetting;

    void Awake()
    {
        wavetimer = 0;
        i = 0;
        PlayerHP = playerSetting.Player_HP;
        PlayerMana = playerSetting.Player_Mana;// Sonic add
        ManaConsume = playerSetting.Mana_consume;//Sonic Add
        EnemyHP = enemySetting.Enemy_HP;
        HomeHP = playerSetting.Home_HP;
        Damage_P = playerSetting.Player_Damage;
        Damage_E = enemySetting.Enemy_Damage;
        MinnionHP = enemySetting.Minnion_HP;
        Born = false;
        Run = true;
        BossTime = true;
        Instantiate(PlayerClone, PlayerRespawn.transform.position, new Quaternion(0, 0, 0, 0));
    }
    private void Start()
    {
       /* WavesAndNum = new Dictionary<int, int>();
        for (int i=0;i<minionWaves.Length; i++)
        {
            if(!WavesAndNum.ContainsKey(minionWaves[i].waves))
            {
                WavesAndNum.Add(minionWaves[i].waves, minionWaves[i].num);
            }
        }
       */
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

        if (PlayerHP <= 0 && playerSetting.CloneNum >=0) 
        {
            
                Destroy(GameObject.Find("PlayerContent(Clone)"),1);

            timer += Time.deltaTime;
            if (playerSetting.CloneNum > 0)
            {
                YouDied.SetActive(true);
                if (timer >= playerSetting.RebornT)
                {
                    PlayerHP = playerSetting.Player_HP;
                    Debug.Log(PlayerHP);
                    playerSetting.CloneNum = playerSetting.CloneNum - 1;
                    timer = 0;
                    YouDied.SetActive(false);
                    Instantiate(PlayerClone,PlayerRespawn.transform.position, new Quaternion(0, 0, 0, 0));
                }
            }
        }

        if(playerSetting.CloneNum == 0 )
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
            if(Born == false)
            {
                wavetimer += Time.deltaTime;
                if (wavetimer >= BreakTime)
                {
                    Born = true;
                    wavetimer = 0;
                    inCal = true;
                }
            }

            if (inCal == true )
            {
                if(a < minionWaves[i].seconds.Length)
                {
                    S = minionWaves[i].seconds[a];
                    type = minionWaves[i].type[a];
                    Born = true;
                    inCal = false;
                }
                else if (a == minionWaves[i].seconds.Length)
                {
                    Born = false;
                    a = 0;
                    wavetimer = 0;
                    inCal = false;
                    Debug.Log("NextWaves");
                    i++;
                    if (i == Waves)
                    {
                        Run = false;
                        BossTime = true;
                    }
                }
            }

            S -= Time.deltaTime;
            if (S < 0)
            {
                if(type ==0 && Born == true)
                {
                    Instantiate(Minnion, EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    Debug.Log(a);
                    inCal = true;
                    Born = false;
                    a++;
                }
                else if (type == 1 && Born == true)
                {
                    Instantiate(FlyMinnion, EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    Debug.Log(a);
                    inCal = true;
                    Born = false;
                    a++;
                }
            }
        }
    }
}
