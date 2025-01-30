using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector3 anyVector;

    private void Start()
    {
        
    }

    void Update()
    {
       


        AnimationPlayer();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float verticle = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, verticle).normalized;

        

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        controller.SimpleMove(anyVector);


        
        
    }

    public void AnimationPlayer()
    {
        if (Input.GetKey("w"))
        {
            anim.SetBool("walk", true);

            
        }

        else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKey("a"))
        {
            anim.SetBool("walk", true);
        }

        else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKey("s"))
        {
            anim.SetBool("walk", true);
        }

        else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKey("d"))
        {
            anim.SetBool("walk", true);
        }

        else
        {
            anim.SetBool("walk", false);
        }




    }
}