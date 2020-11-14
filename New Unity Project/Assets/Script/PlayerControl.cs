using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Rigidbody rig;
    public float jumpF;
    private bool can_j;
  

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        can_j = true;
    }

    // Update is called once per frame
    void Update()
    {
        float xm = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            xm += speed * Time.deltaTime;
        }
        target.Translate(new Vector3(xm, 0, 0));
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xm -= speed * Time.deltaTime;
        }
       
        
        if (Input.GetKey(KeyCode.Space)&& can_j == true)
        {
            rig.AddForce(new Vector3(0, jumpF, 0), ForceMode.Impulse);
            can_j = false;
            Debug.Log(can_j);
        }
        
        Debug.Log(can_j);
        target.Translate(new Vector3(xm, 0, 0));
        
    }
    private void OnCollisionStay(Collision collision) {
        can_j = true;
    }
}
