using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContril : MonoBehaviour
{
    public float Speed;

    private bool Counter;
    // Start is called before the first frame update
    void Start()
    {
        Counter = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90, 0, 0);
        if (Counter == false)
        {
            transform.Translate(new Vector3(-Speed, 0, 0) * Time.deltaTime, Space.World);
            Destroy(this.gameObject, 10);
        }
        else if (Counter == true)
        {
            transform.Translate(new Vector3(Speed*1.2f, 0, 0) * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Player" )
        {
            Destroy(this.gameObject);          
        }

        //else if (PW.gameObject.tag == "Weapon")
        //{
        //    this.gameObject.tag = "Weapon";
        //    Counter = true;
        //    Debug.Log(Counter);
        //}

        else if(PW.gameObject.tag == "Enemy" && Counter)
        {

         Destroy(this.gameObject);

        }
        
    }
}
