using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControl : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;
    public float smooth;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(this.transform.position.x,target.transform.position.x,smooth*Time.deltaTime);
        this.transform.position = position;
    }
}
