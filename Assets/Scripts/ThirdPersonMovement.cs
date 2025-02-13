using System.Collections;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;
    public GameObject breakableWall;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    bool isJumping;
    bool isAttacking;

    public float speed = 6f;
    public float sprintSpeed = 12f;
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
            velocity.y = -2;
            
        }

        isJumping = false;
        isAttacking = false;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float verticle = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, verticle).normalized;
        anim.SetFloat("speed", direction.magnitude);
        anim.SetFloat("Yvelocity", velocity.y);
        //anim.SetFloat("Jump", velocity.y);]
        anim.SetBool("falling", false);


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            

        }

        

        if (Input.GetButton("Jump") && isGrounded)
        {
            speed = 2f;
            StartCoroutine(JumpDelay());
            anim.SetBool("jumping", true);
            isJumping = true;
           
        }

        if (isJumping == false)
        {
            anim.SetBool("jumping", false);
            //speed = 6f;

        }


        if (velocity.y < -2.3f && isGrounded == false)
        {
            anim.SetBool("falling", true);
        }

        if (velocity.y < -2.1f && isGrounded == true)
        {
            anim.SetBool("falling", false);
        }

     


        StartCoroutine(Attack());


        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        


        //Attack();
        
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds (0.5f);
        velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        speed = 6f;
    }

    IEnumerator Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("punch", true);
            isAttacking = true;
            speed = 0f;
            yield return new WaitForSeconds(1.3f);
            anim.SetBool("punch", false);
            speed = 6f;
        }

        /*if (isAttacking == false)
        {
            anim.SetBool("punch", false);
            speed = 6f;
        }*/


    }

    private void OnTriggerEnter(Collider other)
    {
        breakableWall.SetActive(false);
    }

}