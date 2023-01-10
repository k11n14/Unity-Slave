using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Touch : NetworkBehaviour 
{
    //public string Name { get; set; }
    //private string _name;

    //マテリアルを保持しておく。0:鬼、1:プレイヤー
    public Material[] ColorSet = new Material[2];

    private GameObject parent;
    private GameObject child;
    //private bool isOni = false;


    public static Text readText;
    public static string myID;

    private bool tagOni;
    private float delay = -0.1f;
    private Text hint_text;
    private float limitTime;



    // Start is called before the first frame update

    void Start()
    {
        //Name = _name;
        //親オブジェクトを取得(NetworkPlayer PF [id=*])
        parent = transform.parent.gameObject;
        //子オブジェクトbodyを取得
        child = transform.Find("Body").gameObject;
        hint_text = GameObject.Find("Hint").GetComponent<Text>();

        myID = getId(parent.name);
        
        


        //if (GameObject.FindGameObjectWithTag("Oni") == null)
        //{
        //    parent.tag = "Oni";
        //}

        //if (parent.tag == "Oni")
        //    isOni = true;
    }

    public  void Update()
    {

        //Name = parent.tag;
        //print(Name);
        //Debug.Log(readText.text);
        limitTime = PhaseManager._timeLimit;
        readText = GameObject.Find("idOni").GetComponent<Text>();

        if (getId(parent.name) == readText.text)
        {
            if (!tagOni)
                delay = 3.0f;

            if(delay>0)
            {
                delay -= Time.deltaTime;
                hint_text.text = "ゲーム時間残り、" + limitTime + "秒\n";
                hint_text.text += "安全時間終了まで、"+ delay.ToString()+"秒"; 
            }
            else
            {
                hint_text.text = "ゲーム時間残り、" + limitTime + "秒\n";
                hint_text.text += "奴隷開放権を守り抜け！";
            }

            parent.tag = "Oni";
            child.GetComponent<MeshRenderer>().material = ColorSet[0];
            tagOni = true;
            


        }
        else
        {
            parent.tag = "Player";
            child.GetComponent<MeshRenderer>().material = ColorSet[1];
            tagOni = false;
            hint_text.text = "ゲーム時間残り、" + limitTime + "秒\n";
            hint_text.text += "奴隷解放権を奪い取れ！";
        }
    }



    //private void setPlayerTag()
    //{
    //    Scene scene = SceneManager.GetSceneByBuildIndex(0);
    //    // 取得したシーンのルートにあるオブジェクトを取得
    //    GameObject[] rootObjects = scene.GetRootGameObjects();

    //    // 取得したオブジェクトの名前を表示
    //    foreach (GameObject obj in rootObjects)
    //    {
    //        //[Id:2]
    //        if (obj.name != "ENV" && obj.name != "MAP" && obj.name != "NetworkRunnerHandler" && obj.name != "Camera" && obj.name != "Canvas")
    //        {
    //            if(obj.tag != parent.tag)
    //            {
    //                obj.tag = "Player";
    //            }
    //            else
    //            {
    //                obj.tag = "Oni";
    //            }
    //        }
    //    }
    //}


    private Text _messages;
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        if (_messages == null)
            _messages = GameObject.Find("idOni").GetComponent<Text>();
        if (info.IsInvokeLocal)
        {
            message = $"{message}";
            //自分が他人にタッチした場合、自分はプレイヤーになる。
            //parent.tag = "Player";
            //isOni = false;
            //print("isOni" + isOni);
        }
        else { 
            message = $"{message}";
            //他の人のタッチした宣言のIDが自分と一緒だったら、自分が鬼になる。
            //if (message == getId(parent.name))
            //{
            //    //isOni = true;
            //    parent.tag = "Oni";
            //    print("You are Oni!");
            //    print("isOni" + isOni);
            //}
        }
        _messages.text = message;
    }

    // 当たった時に呼ばれる関数
    void OnCollisionExit(Collision other)
    {
        //print(other.transform.parent.gameObject.name);
        //自分が鬼で、衝突相手がプレイヤーだったら、
        //if(other.transform.parent.gameObject.GetComponent<Iname>().Name == "Oni")
        //    parent.tag = "Oni";

        //if (other.transform.parent.gameObject.GetComponent<Iname>().Name == "Player")
        //    parent.tag = "Player";



        if (parent.CompareTag("Oni") && other.transform.parent!=null)
        {
            if(other.transform.parent.gameObject.CompareTag("Player") && delay<0)
            {
                if (Object.HasInputAuthority)
                {
                    //ID〇〇さんにタッチした宣言をする！
                    RPC_SendMessage(getId(other.transform.parent.gameObject.name));
                }
            }
            //other.transform.parent.gameObject.tag = "Oni";
        }
    }

    public void changeTag()
    {

    }


    string getId(string objname)
    {
        //print(objname);
        var name = objname;
        //Debug.Log("obj.name:"+ obj.name);
        //Debug.Log("org:"+ name + ", " + name.Length);

        name = name.Substring(4);// 前四文字を削除
        //Debug.Log("4:"+name + ", " + name.Length);

        name = name.Substring(0, name.Length - 1);//後ろ一文字を削除
        //Debug.Log("1:"+ name + ", " + name.Length);

        //print(name);
        return name;
    }
}


