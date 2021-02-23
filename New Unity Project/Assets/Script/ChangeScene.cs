using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public Text Target;
    public float ChangeScale;
    public bool Color ;
    public Image Target_sp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Color)
        {
            float alpha = Mathf.PingPong(ChangeScale * Time.time, 1);
            Target.color = new Color(1, 1, 1, alpha);
            Target_sp.color = new Color(1, 1, 1, alpha);
        }
    }
    public void Change()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log(SceneName);
    }

}
