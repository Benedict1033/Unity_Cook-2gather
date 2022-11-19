using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine.UI;

public class Swipe_Manager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;



    public GameObject Done_Panel;
    public GameObject Final_Done_Panel;
    public GameObject Potato_Fried;
    public GameObject Pork_Fried;
    public GameObject Bun;
    public GameObject Mixed;
    public GameObject cabagge;

    public GameObject[] Destroty_After_Done;

    public static int done_Point=0;


    public static bool meat;

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        switch (swipe)
        {
            case "Right":
                if (Oil_Manager.Done)
                {
                    Done_Panel.SetActive(false);
                    Potato_Fried.SetActive(true);
                    Oil_Manager.Done = false;

                    Destroty_After_Done[0].gameObject.SetActive(false);
                    done_Point += 1;
                }
                if (Meat_Interact.Done) {
                    done_Point += 1;
                    Meat_Interact.Done = false;

                    Destroty_After_Done[1].gameObject.SetActive(false);
                    
                    if (trigger.cabagge)
                    {
                        Mixed.SetActive(true);
                        cabagge.SetActive(false);
                        meat = true;
                        trigger.cabagge = false;
                    }
                    else {

                        Pork_Fried.SetActive(true);
                    }

                    Done_Panel.SetActive(false);


                    Bun.SetActive(false);
                }

                break;

            case "Up":
                if (done_Point == 3) {
                    Swipe_Manager.done_Point += 1;
                    Final_Done_Panel.SetActive(false);
                }
                break;
        }
    }

}
