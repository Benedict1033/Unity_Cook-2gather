using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine.UI;

public class Cabage_Manager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    public GameObject[] veg;

    public Text Left_Text;
    public Text Right_Text;
    //public Text scoreText;

    int Score = 0;
    int Random_Result = 0;
    int Count = 0;
    int Left_Count = 0;
    int Right_Count = 8;

    public GameObject Done_Panel;
    public static bool Done;
    int Food_Scan=0;


    public GameObject[] Destroty_After_Done;

    public Text Tips;


    #region MainCode
    void Start() => InvokeRepeating("RandomNum", 0.5f, 0.5f);

    private void OnEnable()
    {
       
            swipeListener.OnSwipe.AddListener(OnSwipe);
        
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    #endregion 

    private void OnSwipe(string swipe)
    {
        switch (swipe)
        {

            case "Left":
                if (Count <= 8)
                {
                    if (Random_Result == 1)
                    {
                        Score++;
                        //scoreText.text = Score.ToString();

                        veg[Left_Count].GetComponent<Rigidbody>().isKinematic = false;
                        veg[Left_Count].GetComponent<Rigidbody>().AddForce(-10, 0, 0);
                        Left_Count++;
                        Count++;
                    }
                    else if (Score > 0)
                    {
                        Score--;
                        //scoreText.text = Score.ToString();
                    }
                }
                break;

            case "Right":
                if (Count <= 8)
                {
                    if (Random_Result == 2)
                    {
                        Score++;
                        //scoreText.text = Score.ToString();

                        veg[Right_Count - 1].GetComponent<Rigidbody>().isKinematic = false;
                        veg[Right_Count - 1].GetComponent<Rigidbody>().AddForce(10, 0, 0);
                        Right_Count--;
                        Count++;
                    }
                    else if (Score > 0)
                    {
                        Score--;
                        //scoreText.text = Score.ToString();
                    }
                }
                break;

            case "Up":
         

                if (Meat_Manager.Done)
                {
                    Meat_Manager.Done = false;
                    Done_Panel.SetActive(false);
                    Send_To_B.MeatGetReady = true;
                    Destroty_After_Done[0].gameObject.SetActive(false);
         
                    Food_Scan += 1;
                }
                if (Done)
                {
                    Left_Text.gameObject.SetActive(false);
                    Right_Text.gameObject.SetActive(false);

                    Send_To_B.CabaggeGetReady = true;
                    Done = false;
                    Done_Panel.SetActive(false);
                   Destroty_After_Done[1].gameObject.SetActive(false);
            
                    Food_Scan += 1;

                }
                if (Potato_Manager.Done)
                {
                    Send_To_B.PotatoGetReady = true;
                    Potato_Manager.Done = false;
                    Done_Panel.SetActive(false);
                    Destroty_After_Done[2].gameObject.SetActive(false);
             
                    Food_Scan += 1;


                }
                break;
        }
    }

    void RandomNum()
    {
        if (NYImageTrackerManager.Image_Status == "Track 02")
        {
            if (Count <= 8)
            {
                Random_Result = Random.Range(1, 3);
            }
            else {

                Random_Result = 3;
            }
            if (Random_Result == 1)
            {
                Left_Text.gameObject.SetActive(true);
                Right_Text.gameObject.SetActive(false);
            }
            else if (Random_Result == 2)
            {
                Left_Text.gameObject.SetActive(false);
                Right_Text.gameObject.SetActive(true);
            }
    }

        if (Count  >= 8&&Count!=11)
        {
            Left_Text.gameObject.SetActive(false);
            Right_Text.gameObject.SetActive(false);
            Count = 10;
        }
         if (Count == 10) {

            Done_Panel.SetActive(true);
            Done = true;
            Count = 11;
            Debug.Log(Done);
        }
    }

}
