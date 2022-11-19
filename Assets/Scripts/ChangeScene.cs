using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public  void ScencNext()
    {
        SceneManager.LoadScene("Choose Player");
        //print(SceneManager.GetActiveScene().buildIndex.ToString());
    }
    public void ScenePrevious()
    {
        Application.Quit();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("quit");
          
         //   EditorApplication.isPlaying = false;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
            
    }
}
