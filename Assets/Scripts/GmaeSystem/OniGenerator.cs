using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OniGenerator : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();
    private List<int> playerIds = new List<int>();
    private int playerCount = 0;

    private int minNumber = 10000;

    // Start is called before the first frame update
    void Start()
    {

    }

    int getPlayerId(GameObject obj)
    {
        var name = obj.name;
        //Debug.Log("org:"+ name + ", " + name.Length);

        name = name.Substring(4);// 前四文字を削除
        //Debug.Log("4:"+name + ", " + name.Length);

        name = name.Substring(0,  name.Length - 1);//後ろ一文字を削除
        //Debug.Log("1:"+ name + ", " + name.Length);

        return int.Parse(name);
    }

    // Update is called once per frame
    void Update()
    {
        // using UnityEngine.SceneManagement の追加を忘れないように
        // シーンを取得
        Scene scene = SceneManager.GetSceneByBuildIndex(0);
        // 取得したシーンのルートにあるオブジェクトを取得
        GameObject[] rootObjects = scene.GetRootGameObjects();

        if(rootObjects.Length -4 != playerCount)
        {
            // 取得したオブジェクトの名前を表示
            foreach (GameObject obj in rootObjects)
            {
                //[Id:2]
                if (obj.name !="ENV" && obj.name != "MAP" && obj.name != "NetworkRunnerHandler" && obj.name != "Camera")
                {
                    int id = getPlayerId(obj);
                    // 含んでいなかった場合
                    if(playerIds.Contains(id) == false)
                    {
                        playerIds.Add(id);
                        players.Add(obj);
                        playerCount= playerIds.Count;
                    }
                    //含んでいた場合
                    else
                    {
                        playerIds.Remove(id);
                        players.Remove(obj);
                    }

                    if (id < minNumber)
                    {
                        minNumber = id;
                    }
                }
            }
        Debug.Log("playerCount" + playerCount);

        }
        //Debug.Log(minNumber);
    }
}
