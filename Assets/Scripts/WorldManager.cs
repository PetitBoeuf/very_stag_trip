using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        //Fonctionnement 1
        //Fonctionnement entre stagManager et worldManager
        //Référence dans le stagManager du worldmanager
        //Trigger d'un spherecollider dans le stag
        //Quand trigger prcq le cerf a collisionné un truc => ajouter, via le worldmanager du stagmanager, le collider dans une liste de colliders de wolrdManager
        // Puis, ici, continuellement faire une vérification du collider le plus proche et si il y en a aucun mettre le bool à false etc
        

        //F2
        //Overlap sphere et vérification continue des colliders les plus proches avec le layer interactable
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

            nearbyIAnimalText.text = interactableAnimal.animalName;
        }


        if (Input.GetKeyDown(KeyCode.Return) && canInteract) //&& there is a near ennemy
        {
            if (!goingDialog)
            {
                currentDialogSentences = interactableAnimal.currentMission.HandleMission();
                dialogueManager.ConfigureDialogue(currentDialogSentences);
                goingDialog = dialogueManager.DisplayNextSentence();
                interactableAnimal.AbleDisableMovement(goingDialog);
                return;
            }

            goingDialog = dialogueManager.DisplayNextSentence();
            interactableAnimal.AbleDisableMovement(goingDialog);
            return;
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
