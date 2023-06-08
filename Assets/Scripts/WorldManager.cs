using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public StagManager stagScript;
    public InteractableAnimal interactableAnimal;
    public bool goingDialog;
    public List<string[]> currentDialogSentences;
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        goingDialog = false;
    }

    // Update is called once per frame
    void Update()
    {
        //OverlapSphere continuously 
        //Nearest ennemy on overlapsphere

        //Input onkeyenter
        //InteractableAnimal.Interact()

        if (Input.GetKeyDown(KeyCode.Return)) //&& there is a near ennemy
        {
            if (!goingDialog)
            {
                currentDialogSentences = interactableAnimal.currentMission.HandleMission();
                dialogueManager.ConfigureDialogue(currentDialogSentences);
                goingDialog = dialogueManager.DisplayNextSentence();

                return;
            }

            goingDialog = dialogueManager.DisplayNextSentence();
            return;
        }

    }
}
