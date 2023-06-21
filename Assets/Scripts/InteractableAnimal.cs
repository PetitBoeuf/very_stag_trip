using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableAnimal : MonoBehaviour
{
    public string animalName { get; set; }
    public bool goingDialog { get; set; }
    public RenderTexture textureRenderer { get; set; }

    protected Queue<IMission> missions;
    protected Queue<IMission> succeededMissions;
    public IMission currentMission { get; set; }
    public virtual void AbleDisableMovement(bool p_goingDialog) { }

    public virtual void DequeueMission() { }
}
