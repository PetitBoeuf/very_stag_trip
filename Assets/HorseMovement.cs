//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HorseMovement : MonoBehaviour

{
    [Header("Sphere Settings")]
    [SerializeField]
    private Transform sphereGO = null;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private float sphereRadius = 0.4f;

    //[SerializeField]
    //private LayerMask layerMask = new LayerMask();

    [Header("UI Settings")]
    //public TextMeshProUGUI appleTextCounter;
    //private int appleCounter = 0;

    //public GameObject[] children;

    //public Rigidbody boarRb;
    //public Animator boarAnimator;

    [Header("Stag Settings")]
    private Animator stagAnimator;
    private Rigidbody stagRb;
    //[SerializeField]
    //private Vector3 stagVelocity;
    //public GameObject stagGO;

    public float walkFactor = 1.5f;
    public float runFactor = 4f;
    private float factor;
    private float speed = 0f;
    private int stagRotationFactor = 360;

    public Transform orientation;
    private bool isEating = false;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        stagRb = GetComponent<Rigidbody>();
        //stagGO = GetComponent<GameObject>();
        //stagTransform = GetComponent<Transform>();
        stagAnimator = GetComponent<Animator>();

        //appleTextCounter.text = $"Pommes tap�es : 0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(sphereGO.position, sphereRadius);

        /* Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius);
         foreach (Collider hitCollider in hitColliders)
         {
             if(hitCollider.gameObject.CompareTag("ground"))
             {
                 isGrounded = true;
             }
         }*/

        //Post-it pour g�rer les animations - stagAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime te donne le nombre de fois en chiffre que l'animation a tourn�, quelque soit la vitesse et le temps, si t'en es � 50% de la 3�me fois de l'animation sur le retour de normalized time t'auras 3.5 environ. 

        //Guard Clause / Early return principle to avoid overnested if statements
        //We move out "wrong" conditions and then we write the "real" body of the Update function of the Stag Movement Script

        if (!isGrounded)
        {
            //Is jumping - letting impulse make its job
            //stagAnimator.SetBool("Jump", true);
            return;
        }


        if (ActiveAnimation("StagEat"))
        {
            //If he's eating, set speed to 0 to avoid stag start running asa the animation stops.
            speed = 0;
            stagAnimator.SetFloat("Speed", speed);
            return;
        }

        //If player wants to eat
        //Missing -> check apple / mushroom nearby
        if (!isEating &&
             Input.GetKey(KeyCode.E))
        {
            isEating = true;
            stagAnimator.SetBool("Eat", isEating);
            //speed = 0;
            //stagAnimator.SetFloat("Speed", speed);
            return;
        }

        float forwardMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        //If player is not moving (0 values at Axis Inputs) and he is not doing a "NotMovingAnimation"
        //Reset all values to smooth animation transitions ---> make sure this is the "right" thing to do (?)
        if (Mathf.Abs(forwardMovement) == 0 &&
            Mathf.Abs(horizontalMovement) == 0 &&
            !ActiveAnimation("StagEat"))
        {
            speed = 0f;
            stagAnimator.SetFloat("Speed", speed);
            isEating = false;
            stagAnimator.SetBool("Eat", isEating);
            return;
        }



        #region MovingStag

        //This prevents values not getting resetted since we can start walking/running asa the Eating animation ends.
        //Therefore, we reset it to make our script work correctly.
        if (isGrounded)
        {

            isEating = false;
            isJumping = false;

            Vector3 inputDirection =
                forwardMovement * Vector3.forward + horizontalMovement * Vector3.right;

            Quaternion destRotation = Quaternion.LookRotation(orientation.forward * forwardMovement + orientation.right * horizontalMovement, orientation.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotation, stagRotationFactor * Time.deltaTime);


            factor = walkFactor;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                factor = runFactor;
            }

            speed = factor;

            transform.Translate(inputDirection * speed * Time.deltaTime, orientation);
            //transform.Translate(inputDirection * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
                //stagRb.AddForce(transform.forward + new Vector3(0, 1f, 0f), ForceMode.Impulse);
                stagRb.AddForce(transform.forward + new Vector3(0, 1f, 0f), ForceMode.Impulse);
            }
            //stagAnimator.SetBool("Eat", isEating);
            stagAnimator.SetFloat("Speed", speed);
            stagAnimator.SetBool("Eat", isEating);
            stagAnimator.SetBool("Jump", isJumping);

            //if (isGrounded)
            //{
            //    stagAnimator.SetBool("Jump", false);
            //    stagRb.velocity = Vector3.zero;
            //}
        }



        #endregion
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Apple")
        {
            //appleCounter++;
            //appleTextCounter.text = $"Pommes tap�es : {appleCounter}";
            Destroy(collision.gameObject);
        }
    }

    bool ActiveAnimation(string animationName)
    {
        //Debug.Log(stagAnimator.GetCurrentAnimatorStateInfo(0).length);
        //Debug.Log(stagAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //Debug.Log(stagAnimator.GetCurrentAnimatorStateInfo(0).length > stagAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        return stagAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
    bool IsEating()
    {
        //Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StagEat"));
        return stagAnimator.GetCurrentAnimatorStateInfo(0).IsName("StagEat") && stagAnimator.GetCurrentAnimatorStateInfo(0).length > stagAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool IsRunning()
    {
        //Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StagEat"));
        return stagAnimator.GetCurrentAnimatorStateInfo(0).IsName("StagRun") && stagAnimator.GetCurrentAnimatorStateInfo(0).length > stagAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
