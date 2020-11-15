using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Rigidbody rig;
    public float jumpF;
    public float hurtX;
    public float hurtY;
    private bool can_j;
    private bool ishurt;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        can_j = true;
        ishurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xm = 0;
        if (Input.GetKey(KeyCode.RightArrow)&& ishurt==false)
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
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground")
        {
            can_j = true;
            ishurt = false;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            rig.AddForce(new Vector3(-hurtX,hurtY, 0), ForceMode.Impulse);
            ishurt = true;
            Debug.Log("Hurt");
        }
    }
}
