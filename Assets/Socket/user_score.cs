using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine.UI;
using TMPro;
using Socket.Newtonsoft.Json;
using UnityEngine.SceneManagement;
public class user_score : MonoBehaviour
{
    private QSocket socket;
    public TextMeshProUGUI[] text_rank;
    public TMP_InputField username;
    public TMP_Text score;
    public static bool a_player = false, b_player = false;
    public string food = "";
    public static bool a_hasselected = false, b_hasselected = false;
    bool end = false;
    string combine_username = "";
    User user;
    // Start is called before the first frame update
    void Start()
    {
        socket = IO.Socket("http://10.120.1.48:5000");

        socket.On(QSocket.EVENT_CONNECT, () => {
            Debug.Log("Connected");

        });

        try
        {
            score.text = Dont.seconds.ToString();

        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        check_selected();
        B_CatchFood_A();
        CatchCombineDate();

        if (SceneManager.GetActiveScene().name == "Rank")

        user_rank();
        if (SceneManager.GetActiveScene().name=="Login")
            EndPage();

        if (end == true)
            SceneManager.LoadScene("Rank");

        if (GameObject.Find("A")!=null &&!a_player) {

            a_player = true;
            Debug.Log("A");
        }
        if (GameObject.Find("B")!=null&& !b_player) {

            b_player = true;
            Debug.Log("B");
        }

    }

    //public void A()
    //{
    //    a_player = true;
    //}
    //public void B()
    //{
    //    b_player = true;
    //}


    void CatchCombineDate()
    {
        socket.On("SendCombineDate", msg =>
        {
            
            combine_username = msg.ToString();
            
        });
        if(combine_username!="")
            userdata();
        Debug.Log(combine_username);

    }

    private void EndPage()
    {
        socket.On("ending_page", msg =>
        {
            if (msg.ToString() == "end")
            {
                Debug.Log("end");
                end = true;
            }
        });
    }
    public void userdata()/////////////////////////////////使用者輸入資料
    {
        user = new User
        {
            name = combine_username,
            score = int.Parse(score.text)
        };

        Debug.Log(user.ToString());
        socket.Emit("user_data", JsonConvert.SerializeObject(user));
        //socket.Disconnect();
        Debug.Log(JsonConvert.SerializeObject(user));
    }

    public void user_rank()/////////////////////////使用者排名
    {

        socket.Emit("user_rank", () => {
        });
        socket.On("user_rank", rank =>
        {
            // string rank_data = rank.ToString();
            Debug.Log(JsonConvert.SerializeObject(rank));
            string json = JsonConvert.SerializeObject(rank);

            List<User> user_data = JsonConvert.DeserializeObject<List<User>>(json);
            Debug.Log(user_data.Count);
            string[] user_name = new string[user_data.Count];/////////////////////////// 存放使用者名稱
            string[] user_score = new string[user_data.Count];/////////////////////// 存放使用者分數

            string[] rank_arry = new string[18];
            int num = 0;
            foreach (var i in user_data)
            {
                user_name[num] = i.name;
                user_score[num] = i.score.ToString();
                num += 1;

            }

            num = 0;
            for (int i = 0; i < user_name.Length; i++)
            {
                int count = 3;
                string[] str = user_name[i].Split(',');
                rank_arry[i * count] = str[0];
                Debug.Log(rank_arry[0]);
                rank_arry[i * count + 1] = str[1];
                rank_arry[i * count + 2] = user_score[i];
                text_rank[i * count].text = rank_arry[i * count].ToString();
                text_rank[i * count + 1].text = rank_arry[i * count + 1].ToString();
                text_rank[i * count + 2].text = rank_arry[i * count + 2].ToString();
            }
        });
    }

    public void SendUserDate()
    {
        Debug.Log(a_player);
        if (a_player == true)
        {
         
            socket.Emit("player_a_name", username.text);
        }
        if (b_player == true)
        {
            socket.Emit("player_b_name", username.text);
        }

    }



    public void select_A()
    {
        socket.Emit("select_A", "A");
    }

    public void select_B()
    {
        socket.Emit("select_B", "B");

    }

    public void A_SendFood_B()
    {
        Debug.Log("aaa");
        socket.Emit("A=>B", food);
    }


    void B_CatchFood_A()
    {
        socket.On("A=>B", msg =>
        {
            Debug.Log(msg.ToString());
        });
    }

    private void check_selected()
    {
        socket.On("A_check", msg =>
        {
            Debug.Log(msg.ToString());


            if (msg.ToString() == "A" && SelectPlayer.a_selected == false)
            {
                a_hasselected = true;

            }
        });

        socket.On("B_check", msg => {
            Debug.Log(msg.ToString());
            if (msg.ToString() == "B" && SelectPlayer.b_selected == false)
            {
                b_hasselected = true;

            }
        });
        socket.On("GoPressed", msg => {
            if (msg.ToString() == "GameStart")
            {
                SelectPlayer.game_start = true;
            }
        });
    }


    public void GoPressed()
    {
        socket.Emit("GoPressed", "GameStart");
    }
}
public class User
{
    public string name { get; set; }
    public int score { get; set; }
}


