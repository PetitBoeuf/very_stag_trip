using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public StagManager stagScript;
    [SerializeField]
    private InteractableAnimal interactableAnimal;
    public bool goingDialog;
    public List<string[]> currentDialogSentences;
    public DialogueManager dialogueManager;
    public bool canInteract;

    public int overlapSphereRadius;
    public LayerMask interactableLayer;
    public TextMeshProUGUI nearbyIAnimalText;
    public Transform stagTransform;

    public RawImage iaRawImage;

    // Start is called before the first frame update
    void Start()
    {
        goingDialog = false;
        canInteract = false;
        nearbyIAnimalText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canInteract) //&& there is a near ennemy
        {
            if (!goingDialog)
            {
                currentDialogSentences = interactableAnimal.currentMission.HandleMission();
                dialogueManager.ConfigureDialogue(currentDialogSentences);
                goingDialog = dialogueManager.DisplayNextSentence();
                interactableAnimal.MoveToStagForward();
                interactableAnimal.AbleDisableMovement(goingDialog);

                return;
            }

            goingDialog = dialogueManager.DisplayNextSentence();
            interactableAnimal.AbleDisableMovement(goingDialog);
            return;
        }


        if(goingDialog) return;

        //Fonctionnement 1
        //Fonctionnement entre stagManager et worldManager
        //R�f�rence dans le stagManager du worldmanager
        //Trigger d'un spherecollider dans le stag
        //Quand trigger prcq le cerf a collisionn� un truc => ajouter, via le worldmanager du stagmanager, le collider dans une liste de colliders de wolrdManager
        // Puis, ici, continuellement faire une v�rification du collider le plus proche et si il y en a aucun mettre le bool � false etc
        

        //F2
        //Overlap sphere et v�rification continue des colliders les plus proches avec le layer interactable
        Collider[] nearInteractables = Physics.OverlapSphere(stagTransform.position, overlapSphereRadius, interactableLayer);

        if (nearInteractables.Length == 0)
        {
            canInteract = false;
            nearbyIAnimalText.text = "";
        }
        else
        {
            canInteract = true;

            if (nearInteractables.Length == 1)
                interactableAnimal = nearInteractables[0].gameObject.GetComponent<InteractableAnimal>();
            else
                interactableAnimal = FindNearestIAnimal(nearInteractables);

            //Debug.Log(interactableAnimal.animalName);
            //Debug.Log(nearbyIAnimalText.text);
            nearbyIAnimalText.text = interactableAnimal.animalName;
            iaRawImage.texture = interactableAnimal.textureRenderer;
        }
    }

    InteractableAnimal FindNearestIAnimal(Collider[] nearInteractables)
    {
        Collider nearest = null;
        float nearestDistance = 9999999f;

        foreach(Collider nearCollider in nearInteractables)
        {
            float colliderDistance = Vector3.Distance(stagTransform.position, nearCollider.transform.position);

            if (colliderDistance < nearestDistance){
                nearest = nearCollider;
                nearestDistance = colliderDistance;
            }
        }

        return nearest.gameObject.GetComponent<InteractableAnimal>();
    }


}
