using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : NetworkBehaviour
{

    public static float _timeLimit = 150.0f;


    // Start is called before the first frame update
    void Start()
    {
        //_timeLimit = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Object.HasInputAuthority && _timeLimit>=0)
        {
            _timeLimit -= Time.deltaTime;
            RPC_SendCount(_timeLimit);
        }

        //print(_timeLimit);
        //if (_timeLimit < 0)
            //print("zero");
            //SceneManager.LoadScene("gamefinish");

    }



    //private Text _messages;
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendCount(float timelimit, RpcInfo info = default)
    {
        if(timelimit< _timeLimit)
            _timeLimit = timelimit;
    }
}
