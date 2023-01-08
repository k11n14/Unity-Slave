using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    public GameObject parent;
    


    // Start is called before the first frame update
    void Start()
    {
        //親オブジェクトを取得(NetworkPlayer PF [id=*])
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 当たった時に呼ばれる関数
    // 当たった時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Oni")
        {
            Debug.Log("Touched!!"); // ログを表示する

            //GameObject oni = GameObject.Find("Oni");
            other.gameObject.tag = "Player";
            parent.tag = "Oni";
        }
    }
}


