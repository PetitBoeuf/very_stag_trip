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
    string title { get; set; }
    string description { get; set; }
    string biome { get; set; }
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
