using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_food : MonoBehaviour
{
    private Vector3 Offset;
    private float Zcoord;

    private void OnMouseDown()
    {
        Zcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {

        Vector3 mousePoint =  Input.mousePosition;

        mousePoint.z = Zcoord;


        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
      

            transform.position = GetMouseWorldPos() + Offset;
    }

}
