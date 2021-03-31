using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shikigami : MonoBehaviour
{
    public static GameObject Target;
    public GameObject body;
    public float speed;
    //public static Shikigami instance;
    // Start is called before the first frame update
    void Start()
    {
        Target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target!=null)
        {
            if (Target.transform.position.x - body.transform.position.x > 0)
            {
                transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime, Space.World);
            }
            else if (Target.transform.position.x - body.transform.position.x < 0)
            {
                transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime, Space.World);
            }
        }
        
    }

}
