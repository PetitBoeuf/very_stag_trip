using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string[]> sentences;
    // Start is called before the first frame update

    public TextMeshProUGUI nameCanvasText;
    public TextMeshProUGUI sentenceCanvasText;
    //private TalkableScript stagScript;
    private Dialogue dialogue;
    public Animator DialogBoxAnimator;

    public StagManager stagScript;
    public Transform stagTransform;

    public CameraScript cameraScript;
    public bool startedDialogue;

    //private TalkableScript interlocutorScript;

    void Start()
    {
        startedDialogue = false;
        sentences = new Queue<string[]>();
        dialogue = new Dialogue();
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
    }

    public bool DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            //EndDialogue();
            //return false to say there is no more sentences
            cameraScript.ResetCamera();
            DialogBoxAnimator.SetBool("show", false);
            return false;
        }
        string[] sentence = sentences.Dequeue();
        nameCanvasText.text = sentence[0];
        sentenceCanvasText.text = sentence[1];

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
