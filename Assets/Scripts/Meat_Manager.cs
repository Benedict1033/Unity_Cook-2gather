using UnityEngine;

public class Meat_Manager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public GameObject Hammer;
    public GameObject Done_Panel;
    public static bool Done;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.name == "pork")
                {
                    if (hit.transform.localScale.y <= 4f)
                    {
                        // meat scale
                        hit.transform.localScale = new Vector3(hit.transform.localScale.x + 0.05f, hit.transform.localScale.y + 0.05f, hit.transform.localScale.z);

                        // hammer rotation
                        Hammer.transform.localPosition = new Vector3(0, 0f, 0.008f);
                        Hammer.transform.localEulerAngles = new Vector3(-180f, -20f, -90f);
                        Invoke("Hammer_Rotate_Back", 0.35f);
                    }
                    else if(hit.transform.localScale.y <= 4.05f&&hit.transform.localScale.y>=3.95f)
                    {
                        Done = true;
                        Done_Panel.SetActive(true);
                    }
                }
            }
        }
    }

    void Hammer_Rotate_Back()
    {
        Hammer.transform.localPosition = new Vector3(0, 0f, 0.012f);
        Hammer.transform.localEulerAngles = new Vector3(-180f, 8f, -90f);
    }

}
