using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShowResult : MonoBehaviour
{

    private Text resultText;
    //private float delay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        //print(Touch.myID);
        //print(Touch.readText.text);

        resultText = GameObject.Find("result").GetComponent<Text>();

        if(Touch.myID == Touch.readText.text)
            resultText.text = "奴隷から開放されました。";
        else
        {
            resultText.text = "まだまだ奴隷のままです";
        }

    }



    void Update()
    {
        //if (delay > 0)
        //    delay -= Time.deltaTime;
        //else
        //    SceneManager.LoadScene("gamemain");
    }

}
