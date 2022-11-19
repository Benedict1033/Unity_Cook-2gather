using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Dont : MonoBehaviour
{
     float targetTime = 0;
    public static int seconds;
    public Text Time_Count;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name != "Login")
        {

            targetTime += Time.deltaTime;
            seconds = (int)targetTime;

            Time_Count.text = "Time : " + seconds.ToString();
        }
        else {
            Debug.Log("Login");
        }

    }
}
