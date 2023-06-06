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
    private TalkableScript stagScript;
    private TalkableScript interlocutorScript;
    private Dialogue dialogue;
    public Animator DialogBoxAnimator;

    void Start()
    {
        sentences = new Queue<string[]>();
        dialogue = new Dialogue();
    }

    public void StartDialogue(Dialogue p_dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);
        DialogBoxAnimator.SetBool("show", true);
        dialogue = p_dialogue;
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
        DialogBoxAnimator.SetBool("show", false);
        dialogue.stagScript.StartEndConversation();
        dialogue.stagInterlocutor.StartEndConversation();
        Debug.Log("Ending conversation between " + dialogue.stagScript.name + " and " + dialogue.stagInterlocutor.name + ".");
    }

}
