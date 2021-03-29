using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shikigami : MonoBehaviour
{
    public static GameObject Target;
    //public static Shikigami instance;
    // Start is called before the first frame update
    void Start()
    {
        Target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Target.transform.position - this.transform.position)*Time.deltaTime,Space.World);
    }

}
