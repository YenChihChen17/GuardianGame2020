using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public int PlayerHP;
    static public int EnemyHP;
    static public int Damage_P;
    static public int Damage_E;
    static public int HomeHP;
    static public int MinnionHP;
    static public int PlayerMana;//Sonic add
    static public int ManaConsume;//Sonic add 魔力消耗
    static public bool KeyBoardControl;

    static public float _Speed;
    static public float _SpeedX; // 起始速度
    static public float _Acceleration;
    static public float _Deceleration;
    static public float _hurtX;
    static public float _defendX;
    static public float _AtkTime;// 攻擊判定時間
    static public float _HurtTime;// 受傷判定時間
    static public float _CounterTime;// 反擊判定時間
    static public float _FallMutilpe;
    static public float _JumpVelocity;
    static public float _DefendCD;//防禦冷卻時間


    //private AudioSource audiosource;
    //public AudioClip PlayerDeadSE;
   // public AudioClip BossDeadSE;
    private bool DeadSE;
    private bool BDeadSE;

    [Header("波數間的時間間隔")]
    public float BreakTime;
    [Header("Boss 前的時間間隔")]
    public float BossBornTimer;
    [Header("切換虛擬鍵盤操作")]
    public bool KeyBoard;
    [Header("是否為Boss關")]
    public bool isBossStage;
    [Header("記得這裡要填符合MinionWveas 中的波數")]
    public int Waves;
    [Header("YouWin顯示前時間間隔")]
    public float YouWinTimer;

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
    private bool Wave1;
    private float StartTimer;
    // Start is called before the first frame update
    [System.Serializable]
    public struct GameObejct
    {
        public GameObject PlayerClone;
        public GameObject Boss;
        public GameObject FlyMinnion;
        public GameObject Minnion;
        public GameObject Home;
        public GameObject PlayerRespawn;
        public GameObject EnemyRespawnPoint;
        [Header("UI")]
        public GameObject GameOverUI;
        public GameObject YouDied;
        public GameObject Wave;
        public GameObject YouWin;
    }
    [System.Serializable]
    public struct MinionWaves
    {
        public float[] seconds;
        [Tooltip("0 是地面小兵, 1 是飛行小兵")]
        public int[] type;
    }
    [System.Serializable]
    public struct PlayerSetting
    {
        [Tooltip("玩家重生數")]
        public int CloneNum;
        [Tooltip("玩家重生時間")]
        public float RebornT;
        [Tooltip("水晶HP")]
        public int Home_HP;
        [Header("玩家資料")]
        public int Player_HP;
        public int Player_Damage;
        public int Player_Mana;//Sonic add
        public int Mana_consume;// 魔力消耗
        [Header("移動跳躍")]
        [Tooltip("最終速度")]
        public float speed;
        [Tooltip("起始速度速度")]
        public float SpeedX; // 起始速度
        [Tooltip("移動開始加速度")]
        public float acceleration;
        [Tooltip("移動結束加速度")]
        public float deceleration;
        [Tooltip("跳躍落下時向下加速度")]
        public float FallMutilpe;
        [Tooltip("跳躍時初始向上加速度")]
        public float JumpVelocity;
        [Header("受傷")]
        [Tooltip("受傷後受力")]
        public float hurtX;
        [Tooltip("防禦狀態下受傷後受力")]
        public float defendX;
        [Tooltip("受傷後多久可以移動")]
        public float HurtTime;// 受傷判定時間
        [Header("攻擊防禦")]
        [Tooltip("攻擊後多久可以移動")]
        public float AtkTime;// 攻擊判定時間
        [Tooltip("反擊判定持續時間")]
        public float CounterTime;// 反擊判定時間
        [Tooltip("防禦冷卻時間")]
        public float Defend_CD;
    }
    [System.Serializable]
    public struct EnemySetting
    {
        public int Boss_HP;
        public int Enemy_Damage;
        public int Minnion_HP;
    }
    public MinionWaves[] minionWaves;
    public PlayerSetting playerSetting;
    public EnemySetting enemySetting;
    public GameObejct gameobject;

    void Awake()
    {
        KeyBoardControl = KeyBoard;
        wavetimer = 0;
        i = 0;

        EnemyHP = enemySetting.Boss_HP;

        Damage_P = playerSetting.Player_Damage;
        Damage_E = enemySetting.Enemy_Damage;
        MinnionHP = enemySetting.Minnion_HP;
        Born = false;
        Run = false;
        BossTime = true;
        Instantiate(gameobject.PlayerClone, gameobject.PlayerRespawn.transform.position, new Quaternion(0, 0, 0, 0));
        Wave1 = false;

        #region 玩家狀態初始化
        HomeHP = playerSetting.Home_HP;
        PlayerHP = playerSetting.Player_HP;
        PlayerMana = playerSetting.Player_Mana;// Sonic add
        ManaConsume = playerSetting.Mana_consume;//Sonic Add
        _Speed = playerSetting.speed;
        _SpeedX = playerSetting.SpeedX;
        _Acceleration = playerSetting.acceleration;
        _Deceleration = playerSetting.deceleration;
        _hurtX = playerSetting.hurtX;
        _defendX = playerSetting.defendX;
        _AtkTime = playerSetting.AtkTime;
        _HurtTime = playerSetting.HurtTime;
        _CounterTime = playerSetting.CounterTime;
        _FallMutilpe = playerSetting.FallMutilpe ;
        _JumpVelocity = playerSetting.JumpVelocity;
        _DefendCD = playerSetting.Defend_CD;
        #endregion

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
        gameobject.Wave.GetComponent<Text>().text = "Wave 1";
        gameobject.Wave.SetActive(false);
        gameobject.YouWin.SetActive(false);
        gameobject.YouDied.SetActive(false);
        //audiosource = this.GetComponent<AudioSource>();
        DeadSE = false;
        BDeadSE = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Wave1 == false)
        {
            StartTimer += Time.deltaTime;
            //Debug.Log(StartTimer);
            if(StartTimer >2.5f)
            {
                Wave1 = true;
                Run = true;
                gameobject.Wave.SetActive(true);
            }
        }

        InstantiateMinnion();

        if (Run == false && BossTime == true && GameObject.FindWithTag("Enemy") == false && isBossStage && Wave1 == true)
        {
            gameobject.Wave.SetActive(true);
            gameobject.Wave.GetComponent<Text>().text = "Warning";
            float alpha = Mathf.PingPong(1 * Time.time, 1);
            gameobject.Wave.GetComponent<Text>().color=new Color(100, 0, 0, alpha);

            BossBornTimer -= Time.deltaTime;
            if(BossBornTimer <= 0)
            {
                Instantiate(gameobject.Boss, gameobject.EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                BossTime = false;
            }
        }
        else if (Run == false && BossTime == true && isBossStage == false && GameObject.FindWithTag("Enemy") == false)
        {
            gameobject.YouWin.SetActive(true);
        }

        if (PlayerHP <= 0 && playerSetting.CloneNum >=0) 
        {
            
            Destroy(GameObject.Find("PlayerContent(Clone)"));

            timer += Time.deltaTime;
            if (playerSetting.CloneNum > 0)
            {
                gameobject.YouDied.SetActive(true);
                float alpha = Mathf.PingPong(2* Time.time, 1);
                gameobject.YouDied.GetComponentInChildren<Text>().color = new Color(1, 1, 1, alpha);
                if(DeadSE == false)
                {
                    SoundManager.instance.Player_Dead();
                    DeadSE = true;
                }
                if (timer >= playerSetting.RebornT)
                {
                    PlayerHP = playerSetting.Player_HP;
                    //Debug.Log(PlayerHP);
                    playerSetting.CloneNum = playerSetting.CloneNum - 1;
                    timer = 0;
                    gameobject.YouDied.SetActive(false);
                    Instantiate(gameobject.PlayerClone, gameobject.PlayerRespawn.transform.position, new Quaternion(0, 0, 0, 0));
                    DeadSE = false;
                }
            }
        }  
        else if (HomeHP<=0)
        {
            Destroy(gameobject.Home);
            gameobject.GameOverUI.SetActive(true);
        }
        else if(EnemyHP<=0)
        {
            gameobject.Boss = GameObject.FindWithTag("Enemy");
            //Destroy(gameobject.Boss);
            YouWinTimer -= Time.deltaTime;
            if(BDeadSE == false)
            {
                SoundManager.instance.BossDeadAudio();
                BDeadSE = true;
            }
            //gameobject.YouWin.SetActive(true);
            if (YouWinTimer <= 0)
            {
                gameobject.YouWin.SetActive(true);
            }
        }
        else if (playerSetting.CloneNum == 0)
        {
            if (GameObject.Find("PlayerContent(Clone)") == false)
            {
                gameobject.GameOverUI.SetActive(true);
            }
        }


        if (GameObject.FindWithTag("Enemy"))
        {
            gameobject.Wave.SetActive(false);
        }
    }
    private void InstantiateMinnion()
    {
        if(Run == true)
        {
            if(Born == false && GameObject.FindWithTag("Enemy") == false)
            {
                gameobject.Wave.SetActive(true);
                gameobject.Wave.GetComponent<Text>().text = "Wave " + (i + 1);//出現Wave字樣
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
                    //Debug.Log("NextWaves");
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
                    Instantiate(gameobject.Minnion, gameobject.EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    //Debug.Log(a);
                    inCal = true;
                    Born = false;
                    a++;
                }
                else if (type == 1 && Born == true)
                {
                    Instantiate(gameobject.FlyMinnion, gameobject.EnemyRespawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
                    //Debug.Log(a);
                    inCal = true;
                    Born = false;
                    a++;
                }
            }
        }
    }
}
