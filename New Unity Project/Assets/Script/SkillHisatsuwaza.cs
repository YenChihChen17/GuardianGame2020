using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI物件需要增加此行

public class SkillHisatsuwaza : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void click()
    {

        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        Debug.Log("Skill activate");
    }


}