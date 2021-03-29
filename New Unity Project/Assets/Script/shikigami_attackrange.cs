using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shikigami_attackrange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Shikigami.Target = other.gameObject;
            Debug.Log("Find Enemy");
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Find Player");
        }
    }
}
