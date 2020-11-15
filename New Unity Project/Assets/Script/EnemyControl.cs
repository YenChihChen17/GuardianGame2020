using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    private float timer;
    private bool attacked;
    public float stop_t;
    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (attacked == false)
        {
            transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime, Space.World);
        }
        if (attacked == true)
        {
            if (timer >=stop_t )
            {
                attacked = false;
            }
        }
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Weapon") {
            attacked = true;
            timer = 0;
        }
    }
}
