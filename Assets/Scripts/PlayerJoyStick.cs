using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStick : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    public float runSpeed = 3.0f;
    public VariableJoystick variableJoystick;
    public bool gamePadSupport = false;
    public float runJoyStickValue = 0.5f;
    public float walkJoyStickValue = 0.1f;

    Animator animator;
    bool grounded = false;
    float angluarDrag = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        angluarDrag = gameObject.GetComponent<Rigidbody>().angularDrag;
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
        walkRunJump();

        if (variableJoystick != null)
        {
            moveByJoystick();
        }
        if ( gamePadSupport)
        {
            moveByGamePad();
        }
        
        setSpeedFromAnimationStatus();
    }

    private void FixedUpdate()
    {
        grounded = false;
        // Debug.Log(grounded);
    }

    void walkRunJump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }

    void setSpeedFromAnimationStatus()
    {
        if (animator.GetBool("Run"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * runSpeed;
        }
        else if (animator.GetBool("Walk"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * walkSpeed;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, gameObject.GetComponent<Rigidbody>().velocity.y, 0.0f);
        }

    }

    void changeDirection()
    {
        float rotAngle = 0.0f;
        bool rot = false;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle = 45.0f;
            rot = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle = -45.0f;
            rot = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle = 180.0f - 45.0f;
            rot = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle = 180.0f + 45.0f;
            rot = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rotAngle = 0.0f;
            rot = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotAngle = 180.0f;
            rot = true;

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotAngle = 90.0f;
            rot = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotAngle = -90.0f;
            rot = true;
        }

        if (rot)
        {
            gameObject.transform.rotation = Quaternion.Euler(0.0f, rotAngle, 0.0f);
        }
    }

    void moveByJoystick()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if (direction.magnitude < walkJoyStickValue)
        {
            // 回転止める
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            return;
        }

        if (grounded)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            if (direction.magnitude > runJoyStickValue)
            {
                animator.SetBool("Run", true);
            }
            else if (direction.magnitude > walkJoyStickValue)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }


    void moveByGamePad()
    {
        float hori2 = Input.GetAxis("Horizontal2");
        float vert2 = Input.GetAxis("Vertical2");
        if ((hori2 != 0) || (vert2 != 0))
        {
            Debug.Log("hori2:" + hori2);
            Debug.Log("vert2:" + vert2);
        }


        Vector3 direction = Vector3.forward * vert2 + Vector3.right * hori2;
        if (direction.magnitude < walkJoyStickValue)
        {
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            return;
        }

        if (grounded)
        {

            gameObject.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            if (direction.magnitude > runJoyStickValue)
            {
                animator.SetBool("Run", true);
            }
            else if (direction.magnitude > walkJoyStickValue)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
            }

            if (Input.GetKeyDown("joystick button 17"))
            {
                animator.SetTrigger("Jump");
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
