using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnimal : MonoBehaviour
{
    public string animalName { get; set; }
    public bool goingDialog { get; set; }

    protected Queue<IMission> missions;
    protected Queue<IMission> succeededMissions;
    public IMission currentMission { get; set; }
    public virtual void AbleDisableMovement(bool p_goingDialog) { }

    public virtual void DequeueMission() { }
}
