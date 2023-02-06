using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;                  // A reference to the main camera in the scenes transform
    public Vector3 moveDir;
    public Vector3 movement;

    [Header ("Gravity")]
    public float gravity = 10;
    public float constantGravity = 0.6f;
    public float maxGravity = -15;

    private float currentGravity;
    private Vector3 gravityDirection = Vector3.down;
    private Vector3 gravityMovement;

    //public Rigidbody rb;
    public Animator animator; 

     private void Start()
        {
            //rb = GetComponent<Rigidbody>();
            // get the transform of the main camera
            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            }
        }

    private bool IsGrounded()
    {
        return controller.isGrounded;
    }

    private void CalculateGravity()
    {
        if (IsGrounded())
        {
            currentGravity = constantGravity;
        }else
        {
            if(currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }
        gravityMovement = gravityDirection * -currentGravity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateGravity();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        
                
        if(direction.magnitude >= 0.1f) 
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            movement = moveDir.normalized * speed * Time.deltaTime;
            controller.Move(movement);
            animator.SetFloat("speed",controller.velocity.magnitude);// this is just for animation
        }else{
            animator.SetFloat("speed",controller.velocity.magnitude);
        }
        controller.Move(gravityMovement);
        
         //rigidbody.velocity.magnitude
        
    }

    
}