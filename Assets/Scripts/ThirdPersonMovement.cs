using System.Collections;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float speed = 6f;
    public float gravity = 9.81f;
    public float rotationSpeed = 90;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector3 velocity;
    
  
 
    private void Start()
    {
        
    }

    void Update()
    {
       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <0)
        {
            velocity.y = 0f;
            
        }


        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float verticle = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, verticle).normalized;
        anim.SetFloat("speed", direction.magnitude);
        anim.SetFloat("Yvelocity", velocity.y);
        anim.SetFloat("Jump", velocity.y);


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(JumpDelay());
            anim.SetBool("jumping", true);
            
        }
        
        


        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        
        
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds (0.5f);
        velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
    }

    
}