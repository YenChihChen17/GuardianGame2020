using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRange_Ctrl : MonoBehaviour
{
    public bool defsuccess;

    private void OnTriggerStay(Collider Enemy)//敵人在防禦判定區
    {
        Debug.Log("ssss");
        if (Enemy.gameObject.tag == "Enemy")
        {
            defsuccess = true;
        }
    }
    private void OnTriggerExit(Collider Enemy)//敵人離開方玉判定區
    {
        Debug.Log("aaaa");
        if (Enemy.gameObject.tag == "Enemy")
        {
            defsuccess = false;
        }
    }
}
