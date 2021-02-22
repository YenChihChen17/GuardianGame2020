using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_bar_appear : MonoBehaviour
{
    [Header("敵人被攻擊時,才會顯示血量條")]
    public GameObject BarActive;
    // Start is called before the first frame update
    void Start()
    {
        BarActive.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


    }
   
}