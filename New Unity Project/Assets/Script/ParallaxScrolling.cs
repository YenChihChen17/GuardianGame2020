using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] Backgrounds;
    public float parellaxScale;
    public float parallaxReductionFactor;
    public float smoothing;

    private Transform cam;
    private Vector3 previousCamPos;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        float parallax = (previousCamPos.x - cam.position.x) * parellaxScale;

        for(int i = 0; i<Backgrounds.Length;i++)
        {
            float backgroundTargetPosX = Backgrounds[i].position.x + parallax*(i* parallaxReductionFactor + 1);

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
