using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform playerPosition;

    //Camera will follow the player
    // Update is called once per frame
    void Update()
    {
        if (playerPosition!=null){
            transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
        }

    }
}
