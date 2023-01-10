using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    // InputFieldのText参照用
    public TMP_InputField XXX;//XXXにゲームオブジェクトのinput(TMP)入れてる？



    // InputFieldの文字が変更されたらコールバックされる。OnValueChangedと言う関数
    public void OnValueChanged()
    {
        //「TMP_InputField」型の変数を宣言
        //string型の変数YYY＝XXX（ゲームオブジェクトのinput(TMP)）のTMP_InputField型のコンポーネントのパブリック変数のtext
        string YYY = XXX.GetComponent<TMP_InputField>().text;
        //
        GetComponent<TMP_Text>().text = YYY;
    }
}