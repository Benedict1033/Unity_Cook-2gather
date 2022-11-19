using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fryer_Manager : MonoBehaviour
{

    public GameObject[] Potato_Pieces;
    public Transform Fryer_Pos;

    void Start()
    {
        InvokeRepeating("Potato_Pos", 0.2f, 0.2f);
    }

    void Potato_Pos() {
        for (int i = 0; i < Potato_Pieces.Length; i++)
        {

            Potato_Pieces[i].transform.position = new Vector3(Potato_Pieces[i].transform.position.x, Fryer_Pos.transform.position.y-0.015f, Potato_Pieces[i].transform.position.z);

        }
    }
}
