using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectPlayer : MonoBehaviour
{
    float time_f = 0f;
    int time_i = 0;
    int time_check = 0;
    public Button card_player_a_normal, card_player_a_active, card_player_a_disable, card_player_b_normal, card_player_b_active, card_player_b_disable,go_button;
    public static bool a_selected = false,b_selected=false,game_start=false;
    public Text number;
    int count = 3;
    //public static bool 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        PlayerCheck();
        GoPressed();
        if(a_selected==true&& user_score.b_hasselected==true || b_selected==true && user_score.a_hasselected == true)
        {
            go_button.gameObject.SetActive(true);
        }

        
    }


    private void PlayerCheck()
    {
        if (user_score.a_hasselected == true)
        {
            card_player_a_disable.gameObject.SetActive(true);
        }
        else if (user_score.b_hasselected == true)
        {
            card_player_b_disable.gameObject.SetActive(true);
        }
    }


    public void PlayerAPressed()
    {

        a_selected = true;
        card_player_b_normal.enabled = false;
        card_player_a_normal.gameObject.SetActive(false);
        card_player_a_active.gameObject.SetActive(true);
    }
    public void PlayerBPressed()
    {
        b_selected = true;
        card_player_a_normal.enabled = false;
        card_player_b_normal.gameObject.SetActive(false);
        card_player_b_active.gameObject.SetActive(true);
    }

    private void GoPressed()
    {
        
        if (game_start == true)
        {
            time_f += Time.deltaTime;
            time_i = (int)time_f;
            
            if (time_i - time_check == 1)
            {
                count -= 1;
                time_check = time_i;
                number.text = count.ToString();
            }
            if (count == 0&&a_selected==true)
            {
                SceneManager.LoadScene(2);
            }
            if (count == 0 && b_selected == true)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
