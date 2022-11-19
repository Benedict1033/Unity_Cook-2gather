using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    private Vector3 Offset;
    private float Zcoord;
    public Transform Fryer_Net;

    public Transform Fryer;
    public static bool Down_Limit=false;


    private void OnMouseDown()
    {
        Zcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {

        Vector3 mousePoint = new Vector3(Fryer_Net.position.x,Input.mousePosition.y, Fryer_Net.position.z);

        mousePoint.z = Zcoord;
 


        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if(!Down_Limit)
        transform.position = GetMouseWorldPos() + Offset;
    }

    private void Update()
    {
        Fryer_Net.transform.position = new Vector3(Fryer.transform.position.x, Fryer_Net.transform.position.y, Fryer.transform.position.z);

        if (Fryer_Net.transform.position.y < Fryer.transform.position.y)
        {
            Down_Limit = true;
            Fryer_Net.transform.position = new Vector3(Fryer_Net.transform.position.x, Fryer.transform.position.y, Fryer_Net.transform.position.z);
        }
        else {
            Down_Limit = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
