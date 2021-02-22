using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Analogy_handle : MonoBehaviour
{
    public RectTransform parent;
    public RectTransform handle;
    public GameObject Player;
   /* private bool PressDown;
    private Vector2 newPos;
    private Vector2 result;*/

    public void Drag() {

       /* Vector2 mPos = Input.mousePosition;
        newPos = new Vector2(0, 0);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, mPos, null, out newPos);
        result = Vector2.ClampMagnitude(newPos, 40f);

        handle.anchoredPosition = result;*/
    }
    public void Update() {
        if (GameObject.FindWithTag("Player") == true)
        {
            Player = GameObject.FindWithTag("Player");
           /* if (result.x > 0)
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
            }*/
        }
    }
    public void GoRight()
    {
        Player.SendMessage("GoRight");
    }
    public void StopRight()
    {
        Player.SendMessage("StopRight");
    }
    public void GoLeft()
    {
        Player.SendMessage("GoLeft");
    }
    public void StopLeft()
    {
        Player.SendMessage("StopLeft");
    }
    public void Jump()
    {
        Player.SendMessage("DoJump");
    }
    public void Attack()
    {
        Player.SendMessage("DoAttack");
    }
    public void StopAttack()
    {
        Player.SendMessage("StopAttack");
    }
    /* public void EndDrag() {
         handle.anchoredPosition = Vector2.zero;
         result = Vector2.zero;
     }*/
    public void Defend()
    {
        Player.SendMessage("DoDefend");
    }
    public void ResetDefned()
    {
        Player.SendMessage("ResetDefend");
    }
}
