using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerOrientation;
    [Header("DialogCamera")]
    public Transform dialogCameraTransform;
    public Camera dialogCamera;
    public Transform dialogPOV;
    [Header("TPS-Cinemachine Camera")]
    //public Transform mainCamClone;
    //public Transform TPSCamera;
    public Camera cinemachineCamera;
    [SerializeField]
    private bool freeLook;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        freeLook = true;
        cinemachineCamera = GetComponent<Camera>();
        dialogCamera.enabled = false;
    }

    // Update is called once per frame
    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion destRotation = Quaternion.LookRotation(transform.right);

        //playerOrientation.rotation = Quaternion.RotateTowards(transform.rotation, destRotation, playerRotationFactor);

        playerOrientation.right = transform.right;
        if (freeLook)
        {
            dialogCameraTransform.position = transform.position;
            //dialogCameraTransform.right = transform.right;
        }
    }
    public void SetDialogueCamera()
    {
        //Camera.main = dialogueCamera;
        StartCoroutine(MoveCamToDialogue());
    }
    public void ResetCamera()
    {
        //dialogueCamera.enabled = false;
        //cinemachineCamera.enabled = true;
        StartCoroutine(ResetCamToFreeLook());

    }
    private IEnumerator MoveCamToDialogue()
    {
        cinemachineCamera.enabled = false;
        dialogCamera.enabled = true;
        freeLook = false;
        Vector3 startPosition = transform.position; // Position de départ de la caméra

        float elapsedTime = 0f; // Temps écoulé depuis le début du mouvement
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 3f; // Incrémentez le temps écoulé en fonction de la vitesse
            //Debug.Log(elapsedTime);
            dialogCameraTransform.position = Vector3.Lerp(
                startPosition,
                dialogPOV.position,
                elapsedTime
            ); // Déplacez la caméra de manière fluide en utilisant Lerp

            dialogCameraTransform.rotation = Quaternion.Slerp(dialogCameraTransform.rotation, dialogPOV.rotation, elapsedTime);

            yield return null;
        }

        // Assurez-vous que la caméra soit précisément à la position cible à la fin du mouvement
        dialogCameraTransform.position = dialogPOV.position;
    }
    private IEnumerator ResetCamToFreeLook()
    {

        Vector3 startPosition = dialogCameraTransform.position; // Position de départ de la caméra
        float elapsedTime = 0f; // Temps écoulé depuis le début du mouvement
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 3f; // Incrémentez le temps écoulé en fonction de la vitesse
            //Debug.Log(elapsedTime);
            dialogCameraTransform.position = Vector3.Lerp(
                startPosition,
                transform.position,
                elapsedTime
            );

            dialogCameraTransform.rotation = Quaternion.Slerp(dialogCameraTransform.rotation, transform.rotation, elapsedTime);
            yield return null;
        }

        // Assurez-vous que la caméra soit précisément à la position cible à la fin du mouvement
        dialogCameraTransform.position = transform.position;
        cinemachineCamera.enabled = true;
        dialogCamera.enabled = false;
        freeLook = true;
    }

}
