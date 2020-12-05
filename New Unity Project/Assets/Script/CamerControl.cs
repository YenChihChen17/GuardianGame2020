using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControl : MonoBehaviour
{
    private GameObject target;
    public GameObject Home;
    private Vector3 offset;
    public float smooth;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == true)
        {
            target = GameObject.Find("Player");
            Vector3 position = this.transform.position;
            position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, smooth * Time.deltaTime);
            position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, smooth * Time.deltaTime);
            this.transform.position = position;
        }
        else if(GameObject.Find("Player(Clone)") == true)
        {
            target = GameObject.Find("Player(Clone)");
            Vector3 position = this.transform.position;
            position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, smooth * Time.deltaTime);
            position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, smooth * Time.deltaTime);
            this.transform.position = position;
        }
        else
        {
            target = Home;
            Vector3 position = this.transform.position;
            position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, smooth * Time.deltaTime);
            position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, smooth * Time.deltaTime);
            this.transform.position = position;
        }
    }
}
