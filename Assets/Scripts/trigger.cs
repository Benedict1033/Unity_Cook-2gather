using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject potato;
    public GameObject meat;
    public GameObject Pork_Insert;
    public GameObject Cabagge;
    public GameObject Mixed;


    public GameObject Bun;

    public static bool cabagge;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        other.gameObject.transform.position = new Vector3(1000, 1000, 1000);
      
        if (other.tag == "Potato")
        {
            potato.SetActive(true);
        }
        else if (other.tag == "Meat")
        {
            meat.SetActive(true);
        }
        else if (other.tag == "cabagge")
        {
            Swipe_Manager.done_Point += 1;


            Bun.SetActive(false);
            cabagge = true;

            if (Swipe_Manager.meat)
            {
                Pork_Insert.SetActive(false);
                Cabagge.SetActive(false);
                Mixed.SetActive(true);
                Swipe_Manager.meat = false;
           
            }
            else
            {
                Cabagge.SetActive(true);
            }
        }
    }
}
