using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public float rotateSpeed = 120f;
    Vector2 movement;
    Vector2 mousePos;


    Animator animator;
    bool isMoving;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = false;
    }

    // Update is called once per frame
    void Update () {

        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        //animator.SetFloat("Speed", movement.y);

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * (-rotateSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
        }


        if(Input.GetKey(KeyCode.UpArrow))
        {

            transform.position += transform.right * Time.deltaTime * moveSpeed;
            if(isMoving !=true)
            {
                isMoving = true;
                setMoving(isMoving);
            }

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
            if (isMoving != true)
            {
                isMoving = true;
                setMoving(isMoving);
            }
            
        }
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            if(isMoving == true)
            {
                isMoving = false;
                setMoving(isMoving);
            }
            
        }

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
	}
    private void setMoving(bool move)
    {
        animator.SetBool("IsMoving", move);
    }

}
