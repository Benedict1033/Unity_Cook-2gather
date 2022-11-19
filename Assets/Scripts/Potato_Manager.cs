using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato_Manager : MonoBehaviour
{
    public GameObject[] Potato;

    bool Slice_Done;

    public GameObject Slice_Panel;

    public GameObject Done_Panel;
    public static bool Done;

    private void Update()
    {
        if (!Slice_Done)
        {
            Potato[Drag_Manager.Slice_Count].gameObject.GetComponent<Rigidbody>().isKinematic = false;

            if (Drag_Manager.Slice_Count >= 10&& Drag_Manager.Slice_Count <= 12)
            {
                Slice_Done = true;            
                Drag_Manager.Slice_Count = 13;
            }
             if (Drag_Manager.Slice_Count == 13)
            {

                Done_Panel.SetActive(true);
                Done = true;
                Drag_Manager.Slice_Count = 14;
            }
        }

        if (NYImageTrackerManager.Image_Status == "Track 03"&&!Slice_Done)
        {
            Slice_Panel.SetActive(true);
        }
        else
        {
            Slice_Panel.SetActive(false);
        }
    }
}
