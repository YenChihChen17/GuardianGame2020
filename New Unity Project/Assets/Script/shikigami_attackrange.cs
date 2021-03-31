using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shikigami_attackrange : MonoBehaviour
{
    public bool found_enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Shikigami.Target!=null)
        {
            found_enemy = false;
            Debug.Log("Stop");
        }
        else
        {
            found_enemy = true;
            Debug.Log("Search");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (found_enemy)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Shikigami.Target = other.gameObject;
                Debug.Log("Find Enemy");
            }
        }
    }
}
