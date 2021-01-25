using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject target;
    public GameObject Home;
    public float smooth;
    private bool find;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Player") == true && find == false)
        {
            target = GameObject.FindWithTag("Player");
            find = true;
        }
        else if(GameObject.FindWithTag("Player") == false)
        {
            find = false;
            target = Home;
        }

        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, smooth * Time.deltaTime);
        position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y+2, smooth * Time.deltaTime);
        this.transform.position = position;
    }
}
