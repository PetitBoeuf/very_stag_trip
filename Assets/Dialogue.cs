using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    //public string name;
    //[TextArea(3,10)]
    //public string[] sentences;
    public StagScript stagScript { get; set; }
    public TalkableScript stagInterlocutor { get; set; }
    public List<string[]> dialogList { get; set; }

    public Dialogue(StagScript stagScript, TalkableScript stagInterlocutor, List<string[]> dialogList)
    {
        this.stagScript = stagScript;
        this.stagInterlocutor = stagInterlocutor;
        this.dialogList = dialogList;
    }

    public Dialogue()
    {
    }
}
