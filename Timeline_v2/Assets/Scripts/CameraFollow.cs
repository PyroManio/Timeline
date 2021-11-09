using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    void LateUpdate() {
        // storing current camera's position in variable temp
        Vector3 temp = transform.position;

        // set camera's position x/y to be equal to player's position x/y
        // the reason for the added code is to allow the camera to be aligned with the pixel grid. 0.0625 unit = 1 pixel
        temp.x = (int)(playerTransform.position.x/0.0625)*0.0625f;
        temp.y = (int)(playerTransform.position.y/0.0625)*0.0625f;
        //temp.x = playerTransform.position.x;
        //temp.y = playerTransform.position.y;
        // set back camera's temp position to camera's curr postion
        transform.position = temp;
    }
} // class





