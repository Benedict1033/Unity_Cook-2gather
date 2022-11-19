using UnityEngine;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine.SceneManagement;

public class Send_To_B : MonoBehaviour
{
    private QSocket socket;

    public GameObject pork;
    public GameObject cabagge;
    public GameObject potato;

    public GameObject win_Panel;

    public static bool MeatGetReady;
    public static bool CabaggeGetReady;
    public static bool PotatoGetReady;
    bool food_send = false;
    int food_state = 0;
    string check_done_state;
    string food = "";

    void Start()
    {
        socket = IO.Socket("http://10.120.1.48:5000");

        socket.On(QSocket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected");
        });
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
            B_CatchFood_A();

        if (MeatGetReady)
        {
            MeatGetReady = false;
            A_SendFood_B("pork");
        }
        if (CabaggeGetReady)
        {
            CabaggeGetReady = false;
            A_SendFood_B("cabagge");

        }
        if (PotatoGetReady)
        {
            PotatoGetReady = false;
            A_SendFood_B("potato");

        }

        CheckFood(food);

        try
        {

            if (Swipe_Manager.done_Point == 3)
            {
                win_Panel.SetActive(true);
                socket.Emit("done", "done");
                Swipe_Manager.done_Point += 1;
            }
        }
        catch { }

        //if (Swipe_Manager.done_Point == 4)
        check_done();
        //if (food_send)
        //{
        //    if(food_state==1)
        //        socket.Emit("foood_send", "pork");
        //    if (food_state == 2)
        //        socket.Emit("foood_send", "cabagge");
        //    if (food_state == 3)
        //        socket.Emit("foood_send", "potato");
        //}
        //socket.On("food_send", msg =>
        //{
        //    food_send = false;
        //});

    }
    public void A_SendFood_B(string foodName)
    {
        socket.Emit("A=>B", foodName);
        //if (foodName == "pork")
        //    food_state = 1;
        //if (foodName == "cabagge")
        //    food_state = 2;
        //if (foodName == "potato")
        //    food_state = 3;
        //food_send = true;
        
    }

    void B_CatchFood_A()
    {
        socket.On("A=>B", msg =>
        {
            Debug.Log(msg.ToString());
            food = msg.ToString();
        });
    }

    void CheckFood(string now_food)
    {

        if (now_food == "pork")
            pork.SetActive(true);
        if (now_food == "cabagge")
            cabagge.SetActive(true);
        if (now_food == "potato")
            potato.SetActive(true);
    }

    void check_done()
    {
        try
        {

            //Debug.Log("Done");

            socket.On("done", msg =>
            {
                if (msg.ToString() == "done")
                    check_done_state = msg.ToString();
            });
            if (check_done_state == "done")
                SceneManager.LoadScene("Login");
        }
        catch { }
    }
}
