using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Analogy_handle : MonoBehaviour
{
    public RectTransform parent;
    public RectTransform handle;
    public GameObject Player;
    private Vector2 newPos;
    private Vector2 result;

    public void Drag() {
        Vector2 mPos = Input.mousePosition;
        newPos = new Vector2(0, 0);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, mPos, null, out newPos);
        result = Vector2.ClampMagnitude(newPos, 40f);

        handle.anchoredPosition = result;
    }
    public void Update() {
        if (GameObject.Find("Player") == true)
        {
            Player = GameObject.Find("Player");
            if (result.x > 0)
            {
                Player.SendMessage("GoRight");
            }
            else if (result.x < 0)
            {
                Player.SendMessage("GoLeft");
            }
            else if (result.x == 0)
            {
                Player.SendMessage("Stop");
            }
        }
    }
    public void EndDrag() {
        handle.anchoredPosition = Vector2.zero;
        result = Vector2.zero;
    }
}
