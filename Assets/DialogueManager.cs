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
    void Start()
    {
        sentences = new Queue<string[]>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

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
        Debug.Log("Ending conversation");
    }

}
