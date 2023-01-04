using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // もし入力したキーがSpaceキーならば、中の処理を実行する
    if (Input.GetKey(KeyCode.Space)) {
        // Rendererコンポーネントのmaterialのcolorを呼び出し
        // Colorクラスのred(=赤色のRGBデータ)を値にセットする
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    }
}
