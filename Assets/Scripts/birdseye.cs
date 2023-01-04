using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdseye : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("PlayerArmature");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.player.transform.position + offset;
    }
}
