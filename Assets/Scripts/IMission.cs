using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MissionState
{
    Sleep,
    Started,
    Failed,
    Success,
};

public interface IMission
{
    InteractableAnimal interactableAnimal { get; set; }

    StagManager stagManager { get; set; }

    MissionState missionState { get; set; }
    DialogueManager dialogueManager { get; set; }
    List<string[]> startSentences { get; set; }
    List<string[]> failureSentences { get; set; }
    List<string[]> successSentences { get; set; }

    List<string[]> HandleMission();
    bool MissionSolved(); 

}
