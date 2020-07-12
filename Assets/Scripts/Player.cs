using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    public float runSpeed = 3.0f;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
        walkRunJump();
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

        if ( animator.GetBool("Run"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * runSpeed;
        }else if (animator.GetBool("Walk"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * walkSpeed;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 0.0f;
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

        if ( rot)
        {
            gameObject.transform.rotation = Quaternion.Euler(0.0f, rotAngle, 0.0f);
        }
    }

    void WalkAndBack()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("WF", true);
        }
        else
        {
            animator.SetBool("WF", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("WB", true);
        }
        else
        {
            animator.SetBool("WB", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("WR", true);
        }
        else
        {
            animator.SetBool("WR", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("WL", true);
        }
        else
        {
            animator.SetBool("WL", false);
        }
    }
}
