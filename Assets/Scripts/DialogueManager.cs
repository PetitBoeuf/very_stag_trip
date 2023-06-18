using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string[]> sentences;
    // Start is called before the first frame update

    public GameObject interlocutorHolder;
    public GameObject stagHolder;
    public TextMeshProUGUI stagNameDBox;
    public TextMeshProUGUI interactAnimalDBox;
    public TextMeshProUGUI sentenceDBox;
    //private TalkableScript stagScript;
    private Dialogue dialogue;
    public Animator DialogBoxAnimator;

    public StagManager stagScript;
    public Transform stagTransform;

    public CameraScript cameraScript;
    public bool startedDialogue;
    public Animator stagAnimator;

    //private TalkableScript interlocutorScript;

    void Start()
    {
        startedDialogue = false;
        sentences = new Queue<string[]>();
        dialogue = new Dialogue();
        interlocutorHolder.SetActive(false);
        stagHolder.SetActive(false);
    }

    //public void StartDialogue(Dialogue p_dialogue)
    //{
    //    //Debug.Log("Starting conversation with " + dialogue.name);


    //    cameraScript.SetDialogueCamera();

    //    DialogBoxAnimator.SetBool("show", true);
    //    dialogue = p_dialogue;
    //    dialogue.stagInterlocutorScript.StartEndConversation();

    //    stagTransform.rotation = Quaternion.RotateTowards(
    //        stagTransform.rotation,
    //        Quaternion.LookRotation(
    //            dialogue.stagInterlocutorTransform.position - stagTransform.position,
    //            Vector3.up
    //        ),
    //        360
    //    );

    //    sentences.Clear();
    //    foreach(string[] dialog in dialogue.dialogList)
    //    {
    //        sentences.Enqueue(dialog);
    //    }

    //    DisplayNextSentence();
    //}
    public void ConfigureDialogue(List<string[]> dialogSentences)
    {
        sentences.Clear();
        DialogBoxAnimator.SetBool("show", true);
        cameraScript.SetDialogueCamera();

        foreach (string[] dialog in dialogSentences)
        {
            sentences.Enqueue(dialog);
        }
        stagAnimator.SetBool("Talking", true);
    }

    public bool DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            //EndDialogue();
            //return false to say there is no more sentences
            cameraScript.ResetCamera();
            DialogBoxAnimator.SetBool("show", false);
            stagAnimator.SetBool("Talking", false);

            return false;
        }

        string[] sentence = sentences.Dequeue();

        if (sentence[0] == stagScript.stagName)
        {
            stagHolder.SetActive(true);
            interlocutorHolder.SetActive(false);
        }
        else
        {
            stagHolder.SetActive(false);
            interlocutorHolder.SetActive(true);
        }

        stagNameDBox.text = sentence[0];
        interactAnimalDBox.text = sentence[0];
        sentenceDBox.text = sentence[1];

        return true;
        //Debug.Log(sentence);
    }

    //void EndDialogue()
    //{
    //    cameraScript.ResetCamera();
    //    DialogBoxAnimator.SetBool("show", false);
    //    stagScript.StartEndConversation();
    //    dialogue.stagInterlocutorScript.StartEndConversation();
    //    //Debug.Log("Ending conversation between " + stagScript.name + " and " + dialogue.stagInterlocutorScript.name + ".");
    //}
    void EndDialogue()
    {

        DialogBoxAnimator.SetBool("show", false);
        //Debug.Log("Ending conversation between " + stagScript.name + " and " + dialogue.stagInterlocutorScript.name + ".");

    }

}
