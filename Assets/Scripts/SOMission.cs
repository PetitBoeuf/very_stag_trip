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

[System.Serializable]
[CreateAssetMenu(fileName = "Mission", menuName = "Missions")]
public class SOMission : ScriptableObject
{
    public string title;
    public string description;
    public string biome;
    public InteractableAnimal interactableAnimal;
    public StagManager stagManager;
    public MissionState missionState;
    public DialogueManager dialogueManager;
    public List<string[]> startSentences;
    public List<string[]> failureSentences;
    public List<string[]> successSentences;

    public virtual void InitializeMission(
        InteractableAnimal interactableAnimal,
        DialogueManager dialogueManager,
        StagManager stagManager
        ) { }

    public List<string[]> HandleMission()
    {
        List<string[]> returnedSentences = new List<string[]>();

        switch (missionState)
        {
            case MissionState.Sleep:
                missionState = MissionState.Started;
                returnedSentences = startSentences;
                break;
            case MissionState.Started:
            case MissionState.Failed:

                //Default is set to failure sentences
                returnedSentences = failureSentences;

                if (MissionSolved())
                {
                    missionState = MissionState.Success;
                    returnedSentences = successSentences;
                    //interactableAnimal.DequeueMission();
                }

                break;
        }
        return returnedSentences;
    }
    public bool MissionSolved()
    {
        return true;
    }

}
