using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerOrientation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion destRotation = Quaternion.LookRotation(transform.right);

        //playerOrientation.rotation = Quaternion.RotateTowards(transform.rotation, destRotation, playerRotationFactor);

        playerOrientation.right = transform.right;
    }
}
