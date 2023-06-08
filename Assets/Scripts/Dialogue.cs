using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    //public string name;
    //[TextArea(3,10)]
    //public string[] sentences;
    //public StagScript stagScript { get; set; }
    public InteractableAnimal stagInterlocutorScript { get; set; }
    public Transform stagInterlocutorTransform { get; set; }
    public List<string[]> dialogList { get; set; }

    public Dialogue(InteractableAnimal stagInterlocutorScript, Transform stagInterlocutorTransform, List<string[]> dialogList)
    {
        //this.stagScript = stagScript;
        this.stagInterlocutorScript = stagInterlocutorScript;
        this.stagInterlocutorTransform = stagInterlocutorTransform;
        this.dialogList = dialogList;
    }

    public Dialogue()
    {
    }
}
