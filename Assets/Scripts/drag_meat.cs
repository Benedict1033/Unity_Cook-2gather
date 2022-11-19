using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_meat : MonoBehaviour
{
    private Vector3 Offset;
    private float Zcoord;
    public Transform pork;

    public Transform pan;
    public static bool Down_Limit = false;


    private void OnMouseDown()
    {
        Zcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {

        Vector3 mousePoint = new Vector3(pork.position.x, Input.mousePosition.y, pork.position.z);

        mousePoint.z = Zcoord;



        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        //if (!Down_Limit)
            transform.position = GetMouseWorldPos() + Offset;
    }

    private void Update()
    {
        pork.transform.position = new Vector3(pan.transform.position.x, pork.transform.position.y, pan.transform.position.z);



        //if (Fryer_Pos.transform.position.y < Fryer.transform.position.y)
        //{
        //    Down_Limit = true;
        //    Fryer_Pos.transform.position = new Vector3(Fryer_Pos.transform.position.x, Fryer.transform.position.y, Fryer_Pos.transform.position.z);
        //}
        //else
        //{
        //    Down_Limit = false;
        //}

        if (pork.transform.position.y >= pan.transform.position.y+0.01f) {
            pork.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        
        }

        pork.localEulerAngles = new Vector3(0, 0, -125);
    }
}
