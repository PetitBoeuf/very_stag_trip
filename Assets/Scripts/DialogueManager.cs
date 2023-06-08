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

    public StagScript stagScript;
    public Transform stagTransform;

    public CameraScript cameraScript;

    //private TalkableScript interlocutorScript;

    void Start()
    {
        sentences = new Queue<string[]>();
        dialogue = new Dialogue();
    }

    public void StartDialogue(Dialogue p_dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);


        //SmoothTransition

        cameraScript.SetDialogueCamera();

        DialogBoxAnimator.SetBool("show", true);
        dialogue = p_dialogue;
        dialogue.stagInterlocutorScript.StartEndConversation();

        stagTransform.rotation = Quaternion.RotateTowards(
            stagTransform.rotation,
            Quaternion.LookRotation(
                dialogue.stagInterlocutorTransform.position - stagTransform.position,
                Vector3.up
            ),
            360
        );

        sentences.Clear();
        foreach(string[] dialog in dialogue.dialogList)
        {
            sentences.Enqueue(dialog);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string[] sentence = sentences.Dequeue();
        nameCanvasText.text = sentence[0];
        sentenceCanvasText.text = sentence[1];
        //Debug.Log(sentence);
    }
    void EndDialogue()
    {
        cameraScript.ResetCamera();
        DialogBoxAnimator.SetBool("show", false);
        stagScript.StartEndConversation();
        dialogue.stagInterlocutorScript.StartEndConversation();
        //Debug.Log("Ending conversation between " + stagScript.name + " and " + dialogue.stagInterlocutorScript.name + ".");
    }

}
