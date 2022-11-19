using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Oil_Manager : MonoBehaviour
{

    public Material mat;
    static byte r = 168;
    static byte g = 109;
    static byte b = 65;

    public static bool Done;
    public GameObject Done_Panel;

    private void Start()
    {
        mat.color = new Color32(168, 109, 65, 255);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (r < 250 && g < 184 && b > 0)
        {
            mat.color = new Color32((r++), (g++), (b--), 255);

        }
        else if(r<250)
        {
            r = 251;
            Done_Panel.SetActive(true);
            Done = true;
        }
        other.gameObject.GetComponent<Rigidbody>().drag = 50;
    }

    private void OnTriggerExit(Collider other)
    {
        mat.color = new Color32((r), (g), (b), 255);
        other.gameObject.GetComponent<Rigidbody>().drag = 100;

    }
}
