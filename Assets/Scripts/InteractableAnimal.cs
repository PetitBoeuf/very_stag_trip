using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnimal : MonoBehaviour
{
    public string animalName { get; set; }

    protected Queue<IMission> missions;
    protected Queue<IMission> succeededMissions;
    public IMission currentMission { get; set; }
    public virtual void StartEndConversation() { }

    public virtual void DequeueMission() { }
    

}
