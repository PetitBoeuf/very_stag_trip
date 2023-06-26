using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


public class RabbitManager : InteractableAnimal
{
    [Header("Sphere Settings")]
    public Transform sphereGO;
    public float sphereRadius;

    [Header("Jump Settings")]
    public float minJumpRange = 5;
    public float maxJumpRange = 10;
    [SerializeField]
    private float lastJumpTime = 0f;
    public float jumpPeriod = 4f;

    [Header("Floor Layer Settings")]
    public LayerMask floorLayer;

    private int rabbitRotationFactor = 360;

    private Rigidbody rb;
    private Animator rabbitAnimator;
    private bool isFalling;
    [SerializeField]
    private bool isGrounded;
    private bool isJumping;

    [Header("Dialog Settings")]
    [SerializeField]
    //private bool talkingBool;
    public string rabbitName;

    [Header("Stag")]
    public Transform stagTransform;
    public Transform stagOrientationTransform;

    public DialogueManager dialogueManager;
    public StagManager stagManager;
    public RenderTexture setTextureRenderer;
    public Queue<SOMission> rabbitMissions;

    public List<SOMission> soMissions;
    //public SharedMissionCollection sharedMissionCollection;

    void Start()
    {
        //sharedMissionCollection = GetComponent<SharedMissionCollection>();

        rb = GetComponent<Rigidbody>();
        rabbitAnimator = GetComponent<Animator>();
        //rabbitName = "Carottino";
        animalName = rabbitName;
        //talkingBool = false;
        missions = new Queue<SOMission>();

        foreach(SOMission som in soMissions)
        {
            missions.Enqueue(som);
        }
        //missions = soMissions;
        succeededMissions = new Queue<SOMission>();

        //missions.Enqueue(new MissionKarot(this, dialogueManager, stagManager));
        //missions.Enqueue(new MissionSoif(this, dialogueManager, stagManager));


        currentMission = missions.Dequeue();
        textureRenderer = setTextureRenderer;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        isGrounded = Physics.CheckSphere(sphereGO.position, sphereRadius, floorLayer);
        rabbitAnimator.SetBool("Grounded", isGrounded);

        if (!isGrounded)
        {
            if (rb.velocity.y < 0.3f)
            {

                if (!isFalling)
                {
                    isJumping = false;
                    isFalling = true;
                    rabbitAnimator.SetBool("Jumping", isJumping);
                    rabbitAnimator.SetBool("Falling", isFalling); 
                }
            }
            return;
        }

        if (goingDialog) return;

        if (lastJumpTime != 0)
            if(Time.time - lastJumpTime < jumpPeriod) return;

        #region JumpingRabbit
           
        if(Random.value > 0.8)
        {
            float factor = Random.Range(minJumpRange, maxJumpRange);
            Vector3 jumpDirection = Random.onUnitSphere.normalized * factor;
            jumpDirection.y = factor;

            Quaternion destRotation = Quaternion.LookRotation(new Vector3(jumpDirection.x, 0, jumpDirection.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotation, rabbitRotationFactor);

            rb.AddForce(jumpDirection, ForceMode.Impulse);



            isJumping = true;
            isFalling = false;
            rabbitAnimator.SetBool("Jumping", isJumping);
            rabbitAnimator.SetBool("Falling", isFalling);

            lastJumpTime = Time.time;
        }
        
        #endregion
    }
    public override void MoveToStagForward()
    {
        transform.position = stagTransform.forward * 10 + stagTransform.position;

        //Quaternion destStagRotation = Quaternion.LookRotation(transform.position - stagTransform.position, Vector3.up);

        Quaternion destRabbitRotation = Quaternion.LookRotation(stagTransform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, destRabbitRotation, rabbitRotationFactor);

        //stagTransform.rotation = Quaternion.RotateTowards(stagTransform.rotation, destStagRotation, rabbitRotationFactor);
    }

    public override void DequeueMission()
    {
        succeededMissions.Enqueue(currentMission);
        if(missions.Count > 0)
            currentMission = missions.Dequeue();
    }

    public override void AbleDisableMovement(bool p_goingDialog)
    {
        if (p_goingDialog == goingDialog)
            return;

        goingDialog = p_goingDialog;
    }

    //public interact()
    /*
     * currentMission.checkMission()
     * 
     * 
     *dans la Mission :  
     * 
     * public checkMission() trois chemins possibles : start / echec / succes
     * 
     * 
     * 
     * 
     */

    public void Interact()
    {
        //Debug.Log("Animal Interact passed");

        currentMission.HandleMission();
        if(currentMission.missionState == MissionState.Sleep)
        {
        }
    }
}
