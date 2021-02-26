using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl1 : MonoBehaviour
{
    public GameObject Player;
    public float Speed;
    private bool Counter;
    float angle;
    // Start is called before the first frame update
    void Start()
    {

        Counter = false;
        Vector3 relative = transform.InverseTransformPoint(Player.transform.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        this.transform.Rotate(0, 0, -angle);
    }

    // Update is called once per frame
    void Update()
    {       
        if (Counter == false)
        {
            transform.Translate(new Vector3(-Speed, 0, 0) * Time.deltaTime);
            Destroy(this.gameObject, 10);
        }
        else if (Counter == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(new Vector3(Speed * 1.2f, 0, 0) * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider PW)
    {
        if (PW.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        else if (PW.gameObject.tag == "Weapon")
        {
            this.gameObject.tag = "Weapon";
            Counter = true;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            PlayerControl.AttackEnemy = true;
            //Debug.Log(Counter);          
        }

        else if (PW.gameObject.tag == "Enemy" && Counter)
        {

            Destroy(this.gameObject);

        }

    }
}
