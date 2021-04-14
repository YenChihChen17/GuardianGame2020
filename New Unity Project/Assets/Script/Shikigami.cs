using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shikigami : MonoBehaviour
{
    public static Shikigami instance;
    public static GameObject Target;
    public GameObject body;
    public float speed;
    public int HP;
    public int hurt_value;
    public float hang_around_range;

    private float break_time;
    private float move_distance;
    private bool move;
    private bool get_random_number;
    private Vector3 destination;
    private Vector3 start_position;
    //public static Shikigami instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Target = null;
        move = false;
        get_random_number = false;
        start_position = this.transform.position;
        

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
        else
        {
            if(get_random_number == false)
            {
                Debug.Log(body.transform.position.x);
                break_time = Random.Range(1, 3);
                destination.x = Random.Range(start_position.x+hang_around_range, start_position.x - hang_around_range);
                destination.y = this.transform.position.y;
                Debug.Log(move_distance);
                Debug.Log(destination);
                get_random_number = true;
            }

            if(move == false)
            {
                break_time-=Time.deltaTime;
                if(break_time<0)
                {
                    move = true;
                }
            }
            
            else if (move)
            {
                float hang_around_speed;
                hang_around_speed = speed * 0.5f;
                this.transform.position = Vector3.MoveTowards(this.transform.position,destination,hang_around_speed*Time.deltaTime);
                if(this.transform.position.x == destination.x)
                {
                    get_random_number = false;
                    move = false;
                }
            }
        }

        if(HP<=0)
        {
            Destroy(this.gameObject);
        }

    }

}
