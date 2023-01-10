using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;



public class CharacterMovementHandler : NetworkBehaviour
{
    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    private string myID;
    private Text readText;
    private bool isGameEnd = false;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myID = getId(name);
        //print(myID);

        readText = GameObject.Find("idOni").GetComponent<Text>();

    }


    public override void FixedUpdateNetwork()
    {
        //Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Rotate the transform according to the client aim vector
            transform.forward = networkInputData.aimForwardVector;

            //Cancel out rotation on X axis as we don't want our character to tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            //Move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            //Jump
            if (networkInputData.isJumpPressed)
                networkCharacterControllerPrototypeCustom.Jump();

            //Check if we've fallen off the world.
            CheckFallRespawn();
        }

        //print(PhaseManager._timeLimit);
        //時間切れかつ、自分が鬼じゃなかったら
        if(PhaseManager._timeLimit<=0 && (readText.text != myID) && !isGameEnd) 
        {
            transform.position = Utils.GetPrisonSpawnPoint();
            isGameEnd = true;
        }

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

    void CheckFallRespawn()
    {
        if (transform.position.y < -12)
            transform.position = Utils.GetRandomSpawnPoint();

    }
}

