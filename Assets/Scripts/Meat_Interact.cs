using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Meat_Interact : MonoBehaviour
{

    public Material mat;
    public Material mat2;
    static byte r = 153;
    static byte g = 42;
    static byte b = 30;
    static byte r2 = 231;
    static byte g2 = 170;
    static byte b2 = 144;

    public static bool Done;
    public GameObject Done_Panel;

    int Count;

    private void Start()
    {
        mat.color = new Color32(153, 42, 30, 255);
        mat2.color = new Color32(231, 170, 144, 255);
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i <= 2; i++)
        {
            Invoke("waitColor", 0.1f);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        Count++;
    }

    private void OnTriggerExit(Collider other)
    {
        mat.color = new Color32((r), (g), (b), 255);
        mat2.color = new Color32((r2), (g2), (b2), 255);

    }

    void waitColor() {

        if (r > 56 && b > 7)
        {
            mat.color = new Color32((r--), (30), (b--), 255);
            r -= 2;
        }
        if (g2 > 132 && b2 > 70)
        {
            mat2.color = new Color32((byte)(r2 - 15), (g2--), (byte)(b2--), 255);
            b2--;
        }

    }

    private void Update()
    {
        if (Count == 20) {
            Count = 21;
            Done_Panel.SetActive(true);
            Done = true;
        }
    }
}
