using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableAnimal : MonoBehaviour
{
    public string animalName { get; set; }
    public bool goingDialog { get; set; }
    public RenderTexture textureRenderer { get; set; }

    public Queue<SOMission> missions { get; set; }
    public Queue<SOMission> succeededMissions { get; set; }
    public SOMission currentMission { get; set; }
    public virtual void MoveToStagForward() { }
    public virtual void AbleDisableMovement(bool p_goingDialog) { }

    public virtual void DequeueMission() { }
}
