using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : TalkableScript
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
    private bool talkingBool;
    public string rabbitName;

    [Header("Stag")]
    public Transform stagTransform;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rabbitAnimator = GetComponent<Animator>();
        rabbitName = "Carottino";
        talkingBool = false;
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

        if (talkingBool) return;

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

    public override void StartEndConversation()
    {
        if(!talkingBool)
        {
            Quaternion destRotation = Quaternion.LookRotation(stagTransform.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotation, rabbitRotationFactor);
        }
        talkingBool = !talkingBool;
    }

}